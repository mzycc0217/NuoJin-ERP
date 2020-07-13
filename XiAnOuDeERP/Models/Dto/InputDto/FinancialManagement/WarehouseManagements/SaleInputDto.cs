using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 添加销售输入类
    /// </summary>
    public class SaleInputDto
    {
        /// <summary>
        /// 销售员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 销售合同
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 出库信息Id
        /// </summary>
        public long OutOfStockId { get; set; }

        /// <summary>
        /// 收入备注
        /// </summary>
        public string AssetIncomeDesc { get; set; }

        /// <summary>
        /// 销售类型Id
        /// </summary>
        public long SaleTypeId { get; set; }
    }
}