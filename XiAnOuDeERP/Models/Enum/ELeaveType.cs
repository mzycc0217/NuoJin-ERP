using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 请假类型
    /// </summary>
    [Description("请假类型")]
    public enum ELeaveType
    {
        /// <summary>
        /// 事假
        /// </summary>
        [Description("事假")]
        Compassionate = 0,

        /// <summary>
        /// 病假
        /// </summary>
        [Description("病假")]
        Sick = 1,

        /// <summary>
        /// 产假
        /// </summary>
        [Description("产假")]
        Maternity = 2,

        /// <summary>
        /// 调休
        /// </summary>
        [Description("调休")]
        Compensatory = 3,

    }
}