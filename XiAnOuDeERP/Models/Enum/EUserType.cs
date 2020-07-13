using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [Description("用户类型")]
    public enum EUserType
    {
        /// <summary>
        /// 总经理
        /// </summary>
        [Description("总经理")]
        GeneralManager = 0,

        /// <summary>
        /// 副总经理
        /// </summary>
        [Description("副总经理")]
        ViceGeneralManager = 1,

        /// <summary>
        /// 总经理助理
        /// </summary>
        [Description("总经理助理")]
        GeneralManagerAssistant = 2,

        /// <summary>
        /// 财务总监
        /// </summary>
        [Description("财务总监")]
        CFO = 3,

        /// <summary>
        /// 科研总监
        /// </summary>
        [Description("科研总监")]
        DirectorOfScientificResearch = 4,

        /// <summary>
        /// 生产总监
        /// </summary>
        [Description("生产总监")]
        ProductionDirector = 5,

        /// <summary>
        /// 采购总监
        /// </summary>
        [Description("采购总监")]
        PurchasingDirector = 6,

        /// <summary>
        /// 组长
        /// </summary>
        [Description("组长")]
        TeamLeader = 7,

        /// <summary>
        /// 部门经理
        /// </summary>
        [Description("部门经理")]
        DivisionManager = 8,

        /// <summary>
        /// 员工
        /// </summary>
        [Description("员工")]
        Staff =9,

        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        Admin = 99
    }
}