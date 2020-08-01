using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.UserManage
{

    /// <summary>
    /// 领导和部门的对应表
    /// </summary>
    [Table("Position_Correspond")]
    public class Position_Correspond : EntityBase
    {
        /// <summary>
        /// 个人
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 职位标志
        /// </summary>
        public int? Sign { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public bool del_Or { get; set; }

        public virtual Position_User Position_User { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}