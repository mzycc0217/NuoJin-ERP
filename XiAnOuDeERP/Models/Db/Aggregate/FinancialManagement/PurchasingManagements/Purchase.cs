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

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements
{
    /// <summary>
    /// 采购实体类
    /// </summary>
    [Table("Purchase")]
    public class Purchase : EntityBase
    {
        /// <summary>
        /// 用途
        /// </summary>
        [StringLength(255)]
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
        [StringLength(255)]
        public string Enclosure { get; set; }

        /// <summary>
        /// 申请人备注说明
        /// </summary>
        
        [StringLength(255)]
        public string ApplicantRemarks { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 申请是否完成 3代表完成
        /// </summary>
        public int is_or { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime? PurchaseTime { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [StringLength(255)]
        public string WaybillNumber { get; set; }

        /// <summary>
        /// 采购合同
        /// </summary>
        [StringLength(255)]
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
        /// 审核备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long? ApplicantId { get; set; }

        /// <summary>
        /// 审阅人id
        /// </summary>
        public long? User_Id { get; set; }
        /// <summary>
        /// 删除的（原来的）
        /// </summary>
       // [Required]
        public long? RawMaterialId { get; set; }
        /// <summary>
        /// 原材料Id
        /// </summary>
       // [Required]
        public long? RawId { get; set; }

        public virtual Z_Raw Z_Raw { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 供货商Id
        /// </summary>
        public long? SupplierId { get; set; }

       
        /// <summary>
        /// 审核流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 当前审核
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserDetails User_ID { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public virtual UserDetails Applicant { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public virtual RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}