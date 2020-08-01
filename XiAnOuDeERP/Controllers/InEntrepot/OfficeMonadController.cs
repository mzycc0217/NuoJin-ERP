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
    [RoutePrefix("api/OfficeMonad")]
    public class OfficeMonadController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        private int Is_Or { get; set; }
        /// <summary>
        /// 添加办公用品单
        /// </summary>
        /// <param name="officeMonadlnDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetAddOfficeMonad(OfficeMonadlnDto officeMonadlnDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            try
            {
                //var amount = decimal.Zero;

                //if (officeMonadlnDto.Price != null && officeMonadlnDto.QuasiPurchaseNumber != null && officeMonadlnDto.Price != 0 && officeMonadlnDto.QuasiPurchaseNumber != 0)
                //{
                //    amount = (decimal)officeMonadlnDto.Price * (decimal)officeMonadlnDto.QuasiPurchaseNumber;
                //}

                if (officeMonadlnDto.OfficeId != null)
                {
                    var result = await Task.Run(() => db.Offices.AsNoTracking().Where(p => p.OfficeId == officeMonadlnDto.OfficeId));


                    foreach (var item in result)
                    {

                        if (item.RawNumber >= officeMonadlnDto.ApplyNumber)
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


                    OfficeMonad officeMonad = new OfficeMonad()
                    {
                        Id = IdentityManager.NewId(),
                      //  Amount = amount,
                        ApplicantId = userId,
                        ApplicantRemarks = officeMonadlnDto.ApplicantRemarks,
                        ApplyNumber = officeMonadlnDto.ApplyNumber,
                        ApplyTime = officeMonadlnDto.ApplyTime,

                        ArrivalTime = officeMonadlnDto.ArrivalTime,
                        SupplierId = officeMonadlnDto.SupplierId,
                        Enclosure = officeMonadlnDto.Enclosure,
                      //  Price = officeMonadlnDto.Price,

                        PurchaseContract = officeMonadlnDto.PurchaseContract,
                        PurchaseTime = officeMonadlnDto.PurchaseTime,
                        Purpose = officeMonadlnDto.Purpose,
                        QuasiPurchaseNumber = officeMonadlnDto.ApplyNumber,
                        OfficeId = officeMonadlnDto.OfficeId,
                        WaybillNumber = officeMonadlnDto.WaybillNumber,
                        ExpectArrivalTime = officeMonadlnDto.ExpectArrivalTime,
                        IsDelete = false,

                        is_or = this.Is_Or,
                        // ApprovalKey = related.ApprovalKey,

                    };

                    db.OfficeMonad.Add(officeMonad);
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
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetsOfficeMonad(OfficeMonadOutDto officeMonadOutDto)
        {
            //获取所有采购申请单
           
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false)
                .Select(p => new OfficeMonadOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    Z_Office=p.Z_Office,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    OfficeId = p.Z_Office.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize));
                if (!string.IsNullOrWhiteSpace(officeMonadOutDto.RealName))
                {
                    result = await Task.Run(()=> result.Where(p => p.Applicant.RealName.Contains(officeMonadOutDto.RealName)));
                         return Json(new { code = 200, data =await result.ToListAsync(), Count = result.Count() });
                }
                return Json(new { code = 200, data = await result.ToListAsync(), Count = result.Count() });
            
         
          
        }

        /// <summary>
        /// 领导获取的部门的申请单
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonads(OfficeMonadOutDto officeMonadOutDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            //获取当前领导的部门
            var resule = await db.UserDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == userId);
            var resul = await db.User.AsNoTracking().FirstOrDefaultAsync(p => p.Id == resule.UserId);


            //获取所有采购申请单
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null && !string.IsNullOrWhiteSpace(officeMonadOutDto.ApplicantRelName))
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false && p.Applicant.User.DepartmentId == resul.DepartmentId || p.Applicant.RealName.Contains(officeMonadOutDto.ApplicantRelName))
                //  .Include(p=>p.Applicant.User.DepartmentId== resul.DepartmentId)
                .Select(p => new OfficeMonadOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    Z_Office=p.Z_Office,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                     OfficeId= p.Z_Office.Company.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, data = result, Count = result.Count() });
            }
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false && p.Applicant.User.DepartmentId == resul.DepartmentId)
                //   .Include(p => p.Applicant.User.DepartmentId == resul.DepartmentId)
                .Select(p => new OfficeMonadOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    Z_Office = p.Z_Office,
                    // ApprovalType = p.ApprovalType,
                    //  AssetExpenditureDesc = p.AssetExpenditureDesc,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    OfficeId = p.Z_Office.Company.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToList());
                return Json(new { code = 200, data = result, Count = result.Count() });
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 库管员签核申请单
        /// </summary>
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetOfficeMonadds(Office_UserInDto office_UserInDto)
        {

            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;

                var resuls = await db.OfficeMonad.SingleOrDefaultAsync(p => p.Id == office_UserInDto.OfficeId);
                //添加领料
                long Enportid;//仓库id
                if (office_UserInDto.enportid == null)
                {

                    var result = await Task.Run(() => db.Offices
                    .Where(p => p.OfficeId == resuls.OfficeId && p.RawNumber >= resuls.QuasiPurchaseNumber).FirstOrDefaultAsync());
                    if (result == null)
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
                    Enportid = office_UserInDto.enportid;
                    var officeRoom = await db.Offices
                  .FirstOrDefaultAsync(p => p.OfficeId == resuls.OfficeId && p.EntrepotId == Enportid);
                  //  OfficeRoom officeRoom = new OfficeRoom { OfficeId = (long)resuls.OfficeId, EntrepotId = Enportid };
                   db.Entry(officeRoom).State = System.Data.Entity.EntityState.Unchanged;
                   officeRoom.RawNumber = officeRoom.RawNumber - (double)resuls.QuasiPurchaseNumber;
                }
                //var OfficeRoom = new OfficeRoom { ChemistryId = (long)resuls.ChemistryId };
                //db.Entry(OfficeRoom).State = System.Data.Entity.EntityState.Unchanged;
                //OfficeRoom.RawNumber = OfficeRoom.RawNumber - (double)resuls.QuasiPurchaseNumber;
                resuls.is_or = 0;
                db.Entry(resuls).State = System.Data.Entity.EntityState.Modified;
                db.Entry(resuls).Property("is_or").IsModified = true;


                Office_UsrDetils office_UsrDetils = new Office_UsrDetils
                {
                    Id = IdentityManager.NewId(),
                    OfficeId = (long)resuls.OfficeId,
                    User_id = resuls.Applicant.Id,
                    OutIutRoom = 1,//出库状态,
                    is_or = 1,//显示为出库状态（个人获取到的）
                              //添加仓库（这里）
                    entrepotid = Enportid,
                    OfficeNumber = (double)resuls.QuasiPurchaseNumber,
                    GetTime = DateTime.Now
                };
                db.Office_UsrDetils.Add(office_UsrDetils);
                // 添加签核人
                Office_User office_User = new Office_User
                {
                    Id = IdentityManager.NewId(),
                    OfficeId = office_UserInDto.OfficeId,
                    user_Id = userId,
                    ContentDes = office_UserInDto.ContentDes
                };
                db.Office_Users.Add(office_User);
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
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetOfficeMonadMonad(Office_UserInDto office_UserInDto)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Office_User office_User = new Office_User
                {
                    Id = IdentityManager.NewId(),
                    OfficeId = office_UserInDto.OfficeId,
                    user_Id = userId,
                    ContentDes = office_UserInDto.ContentDes

                };

                db.Office_Users.Add(office_User);
                var resuls = await db.OfficeMonad.SingleOrDefaultAsync(p => p.Id == office_UserInDto.OfficeId);
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
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonadCaigou(OfficeMonadOutDto officeMonadOutDto)
        {
            //获取所有采购申请单
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null && !string.IsNullOrWhiteSpace(officeMonadOutDto.ApplicantRelName))
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 5 && p.IsDelete == false || p.Applicant.RealName.Contains(officeMonadOutDto.ApplicantRelName))
                .Select(p => new OfficeMonadOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    Z_Office = p.Z_Office,
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    OfficeId = p.Z_Office.Company.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToList());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 5 && p.IsDelete == false)
              .Select(p => new OfficeMonadOutDto
              {
                  Id = p.Id.ToString(),
                  Amount = p.Amount,
                  ApplicantRemarks = p.ApplicantRemarks,
                  ApplyNumber = p.ApplyNumber,
                  ApplyTime = p.ApplyTime,
                  // ApprovalType = p.ApprovalType,
                  Z_Office = p.Z_Office,
                  ArrivalTime = p.ArrivalTime,
                  SupplierId = p.Supplier.Id.ToString(),
                  Supplier = p.Supplier,
                  OfficeId = p.Z_Office.Company.Id.ToString(),
                  Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }


        /// <summary>
        /// 获取完成的采购单（获取自己名下的所有采购单input.PageSize == -2input.PageIndex == -2）（获取自己名下的已经完成的所有采购单input.PageSize == -1nput.PageIndex == -1）
        ///获取自己名下未完成的采购单（获取自己名下的所有采购单input.PageSize == -3input.PageIndex == -3）
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonadCaigoud(OfficeMonadOutDto officeMonadOutDto)
        {

            //获取自己发布的申请单
            var userId = ((UserIdentity)User.Identity).UserId;
            if (officeMonadOutDto.PageSize == -1 && officeMonadOutDto.PageIndex == -1)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false)
        .Select(p => new OfficeMonadOutDto
        {
            Id = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            Z_Office = p.Z_Office,
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            OfficeId = p.Z_Office.Company.Id.ToString(),
            Company = p.Z_Office.Company,
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
        .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的所有采购单
            if (officeMonadOutDto.PageSize == -2 && officeMonadOutDto.PageIndex == -2)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId)
       .Select(p => new OfficeMonadOutDto
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
           OfficeId = p.Z_Office.Company.Id.ToString(),
           Company = p.Z_Office.Company,
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
        .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的未完成的采购单
            if (officeMonadOutDto.PageSize == -3 && officeMonadOutDto.PageIndex == -3)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0)
        .Select(p => new OfficeMonadOutDto
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
            OfficeId = p.Z_Office.Company.Id.ToString(),
            Company = p.Z_Office.Company,
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
        .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取所有采购申请单
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null && !string.IsNullOrWhiteSpace(officeMonadOutDto.ApplicantRelName))
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.RealName.Contains(officeMonadOutDto.ApplicantRelName) && p.is_or == 0)
                 .Select(p => new OfficeMonadOutDto
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
                     OfficeId = p.Z_Office.Company.Id.ToString(),
                     Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 0)
                .Select(p => new OfficeMonadOutDto
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
                    OfficeId = p.Z_Office.Company.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 获取自己名下的已经完成的所有采购单
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonadCaigoudd(OfficeMonadOutDto officeMonadOutDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false)
        .Select(p => new OfficeMonadOutDto
        {
            Id = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            Z_Office = p.Z_Office,
            //  AssetExpenditureDesc = p.AssetExpenditureDesc,
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            OfficeId = p.Z_Office.Company.Id.ToString(),
            Company = p.Z_Office.Company,
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
    .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }

        /// <summary>
        /// 获取自己名下的所有采购单
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonadTwoCaigoud(OfficeMonadOutDto officeMonadOutDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.IsDelete == false)
       .Select(p => new OfficeMonadOutDto
       {
           Id = p.Id.ToString(),
           Amount = p.Amount,
           ApplicantRemarks = p.ApplicantRemarks,
           ApplyNumber = p.ApplyNumber,
           ApplyTime = p.ApplyTime,
           Z_Office = p.Z_Office,
           //  AssetExpenditureDesc = p.AssetExpenditureDesc,
           ArrivalTime = p.ArrivalTime,
           SupplierId = p.Supplier.Id.ToString(),
           Supplier = p.Supplier,
           OfficeId = p.Z_Office.Company.Id.ToString(),
           Company = p.Z_Office.Company,
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
    .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }


        /// <summary>
        /// 获取自己名下未完成的所有采购单
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeMonadCaigoudp(OfficeMonadOutDto officeMonadOutDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0 && p.IsDelete == false)
   .Select(p => new OfficeMonadOutDto
   {
       Id = p.Id.ToString(),
       Amount = p.Amount,
       ApplicantRemarks = p.ApplicantRemarks,
       ApplyNumber = p.ApplyNumber,
       ApplyTime = p.ApplyTime,
       // ApprovalType = p.ApprovalType,
       Z_Office = p.Z_Office,
       ArrivalTime = p.ArrivalTime,
       SupplierId = p.Supplier.Id.ToString(),
       Supplier = p.Supplier,
       OfficeId = p.Z_Office.Company.Id.ToString(),
       Company = p.Z_Office.Company,
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
    .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }



        /// <summary>
        /// 获取自己名下的被驳回的采购单
        /// </summary>
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRejectOfficeMonad(OfficeMonadOutDto officeMonadOutDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 50 && p.IsDelete == false)
    .Select(p => new OfficeMonadOutDto
    {
        Id = p.Id.ToString(),
        Amount = p.Amount,
        ApplicantRemarks = p.ApplicantRemarks,
        ApplyNumber = p.ApplyNumber,
        ApplyTime = p.ApplyTime,
        // ApprovalType = p.ApprovalType,
        Z_Office = p.Z_Office,
        ArrivalTime = p.ArrivalTime,
        SupplierId = p.Supplier.Id.ToString(),
        Supplier = p.Supplier,
        OfficeId = p.Z_Office.Company.Id.ToString(),
        Company = p.Z_Office.Company,
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
    .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            return Json(new { code = 201 });
        }




        /// <summary>
        /// 修改（完善采购单）
        /// </summary>
        /// <param name="officeMonadlnDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> EditOfficeMonad(OfficeMonadlnDto officeMonadlnDto)
        {


            try
            {


                OfficeMonad type = new OfficeMonad() { Id = officeMonadlnDto.Id };
                db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;

                if (!string.IsNullOrWhiteSpace(officeMonadlnDto.Purpose))
                {
                    type.Purpose = officeMonadlnDto.Purpose;
                }
                if (officeMonadlnDto.ExpectArrivalTime != null)
                {
                    type.ExpectArrivalTime = officeMonadlnDto.ExpectArrivalTime;
                }
                if (officeMonadlnDto.PurshAmount != null)
                {
                    type.PurchaseAmount = officeMonadlnDto.PurshAmount;
                }
                if (officeMonadlnDto.ApplyNumber != null)
                {
                    type.ApplyNumber = officeMonadlnDto.ApplyNumber;
                }

                if (officeMonadlnDto.QuasiPurchaseNumber != null)
                {
                    type.QuasiPurchaseNumber = officeMonadlnDto.QuasiPurchaseNumber;
                }
                if (officeMonadlnDto.Price != null)
                {
                    type.Price = officeMonadlnDto.Price;
                }
                if (officeMonadlnDto.Amount != null)
                {
                    type.Amount = officeMonadlnDto.Amount;
                }
                if (!string.IsNullOrWhiteSpace(officeMonadlnDto.Enclosure))
                {
                    type.Enclosure = officeMonadlnDto.Enclosure;
                }
                if (!string.IsNullOrWhiteSpace(officeMonadlnDto.ApplicantRemarks))
                {
                    type.ApplicantRemarks = officeMonadlnDto.ApplicantRemarks;
                }
                if (!string.IsNullOrWhiteSpace(officeMonadlnDto.WaybillNumber))
                {
                    type.WaybillNumber = officeMonadlnDto.WaybillNumber;
                }
                if (officeMonadlnDto.ArrivalTime != null)
                {
                    type.ArrivalTime = officeMonadlnDto.ArrivalTime;
                }
                if (!string.IsNullOrWhiteSpace(officeMonadlnDto.PurchaseContract))
                {
                    type.PurchaseContract = officeMonadlnDto.PurchaseContract;
                }
                if (officeMonadlnDto.OfficeId != null)
                {
                    type.OfficeId = officeMonadlnDto.OfficeId;
                }
                if (officeMonadlnDto.SupplierId != null)
                {
                    type.SupplierId = officeMonadlnDto.SupplierId;
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
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>

        public async Task<IHttpActionResult> SureOfficeMonad(Office_UserInDto office_UserInDto)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Office_User office_User = new Office_User
                {
                    Id = IdentityManager.NewId(),
                    OfficeId = office_UserInDto.OfficeId,
                    user_Id = userId,
                    ContentDes = office_UserInDto.ContentDes

                };
                db.Office_Users.Add(office_User);

                var result = new OfficeMonad { Id = office_UserInDto.OfficeId };

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
        /// <param name="officeMonadOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetAddOfficeMonad(OfficeMonadOutDto officeMonadOutDto)
        {
            //获取所有采购申请单
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null && !string.IsNullOrWhiteSpace(officeMonadOutDto.ApplicantRelName))
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 6 && p.IsDelete == false || p.Applicant.RealName.Contains(officeMonadOutDto.ApplicantRelName))
               .Select(p => new OfficeMonadOutDto
               {
                   Id = p.Id.ToString(),
                   Amount = p.Amount,
                   ApplicantRemarks = p.ApplicantRemarks,
                   ApplyNumber = p.ApplyNumber,
                   ApplyTime = p.ApplyTime,
                   // ApprovalType = p.ApprovalType,
                   Z_Office = p.Z_Office,
                   ArrivalTime = p.ArrivalTime,
                   SupplierId = p.Supplier.Id.ToString(),
                   Supplier = p.Supplier,
                   OfficeId = p.Z_Office.Id.ToString(),
                   Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            if (officeMonadOutDto.PageSize != null && officeMonadOutDto.PageIndex != null)
            {
                var result = await Task.Run(() => db.OfficeMonad.Where(p => p.Id > 0 && p.is_or == 6)
                .Select(p => new OfficeMonadOutDto
                {
                    Id = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    Z_Office = p.Z_Office,
                   // OfficeId = p.Z_Office.Id.ToString(),
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    OfficeId = p.Z_Office.Id.ToString(),
                    Company = p.Z_Office.Company,
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
                .Skip((officeMonadOutDto.PageIndex * officeMonadOutDto.PageSize) - officeMonadOutDto.PageSize).Take(officeMonadOutDto.PageSize).ToListAsync());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });

        }


        /// <summary>
        /// 库房的入库申请（采购单）添加入库
        /// </summary>
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IHttpActionResult> AddOfficeMonad(Office_UserInDto office_UserInDto)
        {
            try
            {
                // ChemistryId代表采购单id
                if (office_UserInDto.OfficeId != null)
                {
                    var result = await db.OfficeMonad.SingleOrDefaultAsync(p => p.Id == office_UserInDto.OfficeId && p.IsDelete == false);

                    //修改采购单
                    if (result.PurchaseAmount == null)
                    {
                        result.is_or = 5;
                        await db.SaveChangesAsync();
                        return Json(new { code = 201, msg = "请填写采购数量" });
                    }
                    //修改采购单
                    result.is_or = 1;
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(result).Property("is_or").IsModified = true;
                    var results = await Task.Run(() => db.Offices
                    .FirstOrDefaultAsync(p => p.OfficeId == result.OfficeId && p.del_or == false)
                    );

                    if (office_UserInDto.enportid != 0)
                    {
                        results = await Task.Run(() => db.Offices.FirstOrDefaultAsync(p => p.EntrepotId == office_UserInDto.enportid));
                    }

                    if ((double)result.PurchaseAmount != null)
                    {
                        results.RawNumber = results.RawNumber + (double)result.PurchaseAmount;
                        db.Entry(results).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(results).Property("RawNumber").IsModified = true;
                    }
                    //if (results == null)
                    //{
                    //    ChemistryRoom chemistryRoom = new ChemistryRoom
                    //    {
                    //        Id = IdentityManager.NewId(),
                    //        ChemistryId = (long)result.ChemistryId,
                    //        RawNumber = (double)result.PurchaseAmount,
                    //        EntrepotId = chemistry_UserIn.enportid

                    //    };
                    //    db.ChemistryRooms.Add(chemistryRoom);
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
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>
        [HttpPost]
        
        public async Task<IHttpActionResult> GetOfficeMonad(Office_UserInDto office_UserInDto)
        {
            //获取这个采购单下的审核人

            try
            {
                if (office_UserInDto.OfficeId != null)
                {
                    var result = await Task.Run(() => db.Office_Users.Where(p => p.OfficeId == office_UserInDto.OfficeId)
                    //.Select(p =>new Content_Users { 
                    //UserDetails=p.UserDetails,
                    //ContentDes=p.ContentDes
                    //}).ToListAsync());
             .Include(p => p.UserDetails).Include(p => p.OfficeMonad));

                    return Json(new { code = 200, data = result });

                }

                //获取这个人审核的所有内容
                if (office_UserInDto.user_Id != null)
                {
                    var resul = await Task.Run(() => db.Office_Users.Where(p => p.user_Id == office_UserInDto.user_Id)
                  .Include(p => p.UserDetails).Include(p => p.OfficeMonad).ToListAsync());
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
        /// <param name="office_UserInDto"></param>
        /// <returns></returns>
        /// 

        [HttpPost]

        public async Task<IHttpActionResult> RejetOfficeMonad(Office_UserInDto office_UserInDto)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            Office_User office_User = new Office_User
            {
                Id = IdentityManager.NewId(),
                OfficeId = office_UserInDto.OfficeId,//签核单id
                user_Id = userId,
                ContentDes = office_UserInDto.ContentDes

            };

            db.Office_Users.Add(office_User);

            OfficeMonad officeMonad = new OfficeMonad() { Id = office_UserInDto.OfficeId };
            db.Entry(officeMonad).State = System.Data.Entity.EntityState.Unchanged;

            //  var resuls =await db.Purchases.AsNoTracking().FirstOrDefaultAsync(p => p.Id == content_Users.Purchase_Id);
            officeMonad.is_or = 50;//表示驳回



            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "驳回完成" });
            }

            return Json(new { code = 201, msg = "驳回失败" });

        }



        /// <summary>
        /// 删除采购申请单
        /// </summary>
        /// <param name="officeMonadlnDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveOfficeMonad(OfficeMonadlnDto officeMonadlnDto)
        {

            try
            {
                foreach (var item in officeMonadlnDto.Del_Id)
                {
                    OfficeMonad officeMonad = new OfficeMonad { Id = item };
                    // var RawMaterial = new RawMaterial { Id = (long)Purchase.RawMaterialId };
                    //  var Pursh_User = new Pursh_User { Id = item };

                    //   db.Entry(RawMaterial).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(officeMonad).State = System.Data.Entity.EntityState.Unchanged;
                    officeMonad.IsDelete = true;
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
        public async Task<List<Office_UserDetils>> GetBackgeOfficeMonad(InputBase input)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Office_UsrDetils.AsNoTracking().Where(p => p.User_id == userId && p.del_or == false && p.is_or == 1)
                .Select(p => new Office_UserDetils
                {
                    Id = (p.Id).ToString(),
                    OfficeId = p.Z_Office.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    OfficeNumber = p.OfficeNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Office = p.Z_Office
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
        /// <param name="officeInDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetBackgeOfficeMonad(OfficeInDto officeInDto)
        {

            try
            {
                var result = await Task.Run(() => db.Office_UsrDetils.SingleOrDefaultAsync(p => p.Id == officeInDto.Id));
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
        public async Task<List<Office_UserDetils>> GetRejectOfficeMonads(InputBase input)
        {
            try
            {
                //   var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Office_UsrDetils.AsNoTracking().Where(p => p.del_or == false && p.is_or == 2)
                .Select(p => new Office_UserDetils
                {
                    Id = (p.Id).ToString(),
                    OfficeId = p.Z_Office.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    OfficeNumber = p.OfficeNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Office = p.Z_Office
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
        /// <param name="officeInDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<Office_UserDetils>> GetBackOfficeMonady(OfficeInDto officeInDto)
        {

            try
            {
                // var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Office_UsrDetils.AsNoTracking().Where(p => p.del_or == false || p.userDetails.RealName.Contains(officeInDto.RelName) || p.Z_Office.Name.Contains(officeInDto.Name))
                .Select(p => new Office_UserDetils
                {
                    Id = (p.Id).ToString(),
                    OfficeId = p.Z_Office.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    OfficeNumber = p.OfficeNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetTime = p.GetTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Office = p.Z_Office
                }).OrderBy(p => p.Id)
                    .Skip((officeInDto.PageIndex * officeInDto.PageSize) - officeInDto.PageSize).Take(officeInDto.PageSize).ToListAsync());

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
        /// <param name = "officeInDto" ></param>
        /// <returns></returns >
        [HttpPost]
        public async Task<IHttpActionResult> BackOfficeMonad(OfficeInDto officeInDto)
        {
            try
            {
                if (officeInDto.OfficeId != null)
                {
                    var result = await Task.Run(() => db.Office_UsrDetils.SingleOrDefaultAsync(p => p.Id == officeInDto.Id));
                    if (result != null)
                    {
                        var reus = await Task.Run(() => db.Offices
                        .SingleOrDefaultAsync(p => p.OfficeId == result.OfficeId && p.Z_Office.IsDelete == false && p.EntrepotId == result.entrepotid));

                        if (reus == null)
                        {
                            return Json(new { code = 200, msg = "库房没有此物品" });
                        }
                        reus.RawNumber = reus.RawNumber + result.OfficeNumber;
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
        /// <param name="officeMonadlnDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<OffficeRoomDto>> BackEntitys(OfficeMonadlnDto officeMonadlnDto)
        {
            var result = await Task.Run(() => db.Offices.AsNoTracking().Where(p => p.OfficeId== officeMonadlnDto.OfficeId&&p.del_or==false).Select(p => new OffficeRoomDto
            {
                OfficeId = p.Z_Office.Id,

                EntrepotId = p.entrepot.Id.ToString(),
                entrepot = p.entrepot
            }).ToListAsync());

            return result;


        }

    }
}
