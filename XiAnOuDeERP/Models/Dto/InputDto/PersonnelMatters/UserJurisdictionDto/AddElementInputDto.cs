using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 添加元素输入类
    /// </summary>
    public class AddElementInputDto
    {
        /// <summary>
        /// 元素名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 元素值
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required]
        public long MenuId { get; set; }
    }
}