using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 获取销售类型输入类
    /// </summary>
    public class GetSaleTypeInputDto:InputBase
    {
        /// <summary>
        /// 销售类型Id
        /// </summary>
        public long? SaleTypeId { get; set; }

        /// <summary>
        /// 销售类型名称
        /// </summary>
        public string SaeleTypeName { get; set; }
    }
}