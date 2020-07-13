using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 性别
    /// </summary>
    [Description("性别")]
    public enum ESexType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 0,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 1
    }
}