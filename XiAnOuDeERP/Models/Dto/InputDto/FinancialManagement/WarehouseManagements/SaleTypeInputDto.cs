using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 销售类型输入类
    /// </summary>
    public class SaleTypeInputDto
    {
        /// <summary>
        /// 销售类型Id
        /// </summary>
        public long SaleTypeId { get; set; }

        /// <summary>
        /// 销售类型名称
        /// </summary>
        public string SaleTypeName { get; set; }
    }
}