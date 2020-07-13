using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserJurisdictionDto
{
    /// <summary>
    /// 获取用户模块列表
    /// </summary>
    public class GetUserModuleListOutputDto
    {
        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public List<ModuleOutputDto> Module { get; set; }
    }
}