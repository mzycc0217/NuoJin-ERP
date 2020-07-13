using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
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
                return Json(new { code = 200, data = result });
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

    }
}
