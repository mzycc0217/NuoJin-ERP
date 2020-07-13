using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.Projects
{
    /// <summary>
    /// 项目输出类
    /// </summary>
    public class ProjectOutputDto:OutputBase
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 项目状态Id
        /// </summary>
        public string ProjectStateId { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectState ProjectState { get; set; }
    }
}