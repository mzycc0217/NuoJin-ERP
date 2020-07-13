using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 销售实体类
    /// </summary>
    [Table("Sale")]
    public class Sale:EntityBase
    {
        /// <summary>
        /// 销售员Id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 实际销售
        /// </summary>
        [Required]
        public double ActualSale { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 销售总额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 出库信息Id
        /// </summary>
        [Required]
        public long OutOfStockId { get; set; }

        /// <summary>
        /// 销售合同
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 销售类型Id
        /// </summary>
        public long? SaleTypeId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 销售类型
        /// </summary>
        public virtual SaleType SaleType { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 出库信息
        /// </summary>
        public virtual OutOfStock OutOfStock { get; set; }
    }
}