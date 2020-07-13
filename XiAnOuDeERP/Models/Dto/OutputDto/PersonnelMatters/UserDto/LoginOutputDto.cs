using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 登录输出
    /// </summary>
    public class LoginOutputDto
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public UserOutputDto User { get; set; }
    }
}