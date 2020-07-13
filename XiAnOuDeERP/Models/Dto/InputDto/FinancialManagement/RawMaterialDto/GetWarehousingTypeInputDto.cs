using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 获取入库类型输入类
    /// </summary>
    public class GetWarehousingTypeInputDto:InputBase
    {
        /// <summary>
        /// 入库类型Id
        /// </summary>
        public long? WarehousingId { get; set; }

        /// <summary>
        /// 入库类型名称
        /// </summary>
        public string Name { get; set; }
    }
}