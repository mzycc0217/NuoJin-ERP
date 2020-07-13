using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 单位输出类
    /// </summary>
    public class CompanyOutputDto:OutputBase
    {
        /// <summary>
        /// 单位Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
    }
}