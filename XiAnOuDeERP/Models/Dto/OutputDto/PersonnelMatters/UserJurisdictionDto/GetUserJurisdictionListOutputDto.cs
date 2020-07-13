using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 获取用户权限输出类
    /// </summary>
    public class GetUserJurisdictionListOutputDto
    {
        /// <summary>
        /// 用户类别
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public List<ModuleOutputDto> ModuleList { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuOutputDto> MenuList { get; set; }

        /// <summary>
        /// 元素
        /// </summary>
        public List<ElementOutputDto> ElementList { get; set; }

        ///// <summary>
        ///// 用户类型
        ///// </summary>
        //public UserType UserType { get; set; }
    }
}