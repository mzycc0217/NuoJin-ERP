using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt.Outenport;
using XiAnOuDeERP.Models.Dto.Monad;
using XiAnOuDeERP.Models.Dto.Monad.MonadOut;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.InEntrepot
{
    /// <summary>
    /// 产成品入库
    /// </summary>

    [AppAuthentication]
    [RoutePrefix("api/ FinshedProductMonad")]
    public class FinshedProductMonadController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        private int Is_Or { get; set; }
        /// <summary>
        /// 添加产成品
        /// </summary>
        /// <param name="finshProdutMonadIn"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> FinshedProductMonadMonad(FinshProdutMonadInDto finshProdutMonadIn)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                //    var amount = decimal.Zero;

                //    if (finshProdutMonadIn.Price != null && finshProdutMonadIn.QuasiPurchaseNumber != null && finshProdutMonadIn.Price != 0 && finshProdutMonadIn.QuasiPurchaseNumber != 0)
                //    {
                //        amount = (decimal)finshProdutMonadIn.Price * (decimal)finshProdutMonadIn.QuasiPurchaseNumber;
                //    }

                //    if (finshProdutMonadIn.finshProdutMonadIn != null)
                //    {
                //        var result = await Task.Run(() => db.Offices.AsNoTracking().Where(p => p.OfficeId == finshProdutMonadIn.OfficeId));


                //        foreach (var item in result)
                //        {

                //            if (item.RawNumber >= finshProdutMonadIn.ApplyNumber)
                //            {
                //                //可以直接领取
                //                this.Is_Or = 1;
                //                break;

                //            }
                //            else
                //            {
                //                //不可以直接领取
                //                this.Is_Or = 2;
                //                break;
                //            }
                //        }


                FnishedProductMonad fnishedProductMonad = new FnishedProductMonad()
                    {
                        Id = IdentityManager.NewId(),
                        //Amount = amount,
                        ApplicantId = userId,
                        ApplicantRemarks = finshProdutMonadIn.ApplicantRemarks,
                        ApplyNumber = finshProdutMonadIn.ApplyNumber,
                        ApplyTime = finshProdutMonadIn.ApplyTime,

                        ArrivalTime = finshProdutMonadIn.ArrivalTime,
                      //  SupplierId = finshProdutMonadIn.SupplierId,
                        Enclosure = finshProdutMonadIn.Enclosure,
                       //  Finshed_Sign= finshProdutMonadIn.Finshed_Sign,

                        PurchaseContract = finshProdutMonadIn.PurchaseContract,
                        PurchaseTime = finshProdutMonadIn.PurchaseTime,
                        Purpose = finshProdutMonadIn.Purpose,
                        QuasiPurchaseNumber = finshProdutMonadIn.QuasiPurchaseNumber,
                        FnishedProductId = finshProdutMonadIn.FinshProdutId,
                        WaybillNumber = finshProdutMonadIn.WaybillNumber,
                        ExpectArrivalTime = finshProdutMonadIn.ExpectArrivalTime,
                        IsDelete = false,

                        is_or = 1,
                        // ApprovalKey = related.ApprovalKey,

                    };

                    db.FnishedProductMonad.Add(fnishedProductMonad);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "添加成功" });
                    }
                    if (await db.SaveChangesAsync() < 0)
                    {
                        return Json(new { code = 201, msg = "添加失败" });
                    }
                
                return Json(new { code = 201, msg = "系统错误，请联系管理员" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 库管员获取入库申请
        /// </summary>
        /// <param name="finshProdutMonadOut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> GetFinshedProductMonad(FinshProdutMonadOutDto finshProdutMonadOut)
        {   
                var result = await Task.Run(() => db.FnishedProductMonad.Where(p =>  p.IsDelete == false&&p.is_or == 1)
               .Select(p => new FinshProdutMonadOutDto
               {
                   Id = p.Id.ToString(),
                   Amount = p.Amount,
                   ApplicantRemarks = p.ApplicantRemarks,
                   ApplyNumber = p.ApplyNumber,
                   ApplyTime = p.ApplyTime,
                   // ApprovalType = p.ApprovalType,
                   z_FnishedProduct = p.Z_FnishedProduct,
                   ArrivalTime = p.ArrivalTime,
                   SupplierId = p.Supplier.Id.ToString(),
                   Supplier = p.Supplier,
                   FnishedProductId = p.Z_FnishedProduct.Id.ToString(),
                   Finshed_Sign = p.Z_FnishedProduct.Finshed_Sign,
                   Company = p.Z_FnishedProduct.Company,
                   Enclosure = p.Enclosure,
                   Price = p.Price,
                   PurchaseContract = p.PurchaseContract,
                   PurchaseTime = p.PurchaseTime,
                   Purpose = p.Purpose,
                   QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                   WaybillNumber = p.WaybillNumber,
                   ApplicantId = p.ApplicantId.ToString(),
                   Applicant = p.Applicant,

                   ExpectArrivalTime = p.ExpectArrivalTime,
               })
              );
               // return Json(new { code = 200, data = result, Count = result.Count() });
            
            //获取所有采购申请单
            if (!string.IsNullOrWhiteSpace(finshProdutMonadOut.RelName))
            {
                result = await Task.Run(() => result.Where(p => p.Applicant.RealName.Contains(finshProdutMonadOut.RelName))
              .OrderBy(p => p.Id)
                .Skip((finshProdutMonadOut.PageIndex * finshProdutMonadOut.PageSize) - finshProdutMonadOut.PageSize).Take(finshProdutMonadOut.PageSize));
                return Json(new { code = 200, data =await result.ToListAsync(), Count = result.Count() });
            }
            if (finshProdutMonadOut.PageIndex!=null&& finshProdutMonadOut.PageSize != null)
            {
                result = await Task.Run(() => result.Where(p=>p.Id!=null)
              .OrderBy(p => p.Id)
              .Skip((finshProdutMonadOut.PageIndex * finshProdutMonadOut.PageSize) - finshProdutMonadOut.PageSize).Take(finshProdutMonadOut.PageSize));
            }

            return Json(new { code = 200, data =await result.ToListAsync(), Count = result.Count() });
        }

        /// <summary>
        /// 添加入库
        /// </summary>
        /// <param name="finshProduct_User"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddFinshedProductMonad(FinshProduct_UserIn finshProduct_User)
        {
            try
            {
                // FinishProductId
                if (finshProduct_User.FinishProductId != null)
                {
                    var result = await db.FnishedProductMonad.SingleOrDefaultAsync(p => p.Id == finshProduct_User.FinishProductId && p.IsDelete == false);
                    //修改采购单
                    result.is_or = 2;//添加入库
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(result).Property("is_or").IsModified = true;
                    var results = await Task.Run(() => db.FnishedProductRooms
                    .FirstOrDefaultAsync(p => p.FnishedProductId == result.FnishedProductId && p.del_or == false)
                    );

                    if (finshProduct_User.enportid != 0)
                    {
                        results = await Task.Run(() => db.FnishedProductRooms.FirstOrDefaultAsync(p => p.EntrepotId == finshProduct_User.enportid));
                    }

                    if ((double)result.ApplyNumber != null)
                    {
                        results.RawNumber = results.RawNumber + (double)result.ApplyNumber;
                        db.Entry(results).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(results).Property("RawNumber").IsModified = true;
                    }
                    //if (results == null)
                    //{
                
                          FnishedProduct_UserDetils fnishedProduct_User = new FnishedProduct_UserDetils
                        {
                            Id = IdentityManager.NewId(),
                            FnishedProductId = (long)result.Z_FnishedProduct.Id,
                            FnishedProductNumbers = (double)result.ApplyNumber,

                            User_id= (long)result.Applicant.Id,
                              entrepotid = results.EntrepotId

                        };
                    db.FnishedProduct_UserDetils.Add(fnishedProduct_User);
                 
                    
                      
                    //}
                    //if (results != null)
                    //{



                    //   }





                    if (await db.SaveChangesAsync() > 0)
                    {



                        return Json(new { code = 200, msg = "入库成功" });
                    }
                    return Json(new { code = 201, msg = "入库失败" });
                }

                return Json(new { code = 200, msg = "入库失败" });


            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取已经完成入库的申请
        /// </summary>
        /// <param name="finshProdutMonadOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetFinshedProductMonads(FinshProdutMonadOutDto finshProdutMonadOut)
        {
            var result = await Task.Run(() => db.FnishedProductMonad.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false)
               .Select(p => new FinshProdutMonadOutDto
               {
                   Id = p.Id.ToString(),
                   Amount = p.Amount,
                   ApplicantRemarks = p.ApplicantRemarks,
                   ApplyNumber = p.ApplyNumber,
                   ApplyTime = p.ApplyTime,
                   // ApprovalType = p.ApprovalType,
                   z_FnishedProduct = p.Z_FnishedProduct,
                   ArrivalTime = p.ArrivalTime,
                   SupplierId = p.Supplier.Id.ToString(),
                   Supplier = p.Supplier,
                   FnishedProductId = p.Z_FnishedProduct.Id.ToString(),
                   Finshed_Sign = p.Finshed_Sign,
                   Company = p.Z_FnishedProduct.Company,
                   Enclosure = p.Enclosure,
                   Price = p.Price,
                   PurchaseContract = p.PurchaseContract,
                   PurchaseTime = p.PurchaseTime,
                   Purpose = p.Purpose,
                   QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                   WaybillNumber = p.WaybillNumber,
                   ApplicantId = p.ApplicantId.ToString(),
                   Applicant = p.Applicant,

                   ExpectArrivalTime = p.ExpectArrivalTime,
               }));
              
            //获取所有采购申请单
            if (!string.IsNullOrWhiteSpace(finshProdutMonadOut.RelName))
            {
                 result = await Task.Run(() => result.Where(p =>  p.Applicant.RealName.Contains(finshProdutMonadOut.RelName))
                . OrderBy(p => p.Id)
                .Skip((finshProdutMonadOut.PageIndex * finshProdutMonadOut.PageSize) - finshProdutMonadOut.PageSize).Take(finshProdutMonadOut.PageSize));
                  return Json(new { code = 200, data = await result.ToListAsync(), Count = result.Count()});
            }

            if (finshProdutMonadOut.PageIndex != null && finshProdutMonadOut.PageSize != null)
            {
                result = await Task.Run(() => result.Where(p => p.Id != null)
              .OrderBy(p => p.Id)
              .Skip((finshProdutMonadOut.PageIndex * finshProdutMonadOut.PageSize) - finshProdutMonadOut.PageSize).Take(finshProdutMonadOut.PageSize));

                return Json(new { code = 200, data = await result.ToListAsync(), Count = result.Count() });
            }
            return Json(new { code = 200, data = await result.ToListAsync(), Count = result.Count()});
              
            
          
        }

    
    
    }
}
