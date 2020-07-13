using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;
using System.ComponentModel.DataAnnotations;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 入库实体类
    /// </summary>
    [Table("Warehousing")]
    public class Warehousing:EntityBase
    {
        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public double Number { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        [Required]
        public long ApplicantId { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public long? PurchaseId { get; set; }

        /// <summary>
        /// 基础资料Id
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 审核流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 当前审核
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public virtual UserDetails Applicant { get; set; }

        /// <summary>
        /// 采购申请单
        /// </summary>
        public virtual Purchase Purchase { get; set; }

        /// <summary>
        /// 基础资料
        /// </summary>
        public virtual RawMaterial RawMaterial { get; set; }
    }
}