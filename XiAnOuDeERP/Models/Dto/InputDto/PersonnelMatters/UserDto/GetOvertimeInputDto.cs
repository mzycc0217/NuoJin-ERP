using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 获取加班信息输入类
    /// </summary>
    public class GetOvertimeInputDto:InputBase
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 部门领导名称
        /// </summary>
        public string DepartmentLeaderName { get; set; }

        /// <summary>
        /// 审核类型
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }
    }
}