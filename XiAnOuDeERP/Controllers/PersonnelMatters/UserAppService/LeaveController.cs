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
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.PersonnelMatters.UserAppService
{
    /// <summary>
    /// 请假服务
    /// </summary>
    [AppAuthentication]
    public class LeaveController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加请假
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(LeaveInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "Leave");

            if (related == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请假申请未绑定审批流，添加失败" }))
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

            var data = await db.Leaves.SingleOrDefaultAsync(m => m.UserId == userId && m.StartTime <= input.StartTime && m.EndTime >= input.EndTime && !m.IsDelete);

            if (data != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "已提交请假申请" }))
                });
            }

            var leave = new Leave()
            {
                Id = IdentityManager.NewId(),
                ApprovalType = EApprovalType.UnderReview,
                Desc = input.Desc,
                UserId = userId,
                EndTime = input.EndTime,
                StartTime = input.StartTime,
                IsDelete = false,
                LeaveType = input.LeaveType,
                ApprovalIndex = 0,
                ApprovalKey = related.ApprovalKey
            };

            db.Leaves.Add(leave);

            var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == data.ApprovalKey);

            if (userTypeKey != null)
            {
                db.LeaveApprovals.Add(new LeaveApproval
                {
                    Id = IdentityManager.NewId(),
                    LeaveId = leave.Id,
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
        /// 更新请假
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(LeaveInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var data = await db.Leaves.SingleOrDefaultAsync(m => m.Id == input.LeaveId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请假记录不存在" }))
                });
            }

            var leave = await db.Leaves.SingleOrDefaultAsync(m => m.UserId == userId && m.StartTime <= input.StartTime && m.EndTime >= input.EndTime && !m.IsDelete);

            if (leave != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "已提交请假申请" }))
                });
            }

            data.Desc = input.Desc;
            data.EndTime = input.EndTime;
            data.StartTime = input.StartTime;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 更新请假审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateLeaveApproval(LeaveInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.Leaves.SingleOrDefaultAsync(m => m.Id == input.LeaveId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请假记录不存在" }))
                });
            }

            if (data.ApprovalType == EApprovalType.Rejected)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该请假申请已被驳回" }))
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

            if (userApproval != null)
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

                    //var leaveApproval = await db.LeaveApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.LeaveId == data.Id);
                    var leaveApproval = await db.LeaveApprovals.SingleOrDefaultAsync(m => userTypeKeys.Contains(m.UserTypeKey) && m.ApprovalIndex == index && m.LeaveId == data.Id);

                    if (leaveApproval == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
                        });
                    }

                    leaveApproval.IsApproval = true;
                    leaveApproval.UserId = token.UserId;

                    var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

                    if (userTypeKey != null)
                    {
                        db.LeaveApprovals.Add(new LeaveApproval
                        {
                            Id = IdentityManager.NewId(),
                            LeaveId = data.Id,
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
            }
            else
            {
                if (input.ApprovalType == EApprovalType.Rejected)
                {
                    data.ApprovalType = input.ApprovalType;
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
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> Ids)
        {
            var list = new List<string>();

            foreach (var item in Ids)
            {
                var data = await db.Leaves.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add("【" + item + "不存在】");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取请假数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<LeaveOutputDto>> GetList(GetLeaveInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.Leaves
                    .Include(m => m.User)
                    .Where(m => !m.IsDelete).ToListAsync();

            if (input.IsApproval)
            {
                var leave = await db.LeaveApprovals
                    .Include(m => m.Leave)
                    .Include(m=>m.Leave.User)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.Leave.IsDelete && m.Leave.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.Leave.IsDelete && m.Leave.ApprovalType != EApprovalType.Rejected)
                    .Select(m=>m.Leave)
                    .ToListAsync();

                data = leave;
            }

            if (input.ApprovelId != null)
            {
                var leave = await db.LeaveApprovals
                    .Include(m => m.Leave)
                    .Include(m => m.Leave.User)
                    .Where(m => m.UserId == input.ApprovelId && !m.Leave.IsDelete && m.Leave.ApprovalType == EApprovalType.Reviewed)
                    .Select(m=>m.Leave)
                    .ToListAsync();

                data = leave;
            }

            if (input.LeaveId != null)
            {
                data = data.Where(m => m.Id == input.LeaveId).ToList();
            }

            if (input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.User.User.DepartmentId == input.DepartmentId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                data = data.Where(m => m.User.RealName != null && m.User.RealName.Contains(input.UserName)).ToList();
            }

            var list = new List<LeaveOutputDto>();

            foreach (var item in data)
            {
                list.Add(new LeaveOutputDto()
                {
                    ApprovalDesc = item.ApprovalDesc,
                    ApprovalType = item.ApprovalType,
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    EndTime = item.EndTime,
                    LeaveId = item.Id.ToString(),
                    LeaveType = item.LeaveType,
                    LeaveTypeStr = item.LeaveType.GetDescription(),
                    StartTime = item.StartTime,
                    UpdateDate = item.UpdateDate,
                    User = item.User,
                    UserId = item.UserId.ToString()
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.LeaveId)
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
