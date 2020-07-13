using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户类型输出类
    /// </summary>
    public class UserTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 用户类型Id
        /// </summary>
        public string UserTypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }
    }
}