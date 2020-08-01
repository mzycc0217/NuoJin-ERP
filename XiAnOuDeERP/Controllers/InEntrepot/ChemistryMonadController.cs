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
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Dto.Monad;
using XiAnOuDeERP.Models.Dto.Monad.MonadOut;
using XiAnOuDeERP.Models.Dto.My_FlowDto;
using XiAnOuDeERP.Models.Dto.OutPutDecimal.InDto;
using XiAnOuDeERP.Models.Dto.OutPutDecimal.OutDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.InEntrepot
{
    [AppAuthentication]
    [RoutePrefix("api/ChemistryMonad")]

    public class ChemistryMonadController : ApiController
    {

        XiAnOuDeContext db = new XiAnOuDeContext();
        
        
        private int Is_Or { get; set; }
        /// <summary>
        /// 添加化学用品单
        /// </summary>
        /// <param name="chemistryMonadInDto"></param>
        /// <returns></returns>
       
        [HttpPost]
        public async Task<IHttpActionResult> SetAddChemistryMonad(ChemistryMonadInDto chemistryMonadInDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                var amount = decimal.Zero;

                if (chemistryMonadInDto.Price != null && chemistryMonadInDto.QuasiPurchaseNumber != null && chemistryMonadInDto.Price != 0 && chemistryMonadInDto.QuasiPurchaseNumber != 0)
                {
                    amount = (decimal)chemistryMonadInDto.Price * (decimal)chemistryMonadInDto.QuasiPurchaseNumber;
                }

                if (chemistryMonadInDto.ChemistryId != null)
                {
                    var result = await Task.Run(() => db.ChemistryRooms.AsNoTracking().Where(p => p.ChemistryId == chemistryMonadInDto.ChemistryId));
                  

                    foreach (var item in result)
                    {
                        
                        if (item.RawNumber >= chemistryMonadInDto.ApplyNumber)
                        {
                            //可以直接领取
                            this.Is_Or = 1;
                            break;

                        }
                        else
                        {
                            //不可以直接领取
                            this.Is_Or = 2;
                            break;
                        }
                    }


                    ChemistryMonad chemistryMonad = new ChemistryMonad()
                    {
                        Id = IdentityManager.NewId(),
                        Amount = amount,
                        ApplicantId = userId,
                        ApplicantRemarks = chemistryMonadInDto.ApplicantRemarks,
                        ApplyNumber = chemistryMonadInDto.ApplyNumber,
                        ApplyTime = chemistryMonadInDto.ApplyTime,
                       
                        ArrivalTime = chemistryMonadInDto.ArrivalTime,
                        SupplierId = chemistryMonadInDto.SupplierId,
                        Enclosure = chemistryMonadInDto.Enclosure,
                        Price = chemistryMonadInDto.Price,

                        PurchaseContract = chemistryMonadInDto.PurchaseContract,
                        PurchaseTime = chemistryMonadInDto.PurchaseTime,
                        Purpose = chemistryMonadInDto.Purpose,
                        QuasiPurchaseNumber = chemistryMonadInDto.ApplyNumber,
                        ChemistryId = chemistryMonadInDto.ChemistryId,
                        WaybillNumber = chemistryMonadInDto.WaybillNumber,
                        ExpectArrivalTime = chemistryMonadInDto.ExpectArrivalTime,
                        IsDelete = false,

                        is_or = this.Is_Or,
                        // ApprovalKey = related.ApprovalKey,

                    };

                    db.ChemistryMonad.Add(chemistryMonad);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "添加成功" });
                    }
                    if (await db.SaveChangesAsync() < 0)
                    {
                        return Json(new { code = 201, msg = "添加失败" });
                    }
                }
                return Json(new { code = 201, msg = "系统错误，请联系管理员" });
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// 库管员获取可以直接领取的化学用品(获取)
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetsChemistryMonad(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            //  获取所有采购申请单chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null
            //if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null || !string.IsNullOrWhiteSpace(chemistryMonadIOut.Applicant.RealName))
            //{
            //    var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false || p.Applicant.RealName.Contains(chemistryMonadIOut.Applicant.RealName))
            //    .Select(p => new ChemistryMonadIOutDto
            //    {
            //        Id = p.Id.ToString(),
            //        Amount = p.Amount,
            //        ApplicantRemarks = p.ApplicantRemarks,
            //        ApplyNumber = p.ApplyNumber,
            //        ApplyTime = p.ApplyTime,
            //        // ApprovalType = p.ApprovalType,
            //        //  AssetExpenditureDesc = p.AssetExpenditureDesc,
            //        ArrivalTime = p.ArrivalTime,
            //        SupplierId = p.Supplier.Id.ToString(),
            //        Supplier = p.Supplier,
            //        ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
            //        Company = p.Z_Chemistry.Company,
            //        Z_Chemistry = p.Z_Chemistry,
            //        Enclosure = p.Enclosure,
            //        Price = p.Price,
            //        PurchaseContract = p.PurchaseContract,
            //        PurchaseTime = p.PurchaseTime,
            //        Purpose = p.Purpose,
            //        QuasiPurchaseNumber = p.QuasiPurchaseNumber,
            //        WaybillNumber = p.WaybillNumber,
            //        ApplicantId = p.ApplicantId.ToString(),
            //        Applicant = p.Applicant,

            //        ExpectArrivalTime = p.ExpectArrivalTime,
            //    }).OrderBy(p => p.Id)
            //    .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
            //     return Json(new { code = 200, data = result, Count = result.Count() });
            // }

            var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false)
                 .Select(p => new ChemistryMonadIOutDto
                 {
                     Id = p.Id.ToString(),
                     Amount = p.Amount,
                     ApplicantRemarks = p.ApplicantRemarks,
                     ApplyNumber = p.ApplyNumber,
                     ApplyTime = p.ApplyTime,
                     // ApprovalType = p.ApprovalType,
                     //  AssetExpenditureDesc = p.AssetExpenditureDesc,
                     ArrivalTime = p.ArrivalTime,
                     SupplierId = p.Supplier.Id.ToString(),
                     Supplier = p.Supplier,
                     ChemistryId = p.Z_Chemistry.Id.ToString(),
                     Company = p.Z_Chemistry.Company,
                     Z_Chemistry = p.Z_Chemistry,
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
                 }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize));
                
            
            if (!string.IsNullOrWhiteSpace(chemistryMonadIOut.RelName))
            {
              result=await Task.Run(()=> result.Where(p => p.Applicant.RealName.Contains(chemistryMonadIOut.RelName)));

                return Json(new { code = 200, data =await result.ToListAsync(), Count = result.Count() });
            }
            else
            {
                return Json(new { code = 200, data = await result.ToListAsync(), Count = result.Count() });
            }

           
        }

        /// <summary>
        /// 领导获取的部门的申请单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonads(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            //获取当前领导的部门
            var resule = await db.UserDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == userId);
            var resul = await db.User.AsNoTracking().FirstOrDefaultAsync(p => p.Id == resule.UserId);


            //获取所有采购申请单
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null && !string.IsNullOrWhiteSpace(chemistryMonadIOut.ApplicantRelName))
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false && p.Applicant.User.DepartmentId == resul.DepartmentId || p.Applicant.RealName.Contains(chemistryMonadIOut.ApplicantRelName))
                //  .Include(p=>p.Applicant.User.DepartmentId== resul.DepartmentId)
                .Select(p => new ChemistryMonadIOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    //  AssetExpenditureDesc = p.AssetExpenditureDesc,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                    Company = p.Z_Chemistry.Company,
                    Z_Chemistry=p.Z_Chemistry,
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
                }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, data = result, Count = result.Count() });
            }
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false && p.Applicant.User.DepartmentId == resul.DepartmentId)
                //   .Include(p => p.Applicant.User.DepartmentId == resul.DepartmentId)
                .Select(p => new ChemistryMonadIOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    Z_Chemistry = p.Z_Chemistry,
                    //  AssetExpenditureDesc = p.AssetExpenditureDesc,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                    Company = p.Z_Chemistry.Company,
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
                }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
                return Json(new { code = 200, data = result, Count = result.Count() });
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 库管员签核申请单
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetChemistryMonadds(Chemistry_UserInDto chemistry_UserIn)
        {

            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
             
                var resuls = await db.ChemistryMonad.SingleOrDefaultAsync(p => p.Id == chemistry_UserIn.ChemistryId);
                //添加领料
                long Enportid;//仓库id
                if (chemistry_UserIn.enportid == 0)
                {

                    var result = await Task.Run(() => db.ChemistryRooms
                    .Where(p => p.ChemistryId == resuls.ChemistryId && p.RawNumber >= resuls.QuasiPurchaseNumber).FirstOrDefaultAsync());
                    if (result==null)
                    {
                        resuls.is_or = 5;
                        await db.SaveChangesAsync();
                        return Json(new { code = 200, msg = "仓库数量不足,重新采购" });
                    }
                    Enportid = (long)result.EntrepotId;
                    result.RawNumber = result.RawNumber - (double)resuls.QuasiPurchaseNumber;
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(result).Property("RawNumber").IsModified = true;

                }
                else
                {
                    Enportid = chemistry_UserIn.enportid;
                    var ChemistryRoom = await Task.Run(() => db.ChemistryRooms
                    .FirstOrDefaultAsync(p => p.ChemistryId == resuls.ChemistryId && p.EntrepotId== Enportid));
                  //  ChemistryRoom ChemistryRoom = new ChemistryRoom { ChemistryId = (long)resuls.ChemistryId, EntrepotId = Enportid };
                    db.Entry(ChemistryRoom).State = System.Data.Entity.EntityState.Unchanged;
                    ChemistryRoom.RawNumber = ChemistryRoom.RawNumber - (double)resuls.QuasiPurchaseNumber;
                } 
                //var res = new ChemistryRoom { ChemistryId = (long)resuls.ChemistryId };
                //db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                //res.RawNumber = res.RawNumber - (double)resuls.QuasiPurchaseNumber;
                resuls.is_or = 0;
                db.Entry(resuls).State = System.Data.Entity.EntityState.Modified;
                db.Entry(resuls).Property("is_or").IsModified = true;


                Chemistry_UserDetils chemistry_UserDetils1 = new Chemistry_UserDetils
                    {
                        Id = IdentityManager.NewId(),
                        ChemistryId = (long)resuls.ChemistryId,
                        User_id = resuls.Applicant.Id,
                        OutIutRoom = 1,//出库状态,
                        is_or = 1,//显示为出库状态（个人获取到的）
                                  //添加仓库（这里）
                        entrepotid = Enportid,
                        ChemistryNumber = (double)resuls.QuasiPurchaseNumber,
                        GetTime = DateTime.Now
                    };
                    db.Chemistry_UserDetils.Add(chemistry_UserDetils1);  
                // 添加签核人
                Chmistry_User chmistry_User = new Chmistry_User
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = chemistry_UserIn.ChemistryId,
                    user_Id = userId,
                    ContentDes = chemistry_UserIn.ContentDes
                };
                db.Chmistry_Users.Add(chmistry_User);
                // RawId = resuls.RawId;
                //ApplyNumber = resuls.QuasiPurchaseNumber;

                //var resul = new Purchase { Id = content_Users.Purchase_Id };
                //db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;




             


                if (await db.SaveChangesAsync() > 0)
                {


                    return Json(new { code = 200, msg = "添加成功" });
                }
                else
                {
                    return Json(new { code = 400, msg = "添加失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 部门主管签核申请单
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Chmistry_User chmistry_User = new Chmistry_User
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = chemistry_UserIn.ChemistryId,
                    user_Id = userId,
                    ContentDes = chemistry_UserIn.ContentDes

                };

                db.Chmistry_Users.Add(chmistry_User);
                var resuls =await db.ChemistryMonad.SingleOrDefaultAsync(p => p.Id == chemistry_UserIn.ChemistryId);
                //foreach (var item in resuls)
                //{
                //    RawId = item.RawId;
                //    ApplyNumber = item.ApplyNumber;
                //}
                //var resul = new Purchase { Id = content_Users.Purchase_Id };
                //db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;




                //var res = new Z_Raw { Id = (long)RawId };
                //db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                //res.Number = res.Number - ApplyNumber;

                resuls.is_or = 5;
          
                if (await db.SaveChangesAsync() > 0)
                {


                    return Json(new { code = 200, msg = "添加成功" });
                }
                else
                {
                    return Json(new { code = 400, msg = "添加失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 采购专员采购部门获取申请单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadCaigou(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            //获取所有采购申请单
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null && !string.IsNullOrWhiteSpace(chemistryMonadIOut.ApplicantRelName))
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0&& p.is_or == 5 && p.IsDelete == false|| p.Applicant.RealName.Contains(chemistryMonadIOut.ApplicantRelName) )
                .Select(p => new ChemistryMonadIOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    Z_Chemistry = p.Z_Chemistry,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                    Company = p.Z_Chemistry.Company,
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
                }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 5 && p.IsDelete == false)
              .Select(p => new ChemistryMonadIOutDto
              {
                  Id = p.Id.ToString(),
                  Amount = p.Amount,
                  ApplicantRemarks = p.ApplicantRemarks,
                  ApplyNumber = p.ApplyNumber,
                  ApplyTime = p.ApplyTime,
                  // ApprovalType = p.ApprovalType,
                  Z_Chemistry = p.Z_Chemistry,
                  ArrivalTime = p.ArrivalTime,
                  SupplierId = p.Supplier.Id.ToString(),
                  Supplier = p.Supplier,
                  ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                  Company = p.Z_Chemistry.Company,
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
              }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }


        /// <summary>
        /// 获取完成的采购单（获取自己名下的所有采购单input.PageSize == -2input.PageIndex == -2）（获取自己名下的已经完成的所有采购单input.PageSize == -1nput.PageIndex == -1）
        ///获取自己名下未完成的采购单（获取自己名下的所有采购单input.PageSize == -3input.PageIndex == -3）
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadCaigoud(ChemistryMonadIOutDto chemistryMonadIOut)
        {

            //获取自己发布的申请单
            var userId = ((UserIdentity)User.Identity).UserId;
            if (chemistryMonadIOut.PageSize == -1 && chemistryMonadIOut.PageIndex == -1)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false)
        .Select(p => new ChemistryMonadIOutDto
        {
            Id = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            Z_Chemistry = p.Z_Chemistry,
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
            Company = p.Z_Chemistry.Company,
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
        }).OrderBy(p => p.Id)
        .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的所有采购单
            if (chemistryMonadIOut.PageSize == -2 && chemistryMonadIOut.PageIndex == -2)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId)
        .Select(p => new ChemistryMonadIOutDto
        {
            Id = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            Z_Chemistry = p.Z_Chemistry,
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
            Company = p.Z_Chemistry.Company,
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
        }).OrderBy(p => p.Id)
        .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的未完成的采购单
            if (chemistryMonadIOut.PageSize == -3 && chemistryMonadIOut.PageIndex == -3)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0)
         .Select(p => new ChemistryMonadIOutDto
         {
             Id = p.Id.ToString(),
             Amount = p.Amount,
             ApplicantRemarks = p.ApplicantRemarks,
             ApplyNumber = p.ApplyNumber,
             ApplyTime = p.ApplyTime,
             // ApprovalType = p.ApprovalType,
             Z_Chemistry = p.Z_Chemistry,
             ArrivalTime = p.ArrivalTime,
             SupplierId = p.Supplier.Id.ToString(),
             Supplier = p.Supplier,
             ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
             Company = p.Z_Chemistry.Company,
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
         }).OrderBy(p => p.Id)
        .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取所有采购申请单
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null && !string.IsNullOrWhiteSpace(chemistryMonadIOut.ApplicantRelName))
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.RealName.Contains(chemistryMonadIOut.ApplicantRelName) && p.is_or == 0)
             .Select(p => new ChemistryMonadIOutDto
             {
                 Id = p.Id.ToString(),
                 Amount = p.Amount,
                 ApplicantRemarks = p.ApplicantRemarks,
                 ApplyNumber = p.ApplyNumber,
                 ApplyTime = p.ApplyTime,
                 // ApprovalType = p.ApprovalType,
                 Z_Chemistry = p.Z_Chemistry,
                 ArrivalTime = p.ArrivalTime,
                 SupplierId = p.Supplier.Id.ToString(),
                 Supplier = p.Supplier,
                 ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                 Company = p.Z_Chemistry.Company,
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
             }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 0)
                .Select(p => new ChemistryMonadIOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    Z_Chemistry = p.Z_Chemistry,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
                    Company = p.Z_Chemistry.Company,
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
                }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 获取自己名下的已经完成的所有采购单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadCaigoudk(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false)
         .Select(p => new ChemistryMonadIOutDto
         {
             Id = p.Id.ToString(),
             Amount = p.Amount,
             ApplicantRemarks = p.ApplicantRemarks,
             ApplyNumber = p.ApplyNumber,
             ApplyTime = p.ApplyTime,
             // ApprovalType = p.ApprovalType,
             Z_Chemistry = p.Z_Chemistry,
             ArrivalTime = p.ArrivalTime,
             SupplierId = p.Supplier.Id.ToString(),
             Supplier = p.Supplier,
             ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
             Company = p.Z_Chemistry.Company,
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
         }).OrderBy(p => p.Id)
    .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }

        /// <summary>
        /// 获取自己名下的所有采购单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadTwoCaigoud(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.IsDelete == false)
        .Select(p => new ChemistryMonadIOutDto
        {
            Id = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            Z_Chemistry = p.Z_Chemistry,
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
            Company = p.Z_Chemistry.Company,
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
        }).OrderBy(p => p.Id)
    .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }


        /// <summary>
        /// 获取自己名下未完成的所有采购单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadCaigoudp(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0 && p.IsDelete == false)
    .Select(p => new ChemistryMonadIOutDto
    {
        Id = p.Id.ToString(),
        Amount = p.Amount,
        ApplicantRemarks = p.ApplicantRemarks,
        ApplyNumber = p.ApplyNumber,
        ApplyTime = p.ApplyTime,
        // ApprovalType = p.ApprovalType,
        Z_Chemistry = p.Z_Chemistry,
        ArrivalTime = p.ArrivalTime,
        SupplierId = p.Supplier.Id.ToString(),
        Supplier = p.Supplier,
        ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
        Company = p.Z_Chemistry.Company,
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
    }).OrderBy(p => p.Id)
    .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }



        /// <summary>
        /// 获取自己名下的被驳回的采购单
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRejectChemistryMonad(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 50 && p.IsDelete == false)
     .Select(p => new ChemistryMonadIOutDto
     {
         Id = p.Id.ToString(),
         Amount = p.Amount,
         ApplicantRemarks = p.ApplicantRemarks,
         ApplyNumber = p.ApplyNumber,
         ApplyTime = p.ApplyTime,
         // ApprovalType = p.ApprovalType,
         Z_Chemistry = p.Z_Chemistry,
         ArrivalTime = p.ArrivalTime,
         SupplierId = p.Supplier.Id.ToString(),
         Supplier = p.Supplier,
         ChemistryId = p.Z_Chemistry.Company.Id.ToString(),
         Company = p.Z_Chemistry.Company,
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
     }).OrderBy(p => p.Id)
    .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }




        /// <summary>
        /// 修改（完善采购单）
        /// </summary>
        /// <param name="chemistryMonadInDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> EditChemistryMonad(ChemistryMonadInDto chemistryMonadInDto)
        {


            try
            {


                var type = new ChemistryMonad() { Id = chemistryMonadInDto.Id };
                db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;

                if (!string.IsNullOrWhiteSpace(chemistryMonadInDto.Purpose))
                {
                    type.Purpose = chemistryMonadInDto.Purpose;
                }
                if (chemistryMonadInDto.ExpectArrivalTime != null)
                {
                    type.ExpectArrivalTime = chemistryMonadInDto.ExpectArrivalTime;
                }

                if (chemistryMonadInDto.ApplyNumber != null)
                {
                    type.ApplyNumber = chemistryMonadInDto.ApplyNumber;
                }

                if (chemistryMonadInDto.PurshAmount != null)
                {
                    type.PurchaseAmount = chemistryMonadInDto.PurshAmount;
                }

                if (chemistryMonadInDto.QuasiPurchaseNumber != null)
                {
                    type.QuasiPurchaseNumber = chemistryMonadInDto.QuasiPurchaseNumber;
                }
                if (chemistryMonadInDto.Price != null)
                {
                    type.Price = chemistryMonadInDto.Price;
                }
                if (chemistryMonadInDto.Amount != null)
                {
                    type.Amount = chemistryMonadInDto.Amount;
                }
                if (!string.IsNullOrWhiteSpace(chemistryMonadInDto.Enclosure))
                {
                    type.Enclosure = chemistryMonadInDto.Enclosure;
                }
                if (!string.IsNullOrWhiteSpace(chemistryMonadInDto.ApplicantRemarks))
                {
                    type.ApplicantRemarks = chemistryMonadInDto.ApplicantRemarks;
                }
                if (!string.IsNullOrWhiteSpace(chemistryMonadInDto.WaybillNumber))
                {
                    type.WaybillNumber = chemistryMonadInDto.WaybillNumber;
                }
                if (chemistryMonadInDto.ArrivalTime != null)
                {
                    type.ArrivalTime = chemistryMonadInDto.ArrivalTime;
                }
                if (!string.IsNullOrWhiteSpace(chemistryMonadInDto.PurchaseContract))
                {
                    type.PurchaseContract = chemistryMonadInDto.PurchaseContract;
                }
                if (chemistryMonadInDto.ChemistryId != null)
                {
                    type.ChemistryId = chemistryMonadInDto.ChemistryId;
                }
                if (chemistryMonadInDto.SupplierId != null)
                {
                    type.SupplierId = chemistryMonadInDto.SupplierId;
                }
              
                if (await db.SaveChangesAsync() > 0)
                {
                    return Json(new { code = 200, msg = "修改成功" });
                }
                else
                {
                    return Json(new { code = 400, msg = "修改失败" });
                }

            }
            catch (Exception ex)
            {

                throw;
            }



        }



        /// <summary>
        /// 采购员（部门）确认完成
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>

        public async Task<IHttpActionResult> SureChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Chmistry_User chmistry_User = new Chmistry_User
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = chemistry_UserIn.ChemistryId,
                    user_Id = userId,
                    ContentDes = chemistry_UserIn.ContentDes

                };
                db.Chmistry_Users.Add(chmistry_User);

                var result = new ChemistryMonad { Id = chemistry_UserIn.ChemistryId };

                db.Entry(result).State = EntityState.Unchanged;
                result.is_or = 6;

              

                if (await db.SaveChangesAsync() > 0)
                {


                    return Json(new { code = 200, msg = "添加成功" });
                }
                else
                {
                    return Json(new { code = 400, msg = "添加失败" });
                }

            }
            catch (Exception)
            {

                throw;
            }



        }
        /// <summary>
        ///库房获取入库申请(采购完成)
        /// </summary>
        /// <param name="chemistryMonadIOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetAddChemistryMonad(ChemistryMonadIOutDto chemistryMonadIOut)
        {
            //获取所有采购申请单
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null && !string.IsNullOrWhiteSpace(chemistryMonadIOut.ApplicantRelName))
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 6 && p.IsDelete == false || p.Applicant.RealName.Contains(chemistryMonadIOut.ApplicantRelName))
                .Select(p => new ChemistryMonadIOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    Z_Chemistry = p.Z_Chemistry,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    ChemistryId = p.Z_Chemistry.Id.ToString(),
                    Company = p.Z_Chemistry.Company,
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
                }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null)
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 6)
                 .Select(p => new ChemistryMonadIOutDto
                 {
                     Id = p.Id.ToString(),
                     Amount = p.Amount,
                     ApplicantRemarks = p.ApplicantRemarks,
                     ApplyNumber = p.ApplyNumber,
                     ApplyTime = p.ApplyTime,
                     // ApprovalType = p.ApprovalType,
                     Z_Chemistry = p.Z_Chemistry,
                     ArrivalTime = p.ArrivalTime,
                     SupplierId = p.Supplier.Id.ToString(),
                     Supplier = p.Supplier,
                     ChemistryId = p.Z_Chemistry.Id.ToString(),
                     Company = p.Z_Chemistry.Company,
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
                 }).OrderBy(p => p.Id)
                .Skip((chemistryMonadIOut.PageIndex * chemistryMonadIOut.PageSize) - chemistryMonadIOut.PageSize).Take(chemistryMonadIOut.PageSize).ToListAsync());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });

        }


        /// <summary>
        /// 库房的入库申请（采购单）添加入库
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>
        [HttpPost]
      
        public async Task<IHttpActionResult> AddChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            try
            {
               // ChemistryId代表采购单id
                if (chemistry_UserIn.ChemistryId != null)
                {
                    var result = await db.ChemistryMonad.SingleOrDefaultAsync(p => p.Id == chemistry_UserIn.ChemistryId && p.IsDelete == false);
                    //修改采购单
                    if (result.PurchaseAmount==null)
                    {
                        result.is_or = 5;
                        await db.SaveChangesAsync();
                        return Json(new { code = 201, msg = "请填写采购数量" });
                    }

                    result.is_or = 1;
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(result).Property("is_or").IsModified = true;
                    var results = await Task.Run(() => db.ChemistryRooms
                    .FirstOrDefaultAsync(p => p.ChemistryId == result.ChemistryId && p.del_or == false)
                    );

                    if (chemistry_UserIn.enportid != 0)
                    {
                        results = await Task.Run(() => db.ChemistryRooms.FirstOrDefaultAsync(p => p.EntrepotId == chemistry_UserIn.enportid));
                       
                    } 
                    if (results == null)
                        {
                            return Json(new { code = 201, msg = "请选对仓库，这个仓库中没有数据" });
                        }

                     if ((double)result.PurchaseAmount != null)
                        {
                            results.RawNumber = results.RawNumber + (double)result.PurchaseAmount;
                            db.Entry(results).State = System.Data.Entity.EntityState.Modified;
                            db.Entry(results).Property("RawNumber").IsModified = true;
                        }
                    //if (results == null)
                    //{
                    //Chemistry_UserDetils chemistry_UserDetils = new Chemistry_UserDetils
                    //{
                    //    Id = IdentityManager.NewId(),
                    //    ChemistryId = (long)result.ChemistryId,
                    //    entrepotid= results.EntrepotId,
                    //    o

                    //};
                 //  db.ChemistryRooms.Add(chemistryRoom);
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
        /// 获取审核人
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>
        [HttpPost]
        [ExperAuthentication]
        public async Task<IHttpActionResult> GetChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            //获取这个采购单下的审核人

            try
            {
                if (chemistry_UserIn.ChemistryId != null)
                {
                    var result = await Task.Run(() => db.Chmistry_Users.Where(p => p.ChemistryId == chemistry_UserIn.ChemistryId)
                    //.Select(p =>new Content_Users { 
                    //UserDetails=p.UserDetails,
                    //ContentDes=p.ContentDes
                    //}).ToListAsync());
             .Include(p => p.UserDetails).Include(p => p.ChemistryMonad));

                    return Json(new { code = 200, data = result });

                }

                //获取这个人审核的所有内容
                if (chemistry_UserIn.user_Id != null)
                {
                    var resul = await Task.Run(() => db.Chmistry_Users.Where(p => p.user_Id == chemistry_UserIn.user_Id)
                  .Include(p => p.UserDetails).Include(p => p.ChemistryMonad).ToListAsync());
                    return Json(new { code = 200, data = resul });
                }

                return Json(new { code = 201, msg = "无此参数数据" });



            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// 驳回（不合格）
        /// </summary>
        /// <param name="chemistry_UserIn"></param>
        /// <returns></returns>
        /// 

        [HttpPost]

        public async Task<IHttpActionResult> RejetChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            Chmistry_User chmistry_User = new Chmistry_User
            {
                Id = IdentityManager.NewId(),
                ChemistryId = chemistry_UserIn.ChemistryId,//签核单id
                user_Id = userId,
                ContentDes = chemistry_UserIn.ContentDes

            };

            db.Chmistry_Users.Add(chmistry_User);

            ChemistryMonad ChemistryMonad = new ChemistryMonad() { Id = chemistry_UserIn.ChemistryId };
            db.Entry(ChemistryMonad).State = System.Data.Entity.EntityState.Unchanged;

            //  var resuls =await db.Purchases.AsNoTracking().FirstOrDefaultAsync(p => p.Id == content_Users.Purchase_Id);
            ChemistryMonad.is_or = 50;//表示驳回



            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "驳回完成" });
            }

            return Json(new { code = 201, msg = "驳回失败" });

        }



        /// <summary>
        /// 删除采购申请单
        /// </summary>
        /// <param name="chemistryMonadInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveChemistryMonad(ChemistryMonadInDto chemistryMonadInDto)
        {

            try
            {
                foreach (var item in chemistryMonadInDto.Del_Id)
                {
                    var ChemistryMonad = new ChemistryMonad { Id = item };
                    // var RawMaterial = new RawMaterial { Id = (long)Purchase.RawMaterialId };
                    //  var Pursh_User = new Pursh_User { Id = item };

                    //   db.Entry(RawMaterial).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(ChemistryMonad).State = System.Data.Entity.EntityState.Unchanged;
                    ChemistryMonad.IsDelete = true;
                }
                if (await db.SaveChangesAsync() > 0)
                {
                    return Json(new { code = 200, msg = "删除成功" });
                }
                return Json(new { code = 201, msg = "删除失败" });
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取自己借用仓库的明细
        /// （个人的）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<Chiemistry_UserDetilsOutDto>> GetBackgeChiemistry(InputBase input)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Chemistry_UserDetils.AsNoTracking().Where(p => p.User_id == userId && p.del_or == false && p.is_or == 1)
                .Select(p => new Chiemistry_UserDetilsOutDto
                {
                    Id = (p.Id).ToString(),
                    ChemistryId = p.Z_Chemistry.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    ChemistryNumber = p.ChemistryNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Chemistry = p.Z_Chemistry
                }).OrderBy(p => p.Id)
                    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());

                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        //1.领料之后就变成了出库状态为1，is_or=1//个人获取的（userid）
        //2.点击退料，显示在入库状态为2，is_or=2//仓库获取的
        //3.库管员点击入库之后状态为入库完成3，is_or=3.入库完成的

        //4.获取物料领取明细（入库完成的）
        //5.获取出库明细（状态为1，is_or为1的）
        //6.获取准备入库的

        /// <summary>
        /// 个人点击退库
        /// </summary>
        /// <param name="chiemistry_UserDetilsin"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetBackgeChiemistry(Chiemistry_UserDetilsinDto chiemistry_UserDetilsin)
        {

            try
            {
                var result = await Task.Run(() => db.Chemistry_UserDetils.SingleOrDefaultAsync(p => p.Id == chiemistry_UserDetilsin.Id));
                result.OutIutRoom = 2;//出库状态2
                result.is_or = 2;
                if (await db.SaveChangesAsync() > 0)
                {
                    return Json(new { code = 200, msg = "退库成功" });
                }
                return Json(new { code = 200, msg = "退库失败" });
            }
            catch (Exception)
            {

                throw;
            }





        }




        /// <summary>
        /// 仓库获取退库申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Chiemistry_UserDetilsOutDto>> GetRejectChiemistry(InputBase input)
        {
            try
            {
                //   var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Chemistry_UserDetils.AsNoTracking().Where(p => p.del_or == false && p.is_or == 2)
                .Select(p => new Chiemistry_UserDetilsOutDto
                {
                    Id = (p.Id).ToString(),
                    ChemistryId = p.Z_Chemistry.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    ChemistryNumber = p.ChemistryNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Chemistry = p.Z_Chemistry
                }).OrderBy(p => p.Id)
                    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());

                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }



        /// <summary>
        /// 获取出库明细(已经完成的)
        /// (所有的)
        /// </summary>
        /// <param name="chiemistry_UserDetilsin"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<Chiemistry_UserDetilsOutDto>> GetBackChiemistry(Chiemistry_UserDetilsinDto chiemistry_UserDetilsin)
        {

            try
            {
                // var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Chemistry_UserDetils.AsNoTracking().Where(p => p.del_or == false || p.userDetails.RealName.Contains(chiemistry_UserDetilsin.RelName) || p.Z_Chemistry.Name.Contains(chiemistry_UserDetilsin.Name))
                .Select(p => new Chiemistry_UserDetilsOutDto
                {
                    Id = (p.Id).ToString(),
                    ChemistryId = p.Z_Chemistry.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    ChemistryNumber = p.ChemistryNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Chemistry = p.Z_Chemistry
                }).OrderBy(p => p.Id)
                    .Skip((chiemistry_UserDetilsin.PageIndex * chiemistry_UserDetilsin.PageSize) - chiemistry_UserDetilsin.PageSize).Take(chiemistry_UserDetilsin.PageSize).ToListAsync());

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 退库入库（库管员）
        /// </summary>
        /// <param name = "chiemistry_UserDetilsin" ></param>
        /// <returns></returns >
        [HttpPost]
        public async Task<IHttpActionResult> BackChiemistry(Chiemistry_UserDetilsinDto chiemistry_UserDetilsin)
        {
            try
            {
                if (chiemistry_UserDetilsin.ChemistryId != null)
                {
                    var result = await Task.Run(() => db.Chemistry_UserDetils.SingleOrDefaultAsync(p => p.Id == chiemistry_UserDetilsin.Id));
                    if (result != null)
                    {
                        var reus = await Task.Run(() => db.ChemistryRooms
                        .SingleOrDefaultAsync(p => p.ChemistryId == result.ChemistryId && p.Z_Chemistry.IsDelete == false && p.EntrepotId == result.entrepotid));

                        if (reus==null)
                        {
                            return Json(new { code = 200, msg = "库房没有此物品" });
                        }
                        reus.RawNumber = reus.RawNumber + result.ChemistryNumber;
                        result.OutIutRoom = 3;//入库完成
                        result.is_or = 3;
                        //////var reust = await Task.Run(() => db.Purchases
                        //////.SingleOrDefaultAsync(p => p.RawId == result.RawId && p.Applicant.Id==result.User_id && p.IsDelete == false));
                        // reust.is_or = 20;//退库完成
                        if (await db.SaveChangesAsync() > 0)
                        {
                            return Json(new { code = 200, msg = "成功退库" });

                        }
                        return Json(new { code = 201, msg = "入库失败" });
                    }

                    return Json(new { code = 201, msg = "没有这个物料" });

                }
                return Json(new { code = 201, msg = "请传递正确值" });
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取有这个原材料的仓库(出库，入库的选择)
        /// </summary>
        /// <param name="chemistryMonadInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ChimetryRoomsDto>> BackEntitys(ChemistryMonadInDto chemistryMonadInDto)
        {
            var result = await Task.Run(() => db.ChemistryRooms.AsNoTracking()
            .Where(p => p.ChemistryId == chemistryMonadInDto.ChemistryId && p.del_or == false).Select(p => new ChimetryRoomsDto
            {
                ChemistryId = p.Z_Chemistry.Id,

                EntrepotId = p.entrepot.Id.ToString(),
                entrepot = p.entrepot
            }).ToListAsync());

            return result;


        }
    }
}
