using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 更新用户密码输入类
    /// </summary>
    public class UpdateUserPassWordInputDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassWord { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string PassWord { get; set; }
    }
}