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
    /// 元素实体类
    /// </summary>
    [Table("Element")]
    public class Element:EntityBase
    {
        /// <summary>
        /// 元素名称
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        /// <summary>
        /// 元素键
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Key { get; set; }

        /// <summary>
        /// 元素值
        /// </summary>
        [StringLength(80)]
        public string Value { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required]
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Menu Menu { get; set; }
    }
}