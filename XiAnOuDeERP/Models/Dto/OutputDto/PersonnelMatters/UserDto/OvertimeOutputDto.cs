using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 加班输出类
    /// </summary>
    public class OvertimeOutputDto:OutputBase
    {
        /// <summary>
        /// 加班Id
        /// </summary>
        public string OvertimeId { get; set; }

        /// <summary>
        /// 加班时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 审核状态Str
        /// </summary>
        public string ApprovalTypeStr { get; set; }

        /// <summary>
        /// 部门领导Id
        /// </summary>
        public string DepartmentLeaderId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public  UserDetails User { get; set; }

        /// <summary>
        /// 部门领导
        /// </summary>
        public  UserDetails DepartmentLeader { get; set; }
    }
}