using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 获取单位列表输入类
    /// </summary>
    public class GetCompanyListInputDto:InputBase
    {
        /// <summary>
        /// 单位Id
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
    }
}