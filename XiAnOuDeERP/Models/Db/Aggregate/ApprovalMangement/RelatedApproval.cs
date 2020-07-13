using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement
{
    /// <summary>
    /// 模块关联审批
    /// </summary>
    [Table("RelatedApproval")]
    public class RelatedApproval : EntityBase
    {
        /// <summary>
        /// 模块Key
        /// </summary>
        [Required]
        public string RelatedKey { get; set; }

        /// <summary>
        /// 审批流Key
        /// </summary>
        [Required]
        public string ApprovalKey { get; set; }
    }
}