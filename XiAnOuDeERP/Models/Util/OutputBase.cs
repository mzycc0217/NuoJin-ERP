using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputBase
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}