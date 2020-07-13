using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.ApprovalMangement
{
    /// <summary>
    /// 获取审批流输入类
    /// </summary>
    public class GetApprovalInputDto:InputBase
    {
        /// <summary>
        /// 审批流Id
        /// </summary>
        public long? ApprovalId { get; set; }

        /// <summary>
        /// 审批流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 用户类型Key
        /// </summary>
        public string UserTypeKey { get; set; }

        /// <summary>
        /// 模块Key
        /// </summary>
        public string RelatedKey { get; set; }
    }
}