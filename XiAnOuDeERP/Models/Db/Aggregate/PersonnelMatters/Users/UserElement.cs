using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 用户元素实体类
    /// </summary>
    [Table("UserElement")]
    public class UserElement:EntityBase
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 元素Id
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// 元素
        /// </summary>
        public virtual Element Element { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }
    }
}