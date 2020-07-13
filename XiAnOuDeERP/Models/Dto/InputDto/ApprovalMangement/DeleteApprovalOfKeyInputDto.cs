using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement
{
    /// <summary>
    /// 根据Key删除审批流输入类
    /// </summary>
    public class DeleteApprovalOfKeyInputDto
    {
        /// <summary>
        /// 审批流Key
        /// </summary>
        public string Key { get; set; }
    }
}