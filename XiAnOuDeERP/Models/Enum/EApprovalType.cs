using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 审核状态
    /// </summary>
    [Description("审核状态")]
    public enum EApprovalType
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        UnderReview = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Reviewed = 1,

        /// <summary>
        /// 已撤销
        /// </summary>
        [Description("已撤销")]
        Rescinded = 2,

        /// <summary>
        /// 通过后撤销
        /// </summary>
        [Description("通过后撤销")]
        CancellationAfterAdoption = 3,

        /// <summary>
        /// 已驳回
        /// </summary>
        [Description("已驳回")]
        Rejected = 4,

        /// <summary>
        /// 已付款
        /// </summary>
        [Description("已付款")]
        Paid = 5,

        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        InExecution = 6,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 7,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        No = 99

    }
}