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
    /// 加班表
    /// </summary>
    [Table("Overtime")]
    public class Overtime:EntityBase
    {
        /// <summary>
        /// 加班时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 加班日期
        /// </summary>
        public DateTime OverTimeDate { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 部门领导Id
        /// </summary>
        public long? DepartmentLeaderId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 部门领导
        /// </summary>
        public virtual UserDetails DepartmentLeader { get; set; }

    }
}