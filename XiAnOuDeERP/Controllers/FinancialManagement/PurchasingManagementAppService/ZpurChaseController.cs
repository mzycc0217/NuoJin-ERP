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
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Dto.My_FlowDto;
using XiAnOuDeERP.Models.Dto.OutPutDecimal.InDto;
using XiAnOuDeERP.Models.Dto.OutPutDecimal.OutDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.PurchaseInformation;
using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.FinancialManagement.PurchasingManagementAppService
{
    
    [AppAuthentication]
  // [ExperAuthentication]
    [RoutePrefix("api/ZpurChase")]
    public class ZpurChaseController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 获取所有的采购申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChase(PurshOutDto input)
        {
            //获取所有采购申请单
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.RealName.Contains(input.ApplicantRelName))
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                   
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



                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0)
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                   // AssetExpenditureDesc = p.AssetExpenditureDesc,
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
        /// 库管员获取可以直接领取的物料(出库)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseRaw(PurshOutDto input)
        {
            //获取所有采购申请单
           var resul = await Task.Run(()=> db.Purchases.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false));
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 1 && p.IsDelete == false ||p.Applicant.RealName.Contains(input.ApplicantRelName))
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                  //  AssetExpenditureDesc = p.AssetExpenditureDesc,
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
                return Json(new { code = 200, data = result,Count= result.Count() });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p =>  p.is_or == 1 && p.IsDelete == false)
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                   // AssetExpenditureDesc = p.AssetExpenditureDesc,
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
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, data = result, Count = resul.CountAsync().Result});/*, */
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 领导获取的部门获取的申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseP(PurshOutDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            //获取当前领导的部门
            var resule = await db.UserDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == userId);
            var resul = await db.User.AsNoTracking().FirstOrDefaultAsync(p => p.Id == resule.UserId);


            //获取所有采购申请单
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false ||p.Applicant.RealName.Contains(input.ApplicantRelName) && p.Applicant.User.DepartmentId == resul.DepartmentId)
                //  .Include(p=>p.Applicant.User.DepartmentId== resul.DepartmentId)
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
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 2 && p.IsDelete == false && p.Applicant.User.DepartmentId == resul.DepartmentId)
                //   .Include(p => p.Applicant.User.DepartmentId == resul.DepartmentId)
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



        private long? RawId { get; set; }
        private double? ApplyNumber { get; set; }
        /// <summary>
        /// 库管员签核申请单
        /// </summary>
        /// <param name="content_Users"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetZpurChasePds(Content_Users content_Users)
        {

            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
             
                var resuls =await db.Purchases.SingleOrDefaultAsync(p => p.Id == content_Users.Purchase_Id);
                long Enportid;//仓库id
                if (content_Users.enportid ==0)
                {
                    var result = await db.RawRooms
                    .FirstOrDefaultAsync(p => p.RawId==resuls.RawId&&p.RawNumber>=resuls.QuasiPurchaseNumber);
                    if (result==null)
                    {
                        resuls.is_or = 5;
                        await db.SaveChangesAsync();
                        return Json(new { code = 200, msg = "仓库数量不足,重新采购" });
                    }
                    Enportid =(long)result.EntrepotId;
                    result.RawNumber = result.RawNumber - (double)resuls.QuasiPurchaseNumber;
                    //db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    //db.Entry(result).Property("RawNumber").IsModified = true;

                }
                else
                {
                    Enportid = content_Users.enportid;
                    var res = await db.RawRooms
                    .FirstOrDefaultAsync(p => p.RawId == resuls.RawId && p.EntrepotId == Enportid);
                 //   var res = new RawRoom { RawId = (long)resuls.RawId,EntrepotId= Enportid };
                    db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                    res.RawNumber = res.RawNumber - (double)resuls.QuasiPurchaseNumber;
                }
                     resuls.is_or = 0;
                db.Entry(resuls).State = System.Data.Entity.EntityState.Modified;
                db.Entry(resuls).Property("is_or").IsModified = true;
                //添加领料明细
                Raw_UserDetils raw_UserDetils = new Raw_UserDetils
                {
                    Id = IdentityManager.NewId(),
                    RawId = (long)resuls.RawId,
                    User_id = resuls.Applicant.Id,
                    OutIutRoom = 1,//出库状态,
                    is_or=1,//显示为出库状态（个人获取到的）
                   //添加仓库（这里）
                    entrepotid= Enportid,
                    RawNumber = (double)resuls.QuasiPurchaseNumber,
                    GetRawTime = DateTime.Now
                };
                db.Raw_UserDetils.Add(raw_UserDetils);
                 //添加签核人
                Pursh_User pursh_User = new Pursh_User
                {
                    Id = IdentityManager.NewId(),
                    Purchase_Id = content_Users.Purchase_Id,
                    user_Id = userId,
                    ContentDes = content_Users.ContentDes
                };
                db.Pursh_User.Add(pursh_User);

                // RawId = resuls.RawId;
                //ApplyNumber = resuls.QuasiPurchaseNumber;

                //var resul = new Purchase { Id = content_Users.Purchase_Id };
                //db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;
           






                if (await db.SaveChangesAsync() > 0)
                {


                    return Json(new { code = 200, msg = "出库成功" });
                }
                else
                {
                    return Json(new { code = 400, msg = "出库失败" });
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
        /// <param name="content_Users"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetZpurChasePdst(Content_Users content_Users)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Pursh_User pursh_User = new Pursh_User
                {
                    Id = IdentityManager.NewId(),
                    Purchase_Id = content_Users.Purchase_Id,
                    user_Id = userId,
                    ContentDes = content_Users.ContentDes

                };

                db.Pursh_User.Add(pursh_User);
                var resuls =await db.Purchases.SingleOrDefaultAsync(p => p.Id == content_Users.Purchase_Id);
             
                 resuls.is_or = 5;
                //var resul = new Purchase { Id = content_Users.Purchase_Id };
                //db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;




                //var res = new Z_Raw { Id = (long)RawId };
                //db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                //res.Number = res.Number - ApplyNumber;
               
                   
             

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
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseCaigou(PurshOutDto input)
        {
            //获取所有采购申请单
            var resul = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.IsDelete == false  && p.is_or == 5));
            
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.IsDelete == false || p.Applicant.RealName.Contains(input.ApplicantRelName) && p.is_or == 5)
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
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = resul.CountAsync(), data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 5 && p.IsDelete == false)
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
        /// 获取完成的采购单（获取自己名下的所有采购单input.PageSize == -2input.PageIndex == -2）（获取自己名下的已经完成的所有采购单input.PageSize == -1input.PageIndex == -1）
        ///获取自己名下未完成的采购单（获取自己名下的所有采购单input.PageSize == -3input.PageIndex == -3）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseCaigoud(PurshOutDto input)
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
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.IsDelete == false)
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
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0 && p.IsDelete == false)
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
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.IsDelete == false || p.Applicant.RealName.Contains(input.ApplicantRelName) && p.is_or == 0)
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
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 0 && p.IsDelete == false)
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
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseOneCaigoud(PurshOutDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var resul = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or == 0 && p.IsDelete == false));
            if (input.PageSize != null && input.PageIndex != null)
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
    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = resul.CountAsync(), data = result });
            }
            return Json(new { code = 201});
        }

        /// <summary>
        /// 获取自己名下的所有采购单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseTwoCaigoud(PurshOutDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var resul = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.IsDelete == false));
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.IsDelete == false)
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
    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = resul.CountAsync(), data = result });
            }
            return Json(new { code = 201 });
        }


        /// <summary>
        /// 获取自己名下未完成的所有采购单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseThreeCaigoud(PurshOutDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var resultp = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId && p.is_or != 0 && p.IsDelete == false));
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id == userId&&p.is_or != 0 && p.IsDelete == false)
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
    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = resultp.CountAsync(), data = result });
            }
            return Json(new { code = 201 });
        }



        /// <summary>
        /// 获取自己名下的被驳回的采购单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRejectPursh(PurshOutDto input)
        { 
            var userId = ((UserIdentity)User.Identity).UserId;
            var resul = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.IsDelete == false && p.Applicant.Id == userId && p.is_or == 50));

          
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.IsDelete == false && p.Applicant.Id == userId && p.is_or == 50)
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
    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, Count = resul.CountAsync().Result, data = result });
            }
            return Json(new { code = 201 });
        }




        /// <summary>
        /// 修改（完善采购单）
        /// </summary>
        /// <param name="purshDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> EditPursh(PurshDto purshDto)
        {

          
                try
                {


                    var type = new Purchase() { Id = purshDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;

                    if (!string.IsNullOrWhiteSpace(purshDto.Purpose))
                    {
                        type.Purpose = purshDto.Purpose;
                    }
                    if (purshDto.ExpectArrivalTime != null)
                    {
                        type.ExpectArrivalTime = purshDto.ExpectArrivalTime;
                    }

                    if (purshDto.ApplyNumber != null)
                    {
                        type.ApplyNumber = purshDto.ApplyNumber;
                    }

                    if (purshDto.QuasiPurchaseNumber != null)
                    {
                        type.QuasiPurchaseNumber = purshDto.QuasiPurchaseNumber;
                    }
                    if (purshDto.Price != null)
                    {
                        type.Price = purshDto.Price;
                    }
                    if (purshDto.Amount != null)
                    {
                        type.Amount = purshDto.Amount;
                    }
                    if (!string.IsNullOrWhiteSpace(purshDto.Enclosure))
                    {
                        type.Enclosure = purshDto.Enclosure;
                    }
                    if (!string.IsNullOrWhiteSpace(purshDto.ApplicantRemarks))
                    {
                        type.ApplicantRemarks = purshDto.ApplicantRemarks;
                    }
                    if (!string.IsNullOrWhiteSpace(purshDto.WaybillNumber))
                    {
                        type.WaybillNumber = purshDto.WaybillNumber;
                    }
                    if (purshDto.ArrivalTime != null)
                    {
                        type.ArrivalTime = purshDto.ArrivalTime;
                    }
                    if (!string.IsNullOrWhiteSpace(purshDto.PurchaseContract))
                    {
                        type.PurchaseContract = purshDto.PurchaseContract;
                    }
                if (purshDto.PurchaseAmount!=null)
                {
                    type.PurchaseAmount = purshDto.PurchaseAmount;
                }
                if (purshDto.RawId != null)
                    {
                        type.RawId = purshDto.RawId;
                    }
                    if (purshDto.SupplierId != null)
                    {
                        type.SupplierId = purshDto.SupplierId;
                    }
                    if (purshDto.ProjectId != null)
                    {
                        type.ProjectId = (long)purshDto.ProjectId;
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
        /// <param name="content_Users"></param>
        /// <returns></returns>

        public async Task<IHttpActionResult> SurePursh(Content_Users content_Users)
        {
            try
            { 
            var userId = ((UserIdentity)User.Identity).UserId;
            Pursh_User pursh_User = new Pursh_User
            {
                Id = IdentityManager.NewId(),
                Purchase_Id = content_Users.Purchase_Id,
                user_Id = userId,
                ContentDes = content_Users.ContentDes

            };
            db.Pursh_User.Add(pursh_User);

                var result = new Purchase { Id = content_Users.Purchase_Id };

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
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetAddPursh(PurshOutDto input)
        {
            //获取所有采购申请单
            var resuls = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 6 && p.IsDelete == false));

            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 6 && p.IsDelete == false || p.Applicant.RealName.Contains(input.ApplicantRelName))
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
                }).OrderBy(p=>p.RawId)
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, data = result, Count = resuls.CountAsync() });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 6 && p.IsDelete == false)
                .Select(p => new PurshOutDto
                {
                    PurchaseId = p.Id.ToString(),
                    Amount = p.Amount,
                    ApplicantRemarks = p.ApplicantRemarks,
                    ApplyNumber = p.ApplyNumber,
                    ApplyTime = p.ApplyTime,
                    // ApprovalType = p.ApprovalType,
                    // ApprovalTypeStr = p.ApprovalType.GetDescription(),
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
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());
                return Json(new { code = 200, data = result, Count= resuls.CountAsync().Result });
            }
            return Json(new { code = 400 });

        }


        /// <summary>
        /// 库房的入库申请（采购单）添加入库(入库)
        /// </summary>
        /// <param name="content_Userd"></param>
        /// <returns></returns>
        [HttpPost]
       
        public async Task<IHttpActionResult> AddPursh(Content_Userd content_Userd)
        {
            try
            {
                if (content_Userd.Purchase_Id != null)
                {
                    var result =await db.Purchases.FirstOrDefaultAsync(p => p.Id == content_Userd.Purchase_Id && p.IsDelete == false);
                    //修改采购单
                    result.is_or = 1;
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(result).Property("is_or").IsModified = true;
                   
                    var results = await Task.Run(()=> db.RawRooms
                        .FirstOrDefaultAsync (p => p.RawId == result.RawId&&p.del_or==false)
                        );
                    if (content_Userd.enportid != 0)
                    {
                        results = await Task.Run(() => db.RawRooms.FirstOrDefaultAsync(p => p.EntrepotId == content_Userd.enportid));  
                        if (results==null)
                    {
                        return Json(new { code = 201, msg = "请选对仓库，这个仓库中没有数据" });
                    }
                    }
                 
                   if ((double)result.PurchaseAmount!=null)
                    {
                       results.RawNumber = results.RawNumber + (double)result.PurchaseAmount;   
                      //  db.Entry(results).State = System.Data.Entity.EntityState.Modified;
                     //  db.Entry(results).Property("RawNumber").IsModified = true;
                    }
                   

                    //if (results==null)
                    //{
                    //    RawRoom rawRoom = new RawRoom
                    //    {
                    //        Id = IdentityManager.NewId(),
                    //        RawId = (long)result.RawId,
                    //        RawNumber = (double)result.PurchaseAmount,
                    //        EntrepotId = content_Userd.enportid

                    //    };
                    //    db.RawRooms.Add(rawRoom);
                    //}
                    //results.RawNumber = results.RawNumber + (double)result.PurchaseAmount;
                    //db.Entry(results).State = System.Data.Entity.EntityState.Modified;
                    //db.Entry(results).Property("RawNumber").IsModified = true;


                    //  var RawRoom = new RawRoom { RawId = (long)result.RawId };
                    //     db.Entry(RawRoom).State = System.Data.Entity.EntityState.Unchanged;
                    //    if (RawRoom.RawNumber != null||result.QuasiPurchaseNumber!=null)
                    //   {
                    //      RawRoom.RawNumber = RawRoom.RawNumber + (double)result.QuasiPurchaseNumber;
                 

                   
                    
                  
                     
                 



                    if (await db.SaveChangesAsync()>0)
                    {



                        return Json(new { code = 200, msg = "入库成功" });
                    }
                        return Json(new { code = 201, msg = "入库失败" });
                 }

               //     return Json(new { code = 200, msg = "入库失败" });
              //  }

               return Json(new { code = 200, msg = "${0}不能为空"+ (content_Userd.Purchase_Id  )});

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取审核人
        /// </summary>
        /// <param name="content_Users"></param>
        /// <returns></returns>
        [HttpPost]
   
        public async Task<IHttpActionResult> GetPursh(Content_Users content_Users)
        {
            //获取这个采购单下的审核人
          
            try
            {
                if (content_Users.Purchase_Id != null)
                {
                    var result = await Task.Run(() => db.Pursh_User.Where(p => p.Purchase_Id == content_Users.Purchase_Id)
                    //.Select(p =>new Content_Users { 
                    //UserDetails=p.UserDetails,
                    //ContentDes=p.ContentDes
                    //}).ToListAsync());
             .Include(p => p.UserDetails).Include(p => p.Purchase)) ;
                  
                  return Json(new { code = 200, data = result });

                }

                //获取这个人审核的所有内容
                if (content_Users.user_Id != null)
                {
                    var resul = await Task.Run(() => db.Pursh_User.Where(p => p.user_Id == content_Users.user_Id)
                   .Include(p => p.UserDetails).Include(p => p.Purchase).ToListAsync());
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
        /// <param name="content_Users"></param>
        /// <returns></returns>
        /// 

        [HttpPost]

        public async Task<IHttpActionResult> RejetPursh(Content_Users content_Users)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            Pursh_User pursh_User = new Pursh_User
            {
                Id = IdentityManager.NewId(),
                Purchase_Id = content_Users.Purchase_Id,
                user_Id = userId,
                ContentDes = content_Users.ContentDes

            };

            db.Pursh_User.Add(pursh_User);

            Purchase Purchases = new Purchase() { Id = content_Users.Purchase_Id };
            db.Entry(Purchases).State = System.Data.Entity.EntityState.Unchanged;

            //  var resuls =await db.Purchases.AsNoTracking().FirstOrDefaultAsync(p => p.Id == content_Users.Purchase_Id);
            Purchases.is_or = 50;//表示驳回



            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "驳回完成" });
            }

            return Json(new { code = 201, msg = "驳回失败" });

        }



        /// <summary>
        /// 删除采购申请单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemovePursh(PurshOutDto input)
        {

            try
            {
                foreach (var item in input.Del_Id)
                {
                    var Purchase = new Purchase { Id = item };
                   // var RawMaterial = new RawMaterial { Id = (long)Purchase.RawMaterialId };
                  //  var Pursh_User = new Pursh_User { Id = item };

                 //   db.Entry(RawMaterial).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(Purchase).State = System.Data.Entity.EntityState.Unchanged;
                    Purchase.IsDelete = true;
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
        public async Task<List<Raw_UserDetilsOutDto>> GetBackgePursh(InputBase input)
        {
            try
            {
               var userId = ((UserIdentity)User.Identity).UserId;
            var result = await Task.Run(() => db.Raw_UserDetils.AsNoTracking().Where(p => p.User_id == userId && p.del_or == false&&p.is_or==1)
            .Select(p => new Raw_UserDetilsOutDto
            {
                Id = (p.Id).ToString(),
                RawId = p.z_Raw.Id.ToString(),
                User_id = p.userDetails.Id.ToString(),
                RawNumber=p.RawNumber,
                OutIutRoom =p.OutIutRoom,
                GetRawTime =p.GetRawTime,
                userDetails = p.userDetails,
                entrepot=p.entrepot,
                z_Raw=p.z_Raw
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
        /// <param name="raw_UserDetilsDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetBackgePursh(Raw_UserDetilsDto raw_UserDetilsDto)
        {

            try
            {
                    var result = await Task.Run(()=> db.Raw_UserDetils.SingleOrDefaultAsync(p => p.Id == raw_UserDetilsDto.Id));
                   result.OutIutRoom = 2;//出库状态2
                     result.is_or = 2;
                if (await db.SaveChangesAsync()>0)
                {
                    return Json(new { code = 200, msg = "退库成功"});
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
        public async Task<List<Raw_UserDetilsOutDto>> GetRejectPurshs(InputBase input)
        {  
            //var resu = await Task.Run(()=> db.Raw_UserDetils.AsNoTracking().Where(p => p.Id > 0));
          
             //   var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Raw_UserDetils.AsNoTracking().Where( p=> p.del_or == false && p.is_or == 2)
                .Select(p => new Raw_UserDetilsOutDto
                {
                    Id = (p.Id).ToString(),
                    RawId = p.z_Raw.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    RawNumber = p.RawNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetRawTime = p.GetRawTime,
                    
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Raw = p.z_Raw
                }).OrderBy(p => p.Id)
                    .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToListAsync());

                    //   new Raw_UserDetilsOutDto { Count = result.Count() };
                return result;
            
        


        }



        /// <summary>
        /// 获取出库明细(已经完成的)
        /// (所有的)
        /// </summary>
        /// <param name="raw_UserDetilsDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<Raw_UserDetilsOutDto>> GetBackPursh(Raw_UserDetilsDto raw_UserDetilsDto)
        {

            try
            {
               // var userId = ((UserIdentity)User.Identity).UserId;
                var result = await Task.Run(() => db.Raw_UserDetils.AsNoTracking().Where(p =>p.del_or == false || p.userDetails.RealName.Contains(raw_UserDetilsDto.RelName) || p.z_Raw.Name.Contains(raw_UserDetilsDto.Name))
                .Select(p => new Raw_UserDetilsOutDto
                {
                    Id = (p.Id).ToString(),
                    RawId = p.z_Raw.Id.ToString(),
                    User_id = p.userDetails.Id.ToString(),
                    RawNumber = p.RawNumber,
                    OutIutRoom = p.OutIutRoom,
                    GetRawTime = p.GetRawTime,
                    userDetails = p.userDetails,
                    entrepot = p.entrepot,
                    z_Raw = p.z_Raw
                }).OrderBy(p => p.Id)
                    .Skip((raw_UserDetilsDto.PageIndex * raw_UserDetilsDto.PageSize) - raw_UserDetilsDto.PageSize).Take(raw_UserDetilsDto.PageSize).ToListAsync());

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
        /// <param name = "raw_UserDetilsDto" ></param>
        /// <returns></returns >
        [HttpPost]
        public async Task<IHttpActionResult> BackPursh(Raw_UserDetilsDto raw_UserDetilsDto)
        {
            try
            {
                if ( raw_UserDetilsDto.Id!=null)
                {
                    var result = await Task.Run(() => db.Raw_UserDetils.SingleOrDefaultAsync(p => p.Id == raw_UserDetilsDto.Id));
                    if (result != null)
                    {
                        var reus = await Task.Run(() => db.RawRooms
                        .FirstOrDefaultAsync(p => p.RawId == result.RawId&&p.z_Raw.IsDelete==false&&p.EntrepotId==result.entrepotid));
                        reus.RawNumber = reus.RawNumber + result.RawNumber;
                        result.OutIutRoom = 3;//入库完成
                        result.is_or = 3;
                       //////var reust = await Task.Run(() => db.Purchases
                       //////.SingleOrDefaultAsync(p => p.RawId == result.RawId && p.Applicant.Id==result.User_id && p.IsDelete == false));
                       // reust.is_or = 20;//退库完成
                        if (await db.SaveChangesAsync()>0)
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
        /// <param name="rawRoomOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<RawRoomOutDto>> BackEntitys(RawRoomOutDto rawRoomOutDto)
        {
            var result = await Task.Run(() => db.RawRooms.AsNoTracking()
            .Where(p => p.RawId == rawRoomOutDto.RawId && p.del_or == false)
            .Select(p => new RawRoomOutDto
            {
                RawId = p.z_Raw.Id,
               
                EntrepotId =p.entrepot.Id.ToString(),
                 entrepot=p.entrepot
            }) .ToListAsync()) ;
           

            return result;
           

        }
    }
}
