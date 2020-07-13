using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 用户身份表
    /// </summary>
    [Table("UserDetailsType")]
    public class UserDetailsType:EntityBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }
    }
}