using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户类别输入类
    /// </summary>
    public class UserTypeInputDto
    {
        /// <summary>
        /// 用户类别Id
        /// </summary>
        public long UserTypeId { get; set; }

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