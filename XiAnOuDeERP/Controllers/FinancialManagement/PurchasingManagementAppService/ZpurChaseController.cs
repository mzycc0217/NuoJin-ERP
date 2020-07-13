//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using XiAnOuDeERP.Models.Db;
//using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto;
//using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
//using XiAnOuDeERP.Models.Util;

//namespace XiAnOuDeERP.Controllers.FinancialManagement.PurchasingManagementAppService
//{
//    [AppAuthentication]
//    [RoutePrefix("api/ZpurChase")]
//    public class ZpurChaseController : ApiController
//    {
//        XiAnOuDeContext db = new XiAnOuDeContext();
//        /// <summary>
//        /// 获取所有的采购申请
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        public async Task<IHttpActionResult> GetZpurChase(GetPurchaseListInputDto input)
//        {
//            //获取所有采购申请单
          
//            if (input.PageSize!=null&&input.PageIndex!=null)
//            {
//          //  var result = db.Purchases.Where(p => p.Id > 0).OrderByDescending(p=>p.CreateDate).ToList();
//                //PurchaseId = item.Id.ToString(),
//                //    Amount = item.Amount,
//                //    ApplicantRemarks = item.ApplicantRemarks,
//                //    ApplyNumber = item.ApplyNumber,
//                //    ApplyTime = item.ApplyTime,
//                //    ApprovalType = item.ApprovalType,
//                //    ApprovalTypeStr = item.ApprovalType.GetDescription(),
//                //    ArrivalTime = item.ArrivalTime,
//                //    SupplierId =
//                //    item.SupplierId.ToString(),
//                //    Supplier = item.Supplier,
//                //    CompanyId = item.RawMaterial.Company.ToString(),
//                //    Company = item.RawMaterial.Company,
//                //    Enclosure = item.Enclosure,
//                //    Price = item.Price,
//                //    PurchaseContract = item.PurchaseContract,
//                //    PurchaseTime = item.PurchaseTime,
//                //    Purpose = item.Purpose,
//                //    QuasiPurchaseNumber = item.QuasiPurchaseNumber,
//                //    WaybillNumber = item.WaybillNumber,
//                //    ApplicantId = item.ApplicantId.ToString(),
//                //    Applicant = item.Applicant,
//                //    ApprovalDesc = item.ApprovalDesc,
//                //    ExpectArrivalTime = item.ExpectArrivalTime,
//                //    ProjectId = item.ProjectId.ToString(),
//                //    Project = item.Project,
//                //    RawMaterialId = item.RawMaterialId.ToString(),
//                //    RawMaterial = item.RawMaterial,
//                //    DepartmentId = item.Applicant.User.DepartmentId.ToString(),
//                //    Department = item.Applicant.User.Department,
//                //    UpdateDate = item.UpdateDate,
//                //    CreateDate = item.CreateDate,
//                //    WarehousingType = item.RawMaterial.WarehousingType,
//                //    WarehousingTypeId = item.RawMaterial.WarehousingTypeId.ToString()

//                var result = await Task.Run(() => db.Purchases.Where(p => p.Id > 0)
//                .Select(p=>new PurshOutDto
//                {

//                    //PurchaseId = p.Id.ToString(),
//                    //Amount = p.Amount,
//                    //ApplicantRemarks = p.ApplicantRemarks,
//                    //ApplyNumber = p.ApplyNumber,
//                    //ApplyTime = p.ApplyTime,
//                    //ApprovalType = p.ApprovalType,
//                    //ApprovalTypeStr = p.ApprovalType.GetDescription(),
//                    //ArrivalTime = p.ArrivalTime,
//                    //SupplierId =
//                    //    p.Supplier.Id.ToString(),
//                    //Supplier = p.Supplier,
//                    //CompanyId = p.RawMaterial.Company.ToString(),
//                    //Company = p.RawMaterial.Company,
//                    //Enclosure = p.Enclosure,
//                    //Price = p.Price,
//                    //PurchaseContract = p.PurchaseContract,
//                    //PurchaseTime = p.PurchaseTime,
//                    //Purpose = p.Purpose,
//                    //QuasiPurchaseNumber = p.QuasiPurchaseNumber,
//                    //WaybillNumber = p.WaybillNumber,
//                    //ApplicantId = p.ApplicantId.ToString(),
//                    //Applicant = p.Applicant,
//                    //ApprovalDesc = p.ApprovalDesc,
//                    //ExpectArrivalTime = p.ExpectArrivalTime,
//                    //ProjectId = p.ProjectId.ToString(),
//                    //Project = p.Project,
//                    //RawMaterialId = p.RawMaterialId.ToString(),
//                    //z_Raw = p.row,
//                    //DepartmentId = p.Applicant.User.DepartmentId.ToString(),
//                    //Department = p.Applicant.User.Department,
//                    //UpdateDate = p.UpdateDate,
//                    //CreateDate = p.CreateDate,
//                    //WarehousingType = p.RawMaterial.WarehousingType,
//                    //WarehousingTypeId = p.RawMaterial.WarehousingTypeId.ToString()


//                })
                
//                .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize).ToList());
//                return Json(new { code = 200, data = result });
//            }
//            return Json(new { code = 400 });
//        }

//    }
//}
