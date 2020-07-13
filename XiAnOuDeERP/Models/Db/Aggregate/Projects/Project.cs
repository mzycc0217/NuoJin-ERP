using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.Projects
{
    /// <summary>
    /// 项目实体类
    /// </summary>
    [Table("Project")]
    public class Project:EntityBase
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 项目状态Id
        /// </summary>
        public long ProjectStateId { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        [StringLength(255)]
        public string Desc { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public virtual ProjectState ProjectState { get; set; }
    }
}