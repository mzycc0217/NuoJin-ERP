using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 出库类型输出类
    /// </summary>
    public class OutOfStockTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 出库类型Id
        /// </summary>
        public string OutOfStockId { get; set; }

        /// <summary>
        /// 出库类型名称
        /// </summary>
        public string OutOfStockName { get; set; }
    }
}