using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 请假表
    /// </summary>
    [Table("Leave")]
    public class Leave:EntityBase
    {
        /// <summary>
        /// 请假人Id
        /// </summary>
        public long UserId { get; set; }

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
        /// 请假类型
        /// </summary>
        public ELeaveType LeaveType { get; set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 审核流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 当前审核
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 请假人
        /// </summary>
        public virtual UserDetails User { get; set; }
    }
}