using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class InputBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

    //    /// <summary>
    //    /// 名字（模糊查询）
    //    /// </summary>
    //    public string RelName { get; set; }

    //    /// <summary>
    //    /// 物品名称（模糊查询）
    //    /// </summary>
    //    public string Name { get; set; }
    }
}