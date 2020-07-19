using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Users
{
    /// <summary>
    /// 登录输入类
    /// </summary>
    public class LoginInputDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        ///// <summary>
        ///// 用户类型Id
        ///// </summary>
        //public long UserTypeId { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Verification{ get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}