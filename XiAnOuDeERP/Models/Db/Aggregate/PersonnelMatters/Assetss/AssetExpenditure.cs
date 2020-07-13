using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产支出实体类
    /// </summary>
    [Table("AssetExpenditure")]
    public class AssetExpenditure:EntityBase
    {
        /// <summary>
        /// 支出人Id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public long? PurchaseId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 支出人
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 采购
        /// </summary>
        public virtual Purchase Purchase { get; set; }
    }
}