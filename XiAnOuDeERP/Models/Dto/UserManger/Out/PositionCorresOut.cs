using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.UserManage;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.UserManger.Out
{
    public class PositionCorresOut:InputBase
    {
        /// <summary>
        /// 个人
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string PositionId { get; set; }

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

        /// <summary>
        /// 人名
        /// </summary>
        public string relaname { get; set; }
    }
}