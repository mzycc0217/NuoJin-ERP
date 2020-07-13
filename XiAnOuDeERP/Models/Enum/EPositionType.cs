using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Enum
{
    /// <summary>
    /// 职位状态
    /// </summary>
    [Description("职位状态")]
    public enum EPositionType
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        Incumbency = 0,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Quit = 1,

        /// <summary>
        /// 培训
        /// </summary>
        [Description("培训")]
        Train = 2,

        /// <summary>
        /// 待培训
        /// </summary>
        [Description("待培训")]
        ToBeTrain =3
    }
}