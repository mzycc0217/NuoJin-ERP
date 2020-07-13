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
    /// 用户菜单实体类
    /// </summary>
    [Table("UserMenu")]
    public class UserMenu:EntityBase
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }
    }
}