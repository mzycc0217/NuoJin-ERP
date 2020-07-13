using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 入库类型输入类
    /// </summary>
    public class WarehousingTypeInputDto
    {
        /// <summary>
        /// 入库类型Id
        /// </summary>
        public long WarehousingId { get; set; }

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