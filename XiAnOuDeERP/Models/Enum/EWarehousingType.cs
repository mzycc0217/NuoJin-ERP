using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 入库类型
    /// </summary>
    [Description("入库类型")]
    public enum EWarehousingType
    {
        /// <summary>
        /// 原材料
        /// </summary>
        [Description("原材料")]
        RawMaterial = 0,

        /// <summary>
        /// 科研耗材
        /// </summary>
        [Description("科研耗材")]
        ScientificProduction = 1,

        /// <summary>
        /// 办公用品
        /// </summary>
        [Description("办公用品")]
        OfficeSupplies=2,

        /// <summary>
        /// 设备
        /// </summary>
        [Description("设备")]
        Equipment=3,

        /// <summary>
        /// 成品
        /// </summary>
        [Description("成品")]
        FinishedProduct=4
    }
}