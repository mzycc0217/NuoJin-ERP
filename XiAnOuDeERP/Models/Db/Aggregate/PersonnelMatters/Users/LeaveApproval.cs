using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 请假审核记录表
    /// </summary>
    [Table("LeaveApproval")]
    public class LeaveApproval:EntityBase
    {
        /// <summary>
        /// 请假Id
        /// </summary>
        public long LeaveId { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsApproval { get; set; }

        /// <summary>
        /// 审核人用户类型key
        /// </summary>
        public string UserTypeKey { get; set; }

        /// <summary>
        /// 审批顺序
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 请假申请单
        /// </summary>
        public virtual Leave Leave { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserDetails User { get; set; }
    }
}