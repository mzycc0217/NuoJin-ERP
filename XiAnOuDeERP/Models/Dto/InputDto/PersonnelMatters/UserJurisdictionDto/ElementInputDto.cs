using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 元素输入类
    /// </summary>
    public class ElementInputDto
    {
        /// <summary>
        /// 元素Id
        /// </summary>
        public long ElementId { get; set; }

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
        public long MenuId { get; set; }

    }
}