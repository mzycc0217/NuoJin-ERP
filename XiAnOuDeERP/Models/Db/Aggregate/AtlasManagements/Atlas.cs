using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.AtlasManagements
{
    /// <summary>
    /// 图谱实体库
    /// </summary>
    [Table("Atlas")]
    public class Atlas:EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [Required]
        [StringLength(255)]
        public string BatchNumber{ get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        [StringLength(255)]
        public string Desc { get; set; }

        /// <summary>
        /// 图谱文件路径
        /// </summary>
        [StringLength(255)]
        public string Url { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime TestingTime { get; set; }
    }
}