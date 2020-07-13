using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto
{
    public class PurshOutDto
    {
         /// <summary>
        /// 设备采购申请Id
        /// </summary>
        public long PurchaseId { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long ApplicantId { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// 原材料Id
        /// </summary>
        public long RawMaterialId { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public double ApplyNumber { get; set; }

        /// <summary>
        /// 准购数量
        /// </summary>
        public double QuasiPurchaseNumber { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

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
        /// 期望到货日期
        /// </summary>
        public DateTime? ExpectArrivalTime { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 供货商Id
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// 支出备注
        /// </summary>
        public string AssetExpenditureDesc { get; set; }

        /// <summary>
        /// 审核人id
        /// </summary>
        public long? User_id { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public  UserDetails User_ID { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public  UserDetails Applicant { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public Z_Raw z_Raw { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public  Project Project { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public  Supplier Supplier { get; set; }
    }
}