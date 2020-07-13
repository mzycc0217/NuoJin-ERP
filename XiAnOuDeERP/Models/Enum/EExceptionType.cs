using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 异常类型
    /// </summary>
    [Description("异常类型")]
    public enum EExceptionType
    {
        /// <summary>
        /// 用户未登录
        /// </summary>
        [Description("用户未登录")]
        NoLogin = 0,

        /// <summary>
        /// 执行错误
        /// </summary>
        [Description("执行错误")]
        Implement = 1


    }
}