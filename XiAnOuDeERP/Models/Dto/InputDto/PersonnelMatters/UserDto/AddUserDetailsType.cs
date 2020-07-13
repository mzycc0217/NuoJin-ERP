using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户绑定用户类型输入类
    /// </summary>
    public class AddUserDetailsType
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long UserTypeId { get; set; }
    }
}