using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 出库实体类
    /// </summary>
    [Table("OutOfStock")]
    public class OutOfStock :EntityBase
    {
        /// <summary>
        /// 原材料
        /// </summary>
        public long RawMaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public double Number { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public virtual RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public long ApplicantId { get; set; }

        /// <summary>
        /// 出库类型Id
        /// </summary>
        public long? OutOfStockTypeId { get; set; }

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
        /// 出库类型
        /// </summary>
        public virtual OutOfStockType OutOfStockType { get; set; }
    }
}