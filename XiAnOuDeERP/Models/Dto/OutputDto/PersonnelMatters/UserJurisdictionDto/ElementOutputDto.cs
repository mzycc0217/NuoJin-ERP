using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 元素输出类
    /// </summary>
    public class ElementOutputDto
    {
        /// <summary>
        /// 元素Id
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// 元素名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元素键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 元素值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }
    }
}