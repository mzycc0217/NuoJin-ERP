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
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.PurchaseInformation;
using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.FinancialManagement.PurchasingManagementAppService
{
    [AppAuthentication]
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
            if (input.PageSize != null && input.PageIndex != null&&!string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0&&p.Applicant.RealName.Contains(input.ApplicantRelName))
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());

               

              //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200,Count=result.Count(), data = result });
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
                  //  ApprovalTypeStr = p.ApprovalType.GetDescription(),
                  //  ArrivalTime = p.ArrivalTime,
                    SupplierId =  p.Supplier.Id.ToString(),
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
                    Z_RowType=p.Z_Raw.Z_RowType
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }

        /// <summary>
        /// 库管员获取可以直接领取的物料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetZpurChaseRaw(PurshOutDto input)
        {
            //获取所有采购申请单
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 1 && p.Applicant.RealName.Contains(input.ApplicantRelName))
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0&&p.is_or==1)
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
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
            //获取所有采购申请单
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 3 && p.Applicant.RealName.Contains(input.ApplicantRelName))
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 3)
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }

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
                Content_User content_User = new Content_User
                {
                    Purchase_Id= content_Users.Purchase_Id,
                    user_Id= content_Users.user_Id,
                    ContentDes=content_Users.ContentDes

                };
               
                db.Content_User.Add(content_User);
                var resul = new Purchase { Id = content_Users.Purchase_Id };
                db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;

                var resuls = await Task.Run(() => db.Purchases.SingleOrDefaultAsync(p => p.Id == content_Users.Purchase_Id));

                var res = new Z_Raw {Id= (long)resuls.RawId};
                db.Entry(res).State = System.Data.Entity.EntityState.Unchanged;
                res.Number = res.Number - resuls.ApplyNumber;
                resul.is_or = 0;
                if (await db.SaveChangesAsync()>0)
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
        /// <param name="content_Users"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetZpurChasePdst(Content_Users content_Users)
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                Content_User content_User = new Content_User
                {
                    Purchase_Id = content_Users.Purchase_Id,
                    user_Id = content_Users.user_Id,
                    ContentDes = content_Users.ContentDes

                };
                db.Content_User.Add(content_User);
                var resul = new Purchase { Id = content_Users.Purchase_Id };
                db.Entry(resul).State = System.Data.Entity.EntityState.Unchanged;
                resul.is_or = 5;
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
            if (input.PageSize != null && input.PageIndex != null && !string.IsNullOrWhiteSpace(input.ApplicantRelName))
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.RealName.Contains(input.ApplicantRelName)&&p.is_or==5)
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                //  var resul=db.Content_Users.Where(x=>x.Purchase_Id== res)
                return Json(new { code = 200, Count = result.Count(), data = result });
            }
            if (input.PageSize != null && input.PageIndex != null)
            {
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.is_or == 5)
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }


        /// <summary>
        /// 获取完成的采购单
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
                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0 && p.Applicant.Id== userId && p.is_or == 0)
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
        })
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
                })
                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
                return Json(new { code = 200, data = result });
            }
            return Json(new { code = 400 });
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="purshDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> EditPursh(PurshDto purshDto)
        {

            if (ModelState.IsValid)
            {
                try
                {

            
                var type = new Purchase() { Id = purshDto.Id };
                db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;

                if (!string.IsNullOrWhiteSpace(purshDto.Purpose))
                {
                    type.Purpose = purshDto.Purpose;
                }
                if (purshDto.ExpectArrivalTime!=null)
                {
                    type.ExpectArrivalTime = purshDto.ExpectArrivalTime;
                }

                if (purshDto.ApplyNumber!=null)
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
                    type.ProjectId = purshDto.ProjectId;
                }
                if (await db.SaveChangesAsync()>0)
                {
                    return Json(new { code = 200, msg = "修改成功" });
                }
                    else { 
                    return Json(new { code = 400, msg = "修改失败" });
                    }
              
                }
                catch (Exception ex)
                {

                    throw;
                }
                

            }
            else
            {
                return Json(new { code = 400, msg = "数据格式错误" });
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

            try
            {
                if (content_Users.Purchase_Id!=null)
                {
                  var result =await Task.Run(()=> db.Content_User.Where(p => p.Purchase_Id == content_Users.Purchase_Id)
                  .Include(p => p.UserDetails).Include(p => p.Purchase).ToListAsync());

                    return Json(new { code = 200, data = result });

                }

                //获取这个人审核的所有内容
                if (content_Users.user_Id!=null)
                {
                  var resul = await Task.Run(() => db.Content_User.Where(p => p.user_Id == content_Users.user_Id)
                .Include(p=>p.UserDetails).Include(p=>p.Purchase).ToListAsync());
                    return Json(new { code = 200, data = resul });
                }

                return Json(new { code = 201, msg = "无此参数数据" });
                


            }
            catch (Exception)
            {

                throw;
            }


        }


        

    }
}
