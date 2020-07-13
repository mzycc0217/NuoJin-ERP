using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 获取用户权限输入类
    /// </summary>
    public class GetUserJurisdictionListInputDto
    {
        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long UserTypeId { get; set; }
    }
}