using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 获取出库类型输入类
    /// </summary>
    public class GetOutOfStockTypeInputDto:InputBase
    {
        /// <summary>
        /// 出库类型Id
        /// </summary>
        public long? OutOfStockTypeId { get; set; }

        /// <summary>
        /// 出库类型名称
        /// </summary>
        public string OutOfStockTypeName { get; set; }
    }
}