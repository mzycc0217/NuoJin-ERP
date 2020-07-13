using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseApi
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public EExceptionType Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}