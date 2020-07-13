using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 授权输入类
    /// </summary>
    public class ToGrantAuthorizationInputDto
    {
        /// <summary>
        /// 用户类别
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public List<long> ModuleIds { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public List<long> MenuIds { get; set; }

        /// <summary>
        /// 元素Id
        /// </summary>
        public List<long> ElementIds { get; set; }
    }
}