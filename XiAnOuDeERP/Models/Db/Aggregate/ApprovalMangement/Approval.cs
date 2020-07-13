using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement
{
    /// <summary>
    /// 审批
    /// </summary>
    [Table("Approval")]
    public class Approval:EntityBase
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 用户类型Key
        /// </summary>
        public string UserTypeKey { get; set; }

        /// <summary>
        /// 审批顺序
        /// </summary>
        public int Deis { get; set; }
    }
}