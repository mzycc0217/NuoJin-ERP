using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 销售信息输出类
    /// </summary>
    public class SaleOutputDto:OutputBase
    {
        /// <summary>
        /// 销售信息Id
        /// </summary>
        public string SaleId { get; set; }

        /// <summary>
        /// 销售员Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 实际销售
        /// </summary>
        public double ActualSale { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 销售总额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 销售合同
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 出库信息Id
        /// </summary>
        public string OutOfStockId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 销售类型Id
        /// </summary>
        public string SaleTypeId { get; set; }

        /// <summary>
        /// 销售类型
        /// </summary>
        public SaleTypeOutputDto SaleType { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public  UserDetails User { get; set; }

        /// <summary>
        /// 出库信息
        /// </summary>
        public  OutOfStock OutOfStock { get; set; }
    }
}