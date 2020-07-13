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
    /// 菜单实体类
    /// </summary>
    [Table("Menu")]
    public class Menu:EntityBase
    {
        /// <summary>
        /// 菜单头
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Title { get; set; }

        /// <summary>
        /// 菜单键
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Key { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [Required]
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual Module Module { get; set; }

    }
}