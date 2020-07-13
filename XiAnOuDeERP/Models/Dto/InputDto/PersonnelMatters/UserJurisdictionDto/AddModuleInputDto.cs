using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 添加模块输入类
    /// </summary>
    public class AddModuleInputDto
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 模块键
        /// </summary>
        [Required]
        public string Key { get; set; }
    }
}