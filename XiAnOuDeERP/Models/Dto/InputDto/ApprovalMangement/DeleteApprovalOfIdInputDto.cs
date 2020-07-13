using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement
{
    /// <summary>
    /// 根据Id删除审批流输入类
    /// </summary>
    public class DeleteApprovalOfIdInputDto
    {
        /// <summary>
        /// 审批流Id
        /// </summary>
        public long ApprovalId { get; set; }
    }
}