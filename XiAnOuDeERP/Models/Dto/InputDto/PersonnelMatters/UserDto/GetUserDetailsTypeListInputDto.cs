using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 获取用户绑定身份列表输入类
    /// </summary>
    public class GetUserDetailsTypeListInputDto:InputBase
    {
        /// <summary>
        /// 用户绑定身份Id
        /// </summary>
        public long? UserDetailsTypeId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 身份Id
        /// </summary>
        public long? UserTypeId { get; set; }

        /// <summary>
        /// 用户名字
        /// </summary>
        public string UserRealName { get; set; }

        /// <summary>
        /// 身份名称
        /// </summary>
        public string UserTypeName { get; set; }
    }
}