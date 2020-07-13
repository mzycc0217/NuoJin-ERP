using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.DeviceManagement;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.DeviceManagement;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.DeviceManagement;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.FinancialManagement.DeviceManagementAppService
{
    /// <summary>
    /// 设备服务
    /// </summary>
    [AppAuthentication]
    public class DeviceController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(DeviceInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var user = await db.UserDetails.Include(m => m.User).SingleOrDefaultAsync(m => m.Id == userId && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "身份有误，请重新登陆" }))
                });
            }

            var outStock = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId && !m.IsDelete);

            if (outStock == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "出库信息不存在" }))
                });
            }
            
            for (int i = 0; i < outStock.Number; i++)
            {
                db.Devices.Add(new Device()
                {
                    Id = IdentityManager.NewId(),
                    UserId = userId,
                    IsDelete = false,
                    IsScrap = false,
                    Usage = input.Usage,
                    DepartmentId = (long)user.User.DepartmentId,
                    RawMaterialId = outStock.RawMaterialId,
                    IsRepair = false
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 添加设备报修信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddDeviceRepair(DeviceRepairInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var user = await db.UserDetails.Include(m => m.User).SingleOrDefaultAsync(m => m.Id == userId && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "无权操作权限" }))
                });
            }

            if (!await db.Devices.AnyAsync(m => m.Id == input.DeviceId && !m.IsDelete && !m.IsScrap && m.DepartmentId == user.User.DepartmentId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该设备不存在或已报废、无操作权限" }))
                });
            }

            if (await db.DeviceRepairs.AnyAsync(m=>m.DeviceId == input.DeviceId && m.ApprovalType != EApprovalType.Completed))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该设备已在维修" }))
                });
            }

            var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "DeviceRepair");

            if (related == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "设备报修申请未绑定审批流，添加失败" }))
                });
            }

            var approval = await db.Approvals.Where(m => m.Key == related.ApprovalKey).ToListAsync();

            if (approval == null && approval.Count <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = related.ApprovalKey + "该审批流不存在，添加失败" }))
                });
            }

            var data = new DeviceRepair()
            {
                Id = IdentityManager.NewId(),
                ApprovalType = EApprovalType.UnderReview,
                Desc = input.Desc,
                DeviceId = input.DeviceId,
                UserId = userId,
                IsDelete = false,
                ApprovalKey = related.ApprovalKey,
                ApprovalIndex = 0
            };

            db.DeviceRepairs.Add(data);

            var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == data.ApprovalKey);

            if (userTypeKey != null)
            {
                db.DeviceRepairApprovals.Add(new DeviceRepairApproval
                {
                    Id = IdentityManager.NewId(),
                    DeviceRepairId = data.Id,
                    IsApproval = false,
                    UserTypeKey = userTypeKey.UserTypeKey,
                    ApprovalIndex = 1
                });
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流异常" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新设备报修审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateRepairApprovalType(DeviceRepairInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            //var userId = 42966847102676992;

            var user = await db.UserDetails.Include(m=>m.User).SingleOrDefaultAsync(m => m.Id == userId && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "无权操作权限" }))
                });
            }

            var data = await db.DeviceRepairs.Include(m => m.Device).SingleOrDefaultAsync(m => m.Id == input.DeviceRepairId && !m.IsDelete && !m.Device.IsScrap);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该设备报修信息不存在或已报废、无操作权限" }))
                });
            }

            if (data.ApprovalType == EApprovalType.Rejected)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该订单已被驳回" }))
                });
            }

            var approval = await db.Approvals.Where(m => m.Key == data.ApprovalKey).ToListAsync();

            if (approval == null && approval.Count <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "该审批流不存在，请重新提交申请" }))
                });
            }

            var index = data.ApprovalIndex + 1;

            var maxIndex = approval.Max(m => m.Deis);

            var userApproval = approval.SingleOrDefault(m => m.Deis == index);

            if (userApproval != null && input.ApprovalType == null)
            {
                if (!userTypeKeys.Contains(userApproval.UserTypeKey))
                {

                //}
                //if (userApproval.UserTypeKey != token.UserTypeKey)
                //{
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "无权操作" }))
                    });
                }
                else
                {
                    data.ApprovalIndex += 1;

                    data.ApprovalType = EApprovalType.InExecution;

                    //var deviceRepairApproval = await db.DeviceRepairApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.DeviceRepairId == data.Id);

                    var deviceRepairApproval = await db.DeviceRepairApprovals.SingleOrDefaultAsync(m => userTypeKeys.Contains(m.UserTypeKey) && m.ApprovalIndex == index && m.DeviceRepairId == data.Id);

                    if (deviceRepairApproval == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
                        });
                    }

                    deviceRepairApproval.IsApproval = true;
                    deviceRepairApproval.UserId = token.UserId;

                    var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

                    if (userTypeKey != null)
                    {
                        db.DeviceRepairApprovals.Add(new DeviceRepairApproval
                        {
                            Id = IdentityManager.NewId(),
                            DeviceRepairId = data.Id,
                            IsApproval = false,
                            UserTypeKey = userTypeKey.UserTypeKey,
                            ApprovalIndex = index + 1
                        });
                    }

                    if (maxIndex == data.ApprovalIndex)
                    {
                        data.ApprovalType = EApprovalType.Reviewed;
                    }

                }
            }
            else if (maxIndex == data.ApprovalIndex && input.ApprovalType == null)
            {
                data.ApprovalType = EApprovalType.Reviewed;

                var device = await db.Devices.SingleOrDefaultAsync(m => m.Id == data.DeviceId && !m.IsDelete && !m.IsRepair);

                if (device != null)
                {
                    device.IsRepair = true;
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.DeviceId + "不存在或已报废" }))
                    });
                }
            }
            else if(input.ApprovalType != null)
            {
                data.ApprovalType = (EApprovalType)input.ApprovalType;

                if (data.ApprovalType == EApprovalType.Reviewed)
                {
                    var device = await db.Devices.SingleOrDefaultAsync(m => m.Id == data.DeviceId && !m.IsDelete && !m.IsScrap);

                    if (device != null)
                    {
                        device.IsRepair = true;
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.DeviceId + "不存在或已报废" }))
                        });
                    }
                }
                else if (data.ApprovalType == EApprovalType.Completed)
                {
                    var device = await db.Devices.SingleOrDefaultAsync(m => m.Id == data.DeviceId && !m.IsDelete && !m.IsScrap);

                    if (device != null)
                    {
                        device.IsRepair = false;
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.DeviceId + "不存在或已报废" }))
                        });
                    }
                }
                else if (data.ApprovalType == EApprovalType.No)
                {
                    var device = await db.Devices.SingleOrDefaultAsync(m => m.Id == data.DeviceId && !m.IsDelete && !m.IsScrap);

                    if (device != null)
                    {
                        device.IsScrap = true;
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.DeviceId + "不存在或已报废" }))
                        });
                    }
                }
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 修改设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(DeviceInputDto input)
        {
            var data = await db.Devices.SingleOrDefaultAsync(m => m.Id == input.DeviceId);

            if (data != null)
            {
                data.Usage = input.Usage;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "设备不存在" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 设备报废
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeviceScrap(List<long> Ids)
        {
            var list = new List<string>();

            foreach (var item in Ids)
            {
                var data = await db.Devices.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete && !m.IsScrap);

                if (data != null)
                {
                    data.IsScrap = true;
                }
                else
                {
                    list.Add("【"+item+"】不存在或已删除、已报废");
                }
            }

            return list;
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<DeviceOutputDto>> GetList(GetDeviceInputDto input)
        {
            var data = await db.Devices
                     .Include(m => m.Department)
                     .Include(m => m.User)
                     .Where(m=>!m.IsDelete)
                     .ToListAsync();

            if (input.DeviceId != null)
            {
                data = data.Where(m => m.Id == input.DeviceId).ToList();
            }

            if (input.ServiceLife != null)
            {
                data = data.Where(m => m.RawMaterial.ServiceLife == input.ServiceLife).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.RawMaterial.Name != null && m.RawMaterial.Name.Contains(input.Name)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Usage))
            {
                data = data.Where(m => m.Usage != null && m.Usage.Contains(input.Usage)).ToList();
            }

            var list = new List<DeviceOutputDto>();

            foreach (var item in data)
            {
                var repairCount = await db.DeviceRepairs.CountAsync(m => m.DeviceId == item.Id && !m.IsDelete &&  m.ApprovalType == EApprovalType.Completed);

                list.Add(new DeviceOutputDto()
                {
                    DeviceId = item.Id.ToString(),
                    CreateDate = item.CreateDate,
                    Department = item.Department,
                    DepartmentId = item.DepartmentId.ToString(),
                    IsScrap = item.IsScrap,
                    ServiceLife = (double)item.RawMaterial.ServiceLife,
                    UpdateDate = item.UpdateDate,
                    Usage = item.Usage,
                    User = item.User,
                    UserId = item.UserId.ToString(),
                    RepairCount = repairCount,
                    Name = item.RawMaterial.Name,
                    TechnicalDescription = item.RawMaterial.TechnicalDescription,
                    IsRepair = item.IsRepair
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.DeviceId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;

        }

        /// <summary>
        /// 获取设备报修信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<DeviceRepairOutputDto>> GetDeviceRepairs(GetDeviceRepairInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.DeviceRepairs
                    .Include(m => m.User)
                    .Include(m => m.Device)
                    .Include(m=>m.Device.RawMaterial)
                    .Where(m=>!m.IsDelete)
                    .ToListAsync();

            if (input.IsApproval)
            {
                var deviceRepairApprovals = await db.DeviceRepairApprovals
                    .Include(m=>m.DeviceRepair)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.DeviceRepair.IsDelete && m.DeviceRepair.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.DeviceRepair.IsDelete && m.DeviceRepair.ApprovalType != EApprovalType.Rejected)
                    .Select(m => m.DeviceRepair)
                    .ToListAsync();

                data = deviceRepairApprovals;
            }

            if (input.UserId != null)
            {
                var deviceRepairApprovals = await db.DeviceRepairApprovals
                    .Include(m => m.DeviceRepair)
                    .Where(m => m.UserId == input.UserId && !m.DeviceRepair.IsDelete && m.DeviceRepair.ApprovalType == EApprovalType.Paid && m.IsApproval)
                    .Select(m => m.DeviceRepair)
                    .ToListAsync();

                data = deviceRepairApprovals;
            }

            if (input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            if (input.DeviceId != null)
            {
                data = data.Where(m => m.DeviceId == input.DeviceId).ToList();
            }

            if (input.DeviceRepairId != null)
            {
                data = data.Where(m => m.Id == input.DeviceRepairId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Device.RawMaterial.Name != null && m.Device.RawMaterial.Name.Contains(input.Name)).ToList();
            }

            var list = new List<DeviceRepairOutputDto>();

            foreach (var item in data)
            {
                var repairCount = await db.DeviceRepairs.CountAsync(m => m.DeviceId == item.DeviceId && !m.IsDelete && m.ApprovalType == EApprovalType.Completed);

                list.Add(new DeviceRepairOutputDto()
                {
                    CreateDate = item.CreateDate,
                    UserId = item.UserId.ToString(),
                    User = new UserOutputDto { 
                    UserId = item.UserId.ToString(),
                    RealName = item.User.RealName,
                    Address = item.User.Address,
                    CreateDate = item.User.CreateDate,
                    DateOfBirth = item.User.DateOfBirth
                    },
                    ApprovalType = item.ApprovalType,
                    Desc = item.Desc,
                    Device = new DeviceOutputDto { 
                    CreateDate = item.Device.CreateDate,
                    UserId = item.Device.UserId.ToString(),
                    User = item.Device.User,
                    DepartmentId = item.Device.DepartmentId.ToString(),
                    DeviceId = item.DeviceId.ToString(),
                    IsRepair = item.Device.IsRepair,
                    Department = item.Device.Department,
                    IsScrap = item.Device.IsScrap,
                    Name = item.Device.RawMaterial.Name,
                    RepairCount = repairCount,
                    ServiceLife = (double)item.Device.RawMaterial.ServiceLife,
                    TechnicalDescription = item.Device.RawMaterial.TechnicalDescription,
                    Usage = item.Device.Usage
                    },
                    DeviceId = item.DeviceId.ToString(),
                    DeviceRepairId = item.Id.ToString(),
                    IsDelete = item.IsDelete,
                    UpdateDate = item.UpdateDate
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.DeviceId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;

        }
    }
}
