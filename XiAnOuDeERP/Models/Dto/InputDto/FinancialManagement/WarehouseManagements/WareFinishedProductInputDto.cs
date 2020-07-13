using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 成品入库输入类
    /// </summary>
    public class WareFinishedProductInputDto
    {
        /// <summary>
        /// 原材料Id
        /// </summary>
        public long RawMaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }
    }
}