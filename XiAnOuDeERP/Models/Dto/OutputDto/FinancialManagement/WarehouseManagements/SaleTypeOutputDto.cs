using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 销售类型输出类
    /// </summary>
    public class SaleTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 销售类型Id
        /// </summary>
        public string SaleTypeId { get; set; }

        /// <summary>
        /// 销售类型名称
        /// </summary>
        public string SaleTypeName { get; set; }
    }
}