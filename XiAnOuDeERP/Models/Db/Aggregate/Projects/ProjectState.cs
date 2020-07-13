using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.Projects
{
    /// <summary>
    /// 项目状态表
    /// </summary>
    [Table("ProjectState")]
    public class ProjectState:EntityBase
    {
        /// <summary>
        /// 项目状态名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}