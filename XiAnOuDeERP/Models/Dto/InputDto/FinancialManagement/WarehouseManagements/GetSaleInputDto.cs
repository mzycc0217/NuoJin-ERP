using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 获取销售信息输入类
    /// </summary>
    public class GetSaleInputDto:InputBase
    {
        /// <summary>
        /// 销售信息Id
        /// </summary>
        public long? SaleId { get; set; }

        /// <summary>
        /// 销售员Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 基础数据Id
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 基础数据名称
        /// </summary>
        public string RawMaterialName { get; set; }
    }
}