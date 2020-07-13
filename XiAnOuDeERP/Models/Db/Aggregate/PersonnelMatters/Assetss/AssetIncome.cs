using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产收入实体类
    /// </summary>
    [Table("AssetIncome")]
    public class AssetIncome:EntityBase
    {
        /// <summary>
        /// 收入人Id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 销售信息Id
        /// </summary>
        public long? SaleId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 销售信息
        /// </summary>
        public virtual Sale Sale { get; set; }

        /// <summary>
        /// 收入人
        /// </summary>
        public virtual UserDetails User { get; set; }
    }
}