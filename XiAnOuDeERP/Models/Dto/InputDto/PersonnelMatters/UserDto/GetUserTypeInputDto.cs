using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 获取用户类型输入类
    /// </summary>
    public class GetUserTypeInputDto:InputBase
    {
        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long? UserTypeId { get; set; }

        /// <summary>
        /// 用户类型名称
        /// </summary>
        public string UserTypeName { get; set; }

        /// <summary>
        /// 用户类型Key
        /// </summary>
        public string UserTypeKey { get; set; }
    }
}