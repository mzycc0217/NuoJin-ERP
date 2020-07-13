using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement
{
    /// <summary>
    /// 审批流输入类
    /// </summary>
    public class ApprovalInputDto
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 用户类型Key
        /// </summary>
        public string UserTypeKey { get; set; }
    }
}