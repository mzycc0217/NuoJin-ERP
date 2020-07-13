using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户绑定身份类型输出类
    /// </summary>
    public class UserDetailsTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 用户绑定身份Id
        /// </summary>
        public string UserDeatilsTypeId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户类型Id
        /// </summary>
        public string UserTypeId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserTypeOutputDto UserType { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public UserOutputDto User { get; set; }
    }
}