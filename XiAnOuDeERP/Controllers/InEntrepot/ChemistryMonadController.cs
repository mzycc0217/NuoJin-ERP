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
                    var result = await Task.Run(() => db.RawRooms.AsNoTracking().SingleOrDefaultAsync(p => p.Id == chemistryMonadInDto.ChemistryId));
                    int is_or;

                    if (result.RawNumber >= chemistryMonadInDto.ApplyNumber)
                    {
                        //可以直接领取
                        is_or = 1;
                    }
                    else
                    {
                        //不可以直接领取
                        is_or = 2;
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
                        QuasiPurchaseNumber = chemistryMonadInDto.QuasiPurchaseNumber,
                        ChemistryId = chemistryMonadInDto.ChemistryId,
                        WaybillNumber = chemistryMonadInDto.WaybillNumber,
                        ExpectArrivalTime = chemistryMonadInDto.ExpectArrivalTime,
                        IsDelete = false,

                        is_or = (int)is_or,
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
            //获取所有采购申请单
            if (chemistryMonadIOut.PageSize != null && chemistryMonadIOut.PageIndex != null && !string.IsNullOrWhiteSpace(chemistryMonadIOut.Applicant.RealName))
            {
                var result = await Task.Run(() => db.ChemistryMonad.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false || p.Applicant.RealName.Contains(chemistryMonadIOut.Applicant.RealName))
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
                return Json(new { code = 200, data = result, Count = result.Count() });
            }
            return Json(new { code = 400 });
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
                    // ApprovalType = p.ApprovalType,
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
               // 添加签核人
                Chmistry_User chmistry_User = new Chmistry_User
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = chemistry_UserIn.ChemistryId,
                    user_Id = userId,
                    ContentDes = chemistry_UserIn.ContentDes
                };
                db.Chmistry_Users.Add(chmistry_User);
                var resuls = await db.ChemistryMonad.SingleOrDefaultAsync(p => p.Id == chemistry_UserIn.ChemistryId);
                //添加领料
                Chemistry_UserDetils chemistry_UserDetils1 = new Chemistry_UserDetils
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = (long)resuls.ChemistryId,
                    User_id = userId,
                    ChemistryNumber = (double)resuls.QuasiPurchaseNumber,
                    GetTime = DateTime.Now
                };
                db.Chemistry_UserDetils.Add(chemistry_UserDetils1);


                // RawId = resuls.RawId;
                //ApplyNumber = resuls.QuasiPurchaseNumber;

                //var resul = new Purchase { Id = content_Users.Purchase_Id };
                //db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;




                var res = new ChemistryRoom { ChemistryId = (long)resuls.ChemistryId };
                db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                res.RawNumber = res.RawNumber - (double)resuls.QuasiPurchaseNumber;
                resuls.is_or = 0;


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
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }


        /// <summary>
        /// 获取完成的采购单（获取自己名下的所有采购单input.PageSize == -2input.PageIndex == -2）（获取自己名下的已经完成的所有采购单input.PageSize == -1nput.PageIndex == -1）
        ///获取自己名下未完成的采购单（获取自己名下的所有采购单input.PageSize == -3input.PageIndex == -3）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryMonadCaigoud(PurshOutDto input)
        {

            //获取自己发布的申请单
            var userId = ((UserIdentity)User.Identity).UserId;
            if (input.PageSize == -1 && input.PageIndex == -1)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false)
        .Select(p => new PurshOutDto
        {
            PurchaseId = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
            //  ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            CompanyId = p.Z_Raw.Company.ToString(),
            company = p.Z_Raw.Company,
            Enclosure = p.Enclosure,
            Price = p.Price,
            PurchaseContract = p.PurchaseContract,
            PurchaseTime = p.PurchaseTime,
            Purpose = p.Purpose,
            QuasiPurchaseNumber = p.QuasiPurchaseNumber,
            WaybillNumber = p.WaybillNumber,
            ApplicantId = p.ApplicantId.ToString(),
            Applicant = p.Applicant,
            ApprovalDesc = p.ApprovalDesc,
            ExpectArrivalTime = p.ExpectArrivalTime,
            ProjectId = p.ProjectId.ToString(),
            Project = p.Project,
            RawId = p.Z_Raw.Id.ToString(),
            z_Raw = p.Z_Raw,
            Z_RowType = p.Z_Raw.Z_RowType
        }).OrderBy(p => p.PurchaseId)
        .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的所有采购单
            if (input.PageSize == -2 && input.PageIndex == -2)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId)
        .Select(p => new PurshOutDto
        {
            PurchaseId = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
            //  ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            CompanyId = p.Z_Raw.Company.ToString(),
            company = p.Z_Raw.Company,
            Enclosure = p.Enclosure,
            Price = p.Price,
            PurchaseContract = p.PurchaseContract,
            PurchaseTime = p.PurchaseTime,
            Purpose = p.Purpose,
            QuasiPurchaseNumber = p.QuasiPurchaseNumber,
            WaybillNumber = p.WaybillNumber,
            ApplicantId = p.ApplicantId.ToString(),
            Applicant = p.Applicant,
            ApprovalDesc = p.ApprovalDesc,
            ExpectArrivalTime = p.ExpectArrivalTime,
            ProjectId = p.ProjectId.ToString(),
            Project = p.Project,
            RawId = p.Z_Raw.Id.ToString(),
            z_Raw = p.Z_Raw,
            Z_RowType = p.Z_Raw.Z_RowType
        }).OrderBy(p => p.PurchaseId)
        .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取自己名下的未完成的采购单
            if (input.PageSize == -3 && input.PageIndex == -3)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0)
        .Select(p => new PurshOutDto
        {
            PurchaseId = p.Id.ToString(),
            Amount = p.Amount,
            ApplicantRemarks = p.ApplicantRemarks,
            ApplyNumber = p.ApplyNumber,
            ApplyTime = p.ApplyTime,
            // ApprovalType = p.ApprovalType,
            //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
            ArrivalTime = p.ArrivalTime,
            SupplierId = p.Supplier.Id.ToString(),
            Supplier = p.Supplier,
            CompanyId = p.Z_Raw.Company.ToString(),
            company = p.Z_Raw.Company,
            Enclosure = p.Enclosure,
            Price = p.Price,
            PurchaseContract = p.PurchaseContract,
            PurchaseTime = p.PurchaseTime,
            Purpose = p.Purpose,
            QuasiPurchaseNumber = p.QuasiPurchaseNumber,
            WaybillNumber = p.WaybillNumber,
            ApplicantId = p.ApplicantId.ToString(),
            Applicant = p.Applicant,
            ApprovalDesc = p.ApprovalDesc,
            ExpectArrivalTime = p.ExpectArrivalTime,
            ProjectId = p.ProjectId.ToString(),
            Project = p.Project,
            RawId = p.Z_Raw.Id.ToString(),
            z_Raw = p.Z_Raw,
            Z_RowType = p.Z_Raw.Z_RowType
        }).OrderBy(p => p.PurchaseId)
        .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            //获取所有采购申请单
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.RealName.Contains(input.ApplicantRelName) && p.is_or == 0)
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    CompanyId = p.Z_Raw.Company.Id.ToString(),
                    company = p.Z_Raw.Company,
                    Enclosure = p.Enclosure,
                    Price = p.Price,
                    PurchaseContract = p.PurchaseContract,
                    PurchaseTime = p.PurchaseTime,
                    Purpose = p.Purpose,
                    QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                    WaybillNumber = p.WaybillNumber,
                    ApplicantId = p.ApplicantId.ToString(),
                    Applicant = p.Applicant,
                    ApprovalDesc = p.ApprovalDesc,
                    ExpectArrivalTime = p.ExpectArrivalTime,
                    ProjectId = p.ProjectId.ToString(),
                    Project = p.Project,
                    RawId = p.Z_Raw.Id.ToString(),
                    z_Raw = p.Z_Raw,
                    Z_RowType = p.Z_Raw.Z_RowType
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 0)
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
                    ArrivalTime = p.ArrivalTime,
                    SupplierId = p.Supplier.Id.ToString(),
                    Supplier = p.Supplier,
                    CompanyId = p.Z_Raw.Company.Id.ToString(),
                    company = p.Z_Raw.Company,
                    Enclosure = p.Enclosure,
                    Price = p.Price,
                    PurchaseContract = p.PurchaseContract,
                    PurchaseTime = p.PurchaseTime,
                    Purpose = p.Purpose,
                    QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                    WaybillNumber = p.WaybillNumber,
                    ApplicantId = p.ApplicantId.ToString(),
                    Applicant = p.Applicant,
                    ApprovalDesc = p.ApprovalDesc,
                    ExpectArrivalTime = p.ExpectArrivalTime,
                    ProjectId = p.ProjectId.ToString(),
                    Project = p.Project,
                    RawId = p.Z_Raw.Id.ToString(),
                    z_Raw = p.Z_Raw,
                    Z_RowType = p.Z_Raw.Z_RowType
                }).OrderBy(p => p.PurchaseId)
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
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
        public async Task<IHttpActionResult> GetChemistryMonadCaigoud(ChemistryMonadIOutDto chemistryMonadIOut)
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

                // var resuls =await Task.Run(()=>(  db.Purchases.AsNoTracking().SingleOrDefaultAsync(p => p.Id == content_Users.Purchase_Id));
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
                //foreach (var item in resuls)
                //{
                //    item.is_or = 6;
                //}

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
        [ExperAuthentication]
        public async Task<IHttpActionResult> AddChemistryMonad(Chemistry_UserInDto chemistry_UserIn)
        {
            try
            {
               // ChemistryId代表采购单id
                if (chemistry_UserIn.ChemistryId != null)
                {
                    var result = await db.ChemistryMonad.SingleOrDefaultAsync(p => p.Id == chemistry_UserIn.ChemistryId && p.IsDelete == false);
                    var ChemistryRoom = new ChemistryRoom { ChemistryId = (long)result.ChemistryId };
                    db.Entry(ChemistryRoom).State = System.Data.Entity.EntityState.Unchanged;
                    if (ChemistryRoom.RawNumber != null || result.QuasiPurchaseNumber != null)
                    {
                        ChemistryRoom.RawNumber = ChemistryRoom.RawNumber + (double)result.QuasiPurchaseNumber;

                        //修改采购单
                        result.is_or = 1;
                        db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(result).Property("is_or").IsModified = true;



                        if (await db.SaveChangesAsync() > 0)
                        {



                            return Json(new { code = 200, msg = "入库成功" });
                        }
                        return Json(new { code = 201, msg = "入库失败" });
                    }

                    return Json(new { code = 200, msg = "入库失败" });
                }

                return Json(new { code = 200, msg = "${0}不能为空" + chemistry_UserIn.ChemistryId });

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


        


    }
}
