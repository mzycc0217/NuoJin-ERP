using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 人员需求审核记录表
    /// </summary>
    [Table("PersonnelRtsApproval")]
    public class PersonnelRtsApproval:EntityBase
    {
        /// <summary>
        /// 人员需求Id
        /// </summary>
        public long PersonnelRtsId { get; set; }

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
        /// 人员需求
        /// </summary>
        public virtual PersonnelRts PersonnelRts { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserDetails User { get; set; }
    }
}