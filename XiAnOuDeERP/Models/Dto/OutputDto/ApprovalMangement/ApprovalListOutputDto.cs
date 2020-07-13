using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.ApprovalMangement
{
    /// <summary>
    /// 审批流输出类
    /// </summary>
    public class ApprovalListOutputDto : OutputBase
    {
        /// <summary>
        /// 审批流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 绑定模块Key
        /// </summary>
        public string RelatedKey { get; set; }

        /// <summary>
        /// 审批流
        /// </summary>
        public List<ApprovalOutputDto> Approval { get; set; }
    }
}