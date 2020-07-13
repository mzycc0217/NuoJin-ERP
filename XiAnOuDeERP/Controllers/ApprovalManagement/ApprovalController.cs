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
using XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement;
using XiAnOuDeERP.Models.Db.Aggregate.Departent_User;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Dto;
using XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Dto.My_FlowDto;
using XiAnOuDeERP.Models.Dto.OutputDto.ApprovalMangement;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.ApprovalManagement
{
    /// <summary>
    /// 审批服务
    /// </summary>
    [AppAuthentication]
   [RoutePrefix("api/flow")]
    public class ApprovalController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(ApprovalInputDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Key))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Key + "不能为空" }))
                });
            }

            if (await db.Approvals.AnyAsync(m=>m.Key == input.Key && m.UserTypeKey == input.UserTypeKey))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.UserTypeKey + "该用户类型Key已绑定该流程" }))
                });
            }

            if (!await db.UserTypes.AnyAsync(m => m.Key == input.UserTypeKey))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.UserTypeKey + "不存在" }))
                });
            }

            var maxList = await db.Approvals.Where(m => m.Key == input.Key).Select(m => m.Deis).ToListAsync();

            var max = 0;

            if (maxList != null && maxList.Count > 0)
            {
                max = maxList.Max();
            }

            db.Approvals.Add(new Approval
            {
                Id = IdentityManager.NewId(),
                Key = input.Key,
                Deis = max + 1,
                UserTypeKey = input.UserTypeKey
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 模块绑定审批流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddRelatedApproval(RelatedApprovalInputDto input)
        {
            var data = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == input.RelatedKey);

            if (!await db.Approvals.AnyAsync(m => m.Key == input.ApprovalKey))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ApprovalKey + "不存在" }))
                });
            }

            if (data != null)
            {
                data.ApprovalKey = input.ApprovalKey;
            }
            else
            {
                db.RelatedApprovals.Add(new RelatedApproval
                {
                    Id = IdentityManager.NewId(),
                    ApprovalKey = input.ApprovalKey,
                    RelatedKey = input.RelatedKey
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
        /// 根据key删除指定审批流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteApprovalOfKey(DeleteApprovalOfKeyInputDto input)
        {
            var data = await db.Approvals.Where(m => m.Key == input.Key).ToListAsync();

            var list = new List<string>();

            if (data == null && data.Count < 1)
            {
                list.Add(input.Key + "不存在");
                return list;
            }

            foreach (var item in data)
            {
                db.Approvals.Remove(item);
            }
            await db.SaveChangesAsync();
            return list;
        }

        /// <summary>
        /// 根据Id删除指定审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteApprovalOfId(DeleteApprovalOfIdInputDto input)
        {
            var data = await db.Approvals.SingleOrDefaultAsync(m => m.Id == input.ApprovalId);

            var list = new List<string>();

            if (data != null)
            {
                var dataList = await db.Approvals.Where(m => m.Key == data.Key && m.Deis > data.Deis).ToListAsync();

                foreach (var item in dataList)
                {
                    item.Deis -= 1;
                }

                db.Approvals.Remove(data);
            }
            else
            {
                list.Add(input.ApprovalId + "不存在");
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取审批流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ApprovalOutputDto>> GetList(GetApprovalInputDto input)
        {
            var data = await db.Approvals.ToListAsync();

            if (input.ApprovalId != null)
            {
                data = data.Where(m => m.Id == input.ApprovalId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.ApprovalKey))
            {
                data = data.Where(m => m.Key == input.ApprovalKey).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeKey))
            {
                data = data.Where(m => m.UserTypeKey == input.UserTypeKey).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RelatedKey))
            {
                var data1 = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == input.RelatedKey);

                if (data1 == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.RelatedKey + "不存在" }))
                    });
                }

                data = data.Where(m => m.Key == data1.ApprovalKey).ToList();
            }

            var list = new List<ApprovalOutputDto>();

            foreach (var item in data)
            {
                var relatedKey = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.ApprovalKey == item.Key);

                list.Add(new ApprovalOutputDto
                {
                    ApprovalId = item.Id.ToString(),
                    ApprovalKey = item.Key,
                    UserTypeKey = item.UserTypeKey,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    Deis = item.Deis,
                    RelatedKey = relatedKey == null ? "" : relatedKey.RelatedKey
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.ApprovalId)
                .OrderByDescending(m => m.ApprovalKey)
                .OrderBy(m => m.Deis)
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
        /// 获取审批流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ApprovalListOutputDto>> GetApprovalList(GetApprovalInputDto input)
        {
            var data = await db.Approvals.ToListAsync();

            if (input.ApprovalId != null)
            {
                data = data.Where(m => m.Id == input.ApprovalId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.ApprovalKey))
            {
                data = data.Where(m => m.Key == input.ApprovalKey).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeKey))
            {
                data = data.Where(m => m.UserTypeKey == input.UserTypeKey).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RelatedKey))
            {
                var data1 = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == input.RelatedKey);

                if (data1 == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.RelatedKey + "不存在" }))
                    });
                }

                data = data.Where(m => m.Key == data1.ApprovalKey).ToList();
            }

            var list = new List<ApprovalListOutputDto>();
            var approval = new List<ApprovalOutputDto>();

            var key = data.GroupBy(m => m.Key).Select(m => m.Key).ToList();

            foreach (var item in key)
            {
                approval = new List<ApprovalOutputDto>();

                var approvalList = data.Where(m => m.Key == item).ToList();

                foreach (var item1 in approvalList)
                {
                    var userTypeName = await db.UserTypes.SingleOrDefaultAsync(m => m.Key == item1.UserTypeKey);

                    approval.Add(new ApprovalOutputDto
                    {
                        ApprovalId = item1.Id.ToString(),
                        ApprovalKey = item1.Key,
                        UserTypeKey = item1.UserTypeKey,
                        CreateDate = item1.CreateDate,
                        UpdateDate = item1.UpdateDate,
                        Deis = item1.Deis,
                        UserTypeName = userTypeName == null ? "" : userTypeName.Name
                    });
                }

                approval = approval
                           .OrderByDescending(m => m.ApprovalId)
                           .OrderByDescending(m => m.ApprovalKey)
                           .OrderBy(m => m.Deis)
                           .ToList();

                var relatedKey = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.ApprovalKey == item);

                list.Add(new ApprovalListOutputDto
                {
                    ApprovalKey = item,
                    RelatedKey = relatedKey == null ? "" : relatedKey.RelatedKey,
                    Approval = approval
                });

            }

            var count = list.Count;

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
        /// 添加审核人
        /// </summary>
        /// <param name="depertment_User"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AuditPerson(Depertment_User depertment_User)
        {

            try
            {
                if (depertment_User !=null)
             {
                    List<long?> vs = new List<long?>();
                    foreach (var item in depertment_User.user_id)
                    {
                        var res = db.Departent_Users.Where(p => p.Department_Id == depertment_User.id && p.UserDetails_Id == item);
                        if (res!=null)
                        {
                       foreach (var items in res)
                        {
                                vs.Add(items.UserDetails_Id);
                        }
                        }
                     
                       
                    }


                    foreach (var item in depertment_User.user_id)
                    {

                        if (vs.Contains(item))
                        {
                            continue;
                        } 
                            if ( !vs.Contains(item))
                            {
                                Departent_User departent = new Departent_User()
                                {
                                    Id = IdentityManager.NewId(),
                                    Department_Id = depertment_User.id,
                                    CreateDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
                                    UserDetails_Id = item
                                };
                                db.Departent_Users.Add(departent);
                            }
                       

                    }


                    await db.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                else
                {
                    return Json(new { code = 200,msg="添加字段为空" });
                }
           

            }
            catch (Exception)
            {

                throw;
            }
         
          

          
          
        }
      
        /// <summary>
        ///获取自己部门的内容审批
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetContentFlow(int PageSize, int PageIndex)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                if (userId != null && PageSize != null && PageIndex != null)
                {
                    /* orderby p.CreateDate  select p).Skip((PageIndex - 1) * PageSize).Take(PageSize));*/
                    var result = await Task.Run(() => (from p in db.Purchases.Where(p => p.User_Id == userId)
                      orderby p.CreateDate
                    select new PurchaseOutputDto
                    {
                        PurchaseId = (p.Id).ToString(),
                        Project = p.Project,
                        ProjectId = (p.ProjectId).ToString(),
                        Price = p.Price,
                         PurchaseContract = p.PurchaseContract,
                        PurchaseTime = p.PurchaseTime,
                        Purpose = p.Purpose,
                        ApprovalDesc=p.ApprovalDesc,
                        QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                      //  z_Raw = p.Z_Raw,
                      //  RawId = (p.RawId).ToString(),
                        Supplier = p.Supplier,
                        SupplierId = (p.SupplierId).ToString(),
                        WaybillNumber = p.WaybillNumber
                    }).Skip((PageIndex * PageSize) - PageSize).Take(PageSize).ToList());
                    return Json(new { code = 200, data= result });/*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
               }
                else
                {
                    return Json(new { code = 200, mesage = "获取失败" });
                }
               // var Counts = db.Purchases.Count();
                

            }
            catch (Exception ex)
            {

                throw ex;
            }



          
        }
       
    }
}
