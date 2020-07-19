using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.OutEntropt
{
    /// <summary>
    /// 成品半成品申请单
    /// </summary>
    [Table("FnishedProductMonad")]
    public class FnishedProductMonad : EntityBase
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
        /// 获取数量
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
        /// 采购数量
        /// </summary>
        public double? PurchaseAmount { get; set; }
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
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long? ApplicantId { get; set; }


        /// <summary>
        /// 供货商Id
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// 化学用品
        /// </summary>

        public long? FnishedProductId { get; set; }

        public virtual Z_FnishedProduct Z_FnishedProduct { get; set; }



        /// <summary>
        /// 申请人
        /// </summary>
        public virtual UserDetails Applicant { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}