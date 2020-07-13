using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 入库类型输出类
    /// </summary>
    public class WarehousingTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 入库类型Id
        /// </summary>
        public string WarehousingId { get; set; }

        /// <summary>
        /// 入库类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 入库类型备注
        /// </summary>
        public string Desc { get; set; }
    }
}