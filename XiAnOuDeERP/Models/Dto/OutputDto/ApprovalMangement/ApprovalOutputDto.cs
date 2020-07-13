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
    public class ApprovalOutputDto:OutputBase
    {
        /// <summary>
        /// 审批流Id
        /// </summary>
        public string ApprovalId { get; set; }

        /// <summary>
        /// 审批流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 用户类型Key
        /// </summary>
        public string UserTypeKey { get; set; }

        /// <summary>
        /// 用户类型名称
        /// </summary>
        public string UserTypeName { get; set; }

        /// <summary>
        /// 模块Key
        /// </summary>
        public string RelatedKey { get; set; }

        /// <summary>
        /// 审批顺序
        /// </summary>
        public int Deis { get; set; }
    }
}