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
    /// 请假输出类
    /// </summary>
    public class LeaveOutputDto:OutputBase
    {
        /// <summary>
        /// 请假Id
        /// </summary>
        public string LeaveId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public ELeaveType LeaveType { get; set; }

        /// <summary>
        /// 请假类型Str
        /// </summary>
        public string LeaveTypeStr { get; set; }

        /// <summary>
        /// 请假备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 请假人Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public string ApprovelId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 请假人
        /// </summary>
        public UserDetails User { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public UserDetails Approvel { get; set; }
    }
}