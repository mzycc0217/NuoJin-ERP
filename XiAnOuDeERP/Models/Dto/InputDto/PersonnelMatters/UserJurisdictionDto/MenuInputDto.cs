using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 菜单输入类
    /// </summary>
    public class MenuInputDto
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单头
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public long ModuleId { get; set; }
    }
}