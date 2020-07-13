using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 模块实体类
    /// </summary>
    [Table("Module")]
    public class Module:EntityBase
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        /// <summary>
        /// 模块键
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Key { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }
    }
}