using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 单位输入类
    /// </summary>
    public class CompanyInputDto
    {
        /// <summary>
        /// 单位Id
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
    }
}