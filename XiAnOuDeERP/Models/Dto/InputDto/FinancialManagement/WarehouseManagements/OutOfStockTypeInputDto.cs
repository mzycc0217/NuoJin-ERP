using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 出库类型输入类
    /// </summary>
    public class OutOfStockTypeInputDto
    {
        /// <summary>
        /// 出库类型Id
        /// </summary>
        public long OutOfStockId { get; set; }

        /// <summary>
        /// 出库类型名称
        /// </summary>
        public string OutOfStockName { get; set; }
    }
}