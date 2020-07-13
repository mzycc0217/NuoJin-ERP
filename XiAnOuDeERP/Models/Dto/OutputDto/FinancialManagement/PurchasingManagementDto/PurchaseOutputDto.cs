using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 采购申请输出类
    /// </summary>
    public class PurchaseOutputDto:OutputBase
    {
        /// <summary>
        /// 采购申请Id
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// 期望到货日期
        /// </summary>
        public DateTime? ExpectArrivalTime { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public double? ApplyNumber { get; set; }

        /// <summary>
        /// 准购数量
        /// </summary>
        public double? QuasiPurchaseNumber { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Enclosure { get; set; }

        /// <summary>
        /// 申请人备注说明
        /// </summary>
        public string ApplicantRemarks { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime? PurchaseTime { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { get; set; }

        /// <summary>
        /// 采购合同
        /// </summary>
        public string PurchaseContract { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 审核状态Str
        /// </summary>
        public string ApprovalTypeStr { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public UserDetails Applicant { get; set; }

        /// <summary>
        /// 原材料编码
        /// </summary>
        public string RawId { get; set; }
        
        /// <summary>s
        /// 原材料
        /// </summary>
        public RawMaterial RawMaterial { get; set; }
        /// <summary>
        /// 原材料
        /// </summary>
        public string RawMaterialId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public Company  Company { get; set; }

        /// <summary>
        /// 申购部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 供货商Id
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 入库类型Id
        /// </summary>
        public string WarehousingTypeId { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public WarehousingType WarehousingType { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// 申购部门
        /// </summary>
        public Department Department { get; set; }
    }
}