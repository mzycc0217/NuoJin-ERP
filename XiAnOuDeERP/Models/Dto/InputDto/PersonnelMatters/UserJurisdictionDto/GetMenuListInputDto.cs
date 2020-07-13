using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 获取输入类
    /// </summary>
    public class GetMenuListInputDto
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 元素Id
        /// </summary>
        public long ElementId { get; set; }
    }
}