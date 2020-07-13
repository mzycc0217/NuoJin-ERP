using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.Projects
{
    /// <summary>
    /// 项目状态输出类
    /// </summary>
    public class ProjectStateOutputDto:OutputBase
    {
        /// <summary>
        /// 项目状态Id
        /// </summary>
        public string ProjectStateId { get; set; }

        /// <summary>
        /// 项目状态名称
        /// </summary>
        public string ProjectStateName { get; set; }
    }
}