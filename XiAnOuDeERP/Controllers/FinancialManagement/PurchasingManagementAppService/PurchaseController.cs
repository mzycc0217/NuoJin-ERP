using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using Newtonsoft.Json;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Dto.My_FlowDto;

namespace XiAnOuDeERP.FinancialManagement.Controllers.UserAppService
{
    /// <summary>
    /// 采购申请单服务
    /// </summary>
    [AppAuthentication]
    public class PurchaseController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加采购申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(PurchaseInputDto input)
        {  
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                var amount = decimal.Zero;

                if (input.Price != null && input.QuasiPurchaseNumber != null && input.Price != 0 && input.QuasiPurchaseNumber != 0)
                {
                    amount = (decimal)input.Price * (decimal)input.QuasiPurchaseNumber;
                }

                if (input.User_id!=null)
                {
                    var result =await Task.Run(()=> db.Z_Raw.SingleOrDefaultAsync(p => p.Id == input.RawId));
                    int? is_or = null;
                    if (result.Number >= input.ApplyNumber)
                    {
                        //可以直接领取
                        is_or = 1;
                    }
                    if (result.Number < input.ApplyNumber)
                    {
                        //不可以直接领取
                        is_or = 2;
                    }


                    var data = new Purchase()
                {
                    Id = IdentityManager.NewId(),
                    Amount = amount,
                    ApplicantId = userId,
                    ApplicantRemarks = input.ApplicantRemarks,
                    ApplyNumber = input.ApplyNumber,
                    ApplyTime = input.ApplyTime,
                    ApprovalType = EApprovalType.UnderReview,
                    ArrivalTime = input.ArrivalTime,
                    SupplierId = input.SupplierId,
                    Enclosure = input.Enclosure,
                    Price = input.Price,
                  
                    PurchaseContract = input.PurchaseContract,
                    PurchaseTime = input.PurchaseTime,
                    Purpose = input.Purpose,
                    QuasiPurchaseNumber = input.QuasiPurchaseNumber,
                    RawId = input.RawId,
                    WaybillNumber = input.WaybillNumber,
                    ProjectId = (long)input.ProjectId,
                    ApprovalDesc = input.ApprovalDesc,
                    ExpectArrivalTime = input.ExpectArrivalTime,
                    IsDelete = false,
                    User_Id = input.User_id,
                    is_or= (int)is_or
                        // ApprovalKey = related.ApprovalKey,
                        // ApprovalIndex = 0
                    };

                db.Purchases.Add(data);
                }
                if (input.User_id == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请填写必要字段" }))
                    });
                }
                if ( await db.SaveChangesAsync()>0)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加成功" }))
                    });
                };
            }
            catch (Exception)
            {

                throw;
            }
         

            //var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "Purchase");

            //if (related == null)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage()
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "采购申请未绑定审批流，添加失败" }))
            //    });
            //}

            //var approval = await db.Approvals.Where(m => m.Key == related.ApprovalKey).ToListAsync();

            //if (approval == null && approval.Count <= 0)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage()
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = related.ApprovalKey + "该审批流不存在，添加失败" }))
            //    });
            //}

          

           
            //var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == data.ApprovalKey);

            //if (userTypeKey != null)
            //{
            //    db.PurchaseApprovals.Add(new PurchaseApproval
            //    {
            //        Id = IdentityManager.NewId(),
            //        PurchaseId = data.Id,
            //        IsApproval = false,
            //        UserTypeKey = userTypeKey.UserTypeKey,
            //        ApprovalIndex = 1
            //    });
            //}
            //else
            //{
            //    throw new HttpResponseException(new HttpResponseMessage()
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流异常" }))
            //    });
            //}

            //if (await db.SaveChangesAsync() <= 0)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage()
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
            //    });
            //}
        }


        /// <summary>
        /// 签核内容
        /// </summary>
        /// <param name="conten_UserDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Addaudit( Conten_UserDto conten_UserDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                if (conten_UserDto !=null)
                {
                Pursh_User content_User = new Pursh_User()
                {
                    Id = IdentityManager.NewId(),
                    user_Id = userId,
                    Purchase_Id = conten_UserDto.Id,
                    //备注
                    ContentDes = conten_UserDto.desc
                };
                db.Pursh_User.Add(content_User);
                   var res = db.Purchases.Where(s => s.Id == conten_UserDto.Id).FirstOrDefault();
                   res.is_or = 3;
                    if (await db.SaveChangesAsync() >0)
                    {
                        return Json(new { code = 200 ,msg="更新成功"});
                    }  ;

                    return Json(new { code = 200 , msg = "更新失败" });
                };
                return Json(new { code = 400 });
              
            }
            catch (Exception)
            {

                throw;
            }


        
        }

       /// <summary>
       /// 采购部门获取采购单
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> GetContent(InputBase input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            try
            {
                if(input != null)
                {
             var result = await Task.Run(() => (from s in db.Purchases.Where(s => s.is_or == 3) 
                                             orderby s.Id select s).Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize));
                return Json(new { code = 200,data=result});

                }
                return Json(new { code = 400 });
             

            }
            catch (Exception)
            {

                throw;
            }



        }

        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddSupplier(SupplierInputDto input)
        {
            var data = await db.Suppliers.SingleOrDefaultAsync(m => m.Name == input.Name && !m.IsDelete);

            if (data != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该供应商已存在" }))
                });
            }

            db.Suppliers.Add(new Supplier()
            {
                Id = IdentityManager.NewId(),
                Address = input.Address,
                PhoneNumber = input.PhoneNumber,
                Name = input.Name,
                IsDelete = false,
                RawMaterialId = input.RawMaterialId,
                Price = input.Price
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
        /// 修改供应商信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateSupplier(SupplierInputDto input)
        {
            var Supplier = await db.Suppliers.SingleOrDefaultAsync(m => m.Id == input.SupplierId && !m.IsDelete);

            if (Supplier == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该供应商不存在" }))
                });
            }

            Supplier.Name = input.Name;
            Supplier.PhoneNumber = input.PhoneNumber;
            Supplier.Address = input.Address;
            Supplier.Price = input.Price;
            Supplier.RawMaterialId = input.RawMaterialId;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除供应商信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteSupplierList(List<long> SupplierIds)
        {
            var list = new List<string>();

            foreach (var item in SupplierIds)
            {
                var supplier = await db.Suppliers.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (supplier != null)
                {
                    supplier.IsDelete = true;
                }
                else
                {
                    list.Add("【" + item + "】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 添加评分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddScore(ScoreInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            if (!await db.Suppliers.AnyAsync(m => m.Id == input.SupplierId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该供应商不存在" }))
                });
            }

            db.Scores.Add(new Score()
            {
                Id = IdentityManager.NewId(),
                Fraction = input.Fraction,
                SupplierId = input.SupplierId,
                AddbyId = userId
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
        /// 更新评分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateScore(ScoreInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var data = await db.Scores.SingleOrDefaultAsync(m => m.Id == input.ScoreId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "评分信息不存在" }))
                });
            }

            if (!await db.Suppliers.AnyAsync(m => m.Id == input.SupplierId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "供应商不存在" }))
                });
            }

            data.Fraction = input.Fraction;
            data.SupplierId = input.SupplierId;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除评分
        /// </summary>
        /// <param name="ScoreIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteScoreList(List<long> ScoreIds)
        {
            var list = new List<string>();

            foreach (var item in ScoreIds)
            {
                var data = await db.Scores.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }

            return list;
        }

        /// <summary>
        /// 获取评分列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ScoreOutputDto>> GetScoreList(GetScoreInputDto input)
        {
            var data = await db.Scores
                .Include(m => m.Addby)
                .Include(m => m.Supplier)
                .Where(m => !m.IsDelete).ToListAsync();

            if (input.SupplierId != null)
            {
                data = data.Where(m => m.SupplierId == input.SupplierId).ToList();
            }

            if (input.AddbyId != null)
            {
                data = data.Where(m => m.AddbyId == input.AddbyId).ToList();
            }

            if (input.ScoreId != null)
            {
                data = data.Where(m => m.Id == input.ScoreId).ToList();
            }

            var list = new List<ScoreOutputDto>();

            foreach (var item in data)
            {
                list.Add(new ScoreOutputDto()
                {
                    SupplierId = item.SupplierId.ToString(),
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    AddbyId = item.AddbyId.ToString(),
                    ScoreId = item.Id.ToString(),
                    Addby = item.Addby,
                    Fraction = item.Fraction,
                    Supplier = item.Supplier
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.SupplierId)
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
        /// 更新采购申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(PurchaseInputDto input)
        {
            var data = await db.Purchases.SingleOrDefaultAsync(m => m.Id == input.PurchaseId && !m.IsDelete && m.ApprovalType == EApprovalType.UnderReview);

            var amount = decimal.Zero;

            if (input.Price != null && input.QuasiPurchaseNumber != null && input.Price != 0 && input.QuasiPurchaseNumber != 0)
            {
                amount = (decimal)input.Price * (decimal)input.QuasiPurchaseNumber;
            }

            if (data != null)
            {
                data.Amount = amount;
                data.ApplicantRemarks = input.ApplicantRemarks;
                data.ApplyNumber = input.ApplyNumber;
                data.ApplyTime = input.ApplyTime;
                data.ArrivalTime = input.ArrivalTime;
                data.SupplierId = input.SupplierId;
                data.Enclosure = input.Enclosure;
                data.Price = input.Price;
                data.PurchaseContract = input.PurchaseContract;
                data.PurchaseTime = input.PurchaseTime;
                data.Purpose = input.Purpose;
                data.QuasiPurchaseNumber = input.QuasiPurchaseNumber;
                data.RawMaterialId = input.RawMaterialId;
                data.WaybillNumber = input.WaybillNumber;
                data.ProjectId = (long)input.ProjectId;
                data.ApprovalDesc = input.ApprovalDesc;
                data.ExpectArrivalTime = input.ExpectArrivalTime;
                //data.DepartmentLeaderId = input.DepartmentLeaderId;
                //data.GeneralManagerId = input.GeneralManagerId;
                //data.PurchasingSpecialistId = input.PurchasingSpecialistId;
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
        /// 更新采购申请单审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdatePurchaseApprovalType(PurchaseInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = await db.UserDetailsTypes.Where(m => m.UserId == userId).Select(m => m.UserType.Key).ToListAsync();

            var user = await db.UserDetails.Include(m => m.User).SingleOrDefaultAsync(m => m.Id == userId && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "无权操作权限" }))
                });
            }

            var data = await db.Purchases
                .Include(m => m.Applicant)
                .Include(m => m.Applicant.User)
                .SingleOrDefaultAsync(m => m.Id == input.PurchaseId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该订单不存在" }))
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

                    //var purchaseApproval = await db.PurchaseApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.PurchaseId == data.Id);
                    var purchaseApproval = await db.PurchaseApprovals.SingleOrDefaultAsync(m => userTypeKeys.Contains(m.UserTypeKey) && m.ApprovalIndex == index && m.PurchaseId == data.Id);

                    if (purchaseApproval == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
                        });
                    }

                    purchaseApproval.IsApproval = true;
                    purchaseApproval.UserId = token.UserId;

                    var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

                    if (userTypeKey != null)
                    {
                        db.PurchaseApprovals.Add(new PurchaseApproval
                        {
                            Id = IdentityManager.NewId(),
                            PurchaseId = data.Id,
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
                if (input.ApprovalType == EApprovalType.Rejected || input.ApprovalType == EApprovalType.Paid)
                {
                    data.ApprovalType = input.ApprovalType;
                }

                if (data.ApprovalType == EApprovalType.Paid)
                {
                    db.AssetExpenditures.Add(new AssetExpenditure
                    {
                        Id = IdentityManager.NewId(),
                        Desc = input.AssetExpenditureDesc,
                        PurchaseId = data.Id,
                        UserId = userId,
                        Amount = (decimal)data.Amount
                    });
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
        /// 批量删除采购
        /// </summary>
        /// <param name="PurchaseIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> PurchaseIds)
        {
            var list = new List<string>();
            foreach (var item in PurchaseIds)
            {
                var data = await db.Purchases.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取采购申请列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<PurchaseOutputDto>> GetPurchaseList(GetPurchaseListInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            //var userTypeKeys = await db.UserDetailsTypes.Where(m => m.UserId == userId).Select(m => m.UserType.Key).ToListAsync();
            var userTypeKeys = token.UserTypeKey;

            var data = await db.Purchases
                .Include(m => m.RawMaterial.Company)
                .Include(m => m.Applicant)
                .Include(m => m.Applicant.User)
                .Include(m => m.Applicant.User.Department)
                .Include(m => m.RawMaterial)
                .Include(m => m.Project)
                .Include(m => m.Supplier)
                .Where(m => !m.IsDelete)
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            if (input.IsApproval)
            {
                var purchase = await db.PurchaseApprovals
                    .Include(m => m.Purchase)
                    .Include(m => m.Purchase.RawMaterial.Company)
                    .Include(m => m.Purchase.Applicant)
                    .Include(m => m.Purchase.Applicant.User)
                    .Include(m => m.Purchase.Applicant.User.Department)
                    .Include(m => m.Purchase.RawMaterial)
                    .Include(m => m.Purchase.Project)
                    .Include(m => m.Purchase.Supplier)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.Purchase.IsDelete && m.Purchase.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.Purchase.IsDelete && m.Purchase.ApprovalType != EApprovalType.Rejected)
                    .Select(m => m.Purchase)
                    .ToListAsync();

                data = purchase;
            }

            if (input.UserId != null)
            {
                var data1 = await db.PurchaseApprovals
                    .Include(m => m.Purchase)
                    .Include(m => m.Purchase.RawMaterial.Company)
                    .Include(m => m.Purchase.Applicant)
                    .Include(m => m.Purchase.Applicant.User)
                    .Include(m => m.Purchase.Applicant.User.Department)
                    .Include(m => m.Purchase.RawMaterial)
                    .Include(m => m.Purchase.Project)
                    .Include(m => m.Purchase.Supplier)
                    .Where(m => m.UserId == input.UserId && !m.Purchase.IsDelete && m.Purchase.ApprovalType == EApprovalType.Paid && m.IsApproval).ToListAsync();

                data = data1.Select(m => m.Purchase).ToList();
            }

            if (input.ApprovalType != EApprovalType.No && input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            if (input.PurchaseId != null)
            {
                data = data.Where(m => m.Id == input.PurchaseId).ToList();
            }

            if (input.RawMaterialId != null)
            {
                data = data.Where(m => m.RawMaterialId == input.RawMaterialId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RawMaterialName))
            {
                data = data.Where(m => m.RawMaterial.Name != null && m.RawMaterial.Name.Contains(input.RawMaterialName)).ToList();
            }

            if (input.SupplierId != null)
            {
                data = data.Where(m => m.SupplierId == input.SupplierId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.SupplierName))
            {
                data = data.Where(m => m.Supplier.Name != null && m.Supplier.Name.Contains(input.SupplierName)).ToList();
            }

            if (input.WarehousingTypeId != null)
            {
                data = data.Where(m => m.RawMaterial.WarehousingTypeId == input.WarehousingTypeId).ToList();
            }

            if (input.ApplicantId != null)
            {
                data = data.Where(m => m.ApplicantId == input.ApplicantId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.ApplicantName))
            {
                data = data.Where(m => m.Applicant.RealName != null && m.Applicant.RealName.Contains(input.ApplicantName)).ToList();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.Applicant.User.DepartmentId == input.DepartmentId).ToList();
            }

            var list = new List<PurchaseOutputDto>();

            foreach (var item in data)
            {
                list.Add(new PurchaseOutputDto()
                {
                    PurchaseId = item.Id.ToString(),
                    Amount = item.Amount,
                    ApplicantRemarks = item.ApplicantRemarks,
                    ApplyNumber = item.ApplyNumber,
                    ApplyTime = item.ApplyTime,
                    ApprovalType = item.ApprovalType,
                    ApprovalTypeStr = item.ApprovalType.GetDescription(),
                    ArrivalTime = item.ArrivalTime,
                    SupplierId =
                    item.SupplierId.ToString(),
                    Supplier = item.Supplier,
                    CompanyId = item.RawMaterial.Company.ToString(),
                    Company = item.RawMaterial.Company,
                    Enclosure = item.Enclosure,
                    Price = item.Price,
                    PurchaseContract = item.PurchaseContract,
                    PurchaseTime = item.PurchaseTime,
                    Purpose = item.Purpose,
                    QuasiPurchaseNumber = item.QuasiPurchaseNumber,
                    WaybillNumber = item.WaybillNumber,
                    ApplicantId = item.ApplicantId.ToString(),
                    Applicant = item.Applicant,
                    ApprovalDesc = item.ApprovalDesc,
                    ExpectArrivalTime = item.ExpectArrivalTime,
                    ProjectId = item.ProjectId.ToString(),
                    Project = item.Project,
                    RawMaterialId = item.RawMaterialId.ToString(),
                    RawMaterial = item.RawMaterial,
                    DepartmentId = item.Applicant.User.DepartmentId.ToString(),
                    Department = item.Applicant.User.Department,
                    UpdateDate = item.UpdateDate,
                    CreateDate = item.CreateDate,
                    WarehousingType = item.RawMaterial.WarehousingType,
                    WarehousingTypeId = item.RawMaterial.WarehousingTypeId.ToString()
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.PurchaseId)
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
        /// 获取供应商列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SupplierOutputDto>> GetSupplierList(GetSupplierInputDto input)
        {
            var data = await db.Suppliers.Where(m => !m.IsDelete).ToListAsync();

            if (input.SupplierId != null)
            {
                data = data.Where(m => m.Id == input.SupplierId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.SupplierName))
            {
                data = data.Where(m => m.Name == input.SupplierName).ToList();
            }

            if (input.RawMaterialId != null)
            {
                data = data.Where(m => m.RawMaterialId == input.RawMaterialId).ToList();
            }

            var list = new List<SupplierOutputDto>();

            foreach (var item in data)
            {
                var Fractions = await db.Scores.Where(m => m.SupplierId == item.Id && !m.IsDelete).Select(m => m.Fraction).ToListAsync();

                double Fraction = 0;

                if (Fractions != null && Fractions.Count > 0)
                {
                    Fraction = Fractions.Average(m => m);
                }

                list.Add(new SupplierOutputDto()
                {
                    SupplierId = item.Id.ToString(),
                    Name = item.Name,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    Fraction = (int)Fraction,
                    RawMaterialId = item.RawMaterialId.ToString(),
                    Price = item.Price,
                    RawMaterial = new Models.Dto.OutputDto.FinancialManagement.RawMaterialDto.RawMaterialOutputDto
                    {
                        RawMaterialId = item.RawMaterialId.ToString(),
                        Abbreviation = item.RawMaterial.Abbreviation,
                        AppearanceState = item.RawMaterial.AppearanceState,
                        BeCommonlyCalled1 = item.RawMaterial.BeCommonlyCalled1,
                        BeCommonlyCalled2 = item.RawMaterial.BeCommonlyCalled2,
                        CASNumber = item.RawMaterial.CASNumber,
                        Company = item.RawMaterial.Company,
                        CompanyId = item.RawMaterial.CompanyId.ToString(),
                        CreateDate = item.RawMaterial.CreateDate,
                        EnglishName = item.RawMaterial.EnglishName,
                        EntryPerson = item.RawMaterial.EntryPerson,
                        EntryPersonId = item.RawMaterial.EntryPersonId.ToString(),
                        MolecularFormula = item.RawMaterial.MolecularFormula,
                        MolecularWeight = item.RawMaterial.MolecularWeight,
                        Name = item.RawMaterial.Name,
                        RawMaterialType = item.RawMaterial.RawMaterialType,
                        StructuralFormula = item.RawMaterial.StructuralFormula,
                        UpdateDate = item.RawMaterial.UpdateDate,
                        WarehousingType = item.RawMaterial.WarehousingType,
                        WarehousingTypeId = item.RawMaterial.WarehousingTypeId.ToString()
                    }
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.SupplierId)
                .OrderBy(m=>m.Price)
                .OrderByDescending(m=>m.Fraction)
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