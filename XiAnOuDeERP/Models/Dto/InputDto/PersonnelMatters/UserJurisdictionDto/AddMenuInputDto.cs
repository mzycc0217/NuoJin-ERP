using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 添加菜单输入类
    /// </summary>
    public class AddMenuInputDto
    {
        /// <summary>
        /// 菜单头
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [Required]
        public long ModuleId { get; set; }
    }
}