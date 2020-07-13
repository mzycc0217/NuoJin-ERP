using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement
{
    /// <summary>
    /// 模块绑定审批流输入类
    /// </summary>
    public class RelatedApprovalInputDto
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