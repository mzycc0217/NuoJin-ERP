using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Util
{
    public enum EnupProduct
    {  /// <summary>
       /// 成品
       /// </summary>
        [Description("成品")]
        ProductFinshed=1,
        /// <summary>
        /// 半成品
        /// </summary>
        [Description("半成品")]
        HalfProductFinshed=2
    }
}