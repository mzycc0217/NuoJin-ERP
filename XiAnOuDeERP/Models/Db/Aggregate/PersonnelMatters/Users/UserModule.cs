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
    /// 用户模块实体类
    /// </summary>
    [Table("UserModule")]
    public class UserModule:EntityBase
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }
    }
}