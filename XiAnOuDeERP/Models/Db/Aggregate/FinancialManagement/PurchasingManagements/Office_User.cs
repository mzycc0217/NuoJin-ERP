using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements
{
    /// <summary>
    /// 办公用品记录表
    /// </summary>
    [Table("Office_User")]
    public class Office_User:EntityBase
    {
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long OfficeId { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(255)]
        public string ContentDes { get; set; }


        public virtual OfficeMonad OfficeMonad { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}