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
    /// 产成品签核表
    /// </summary>
    [Table("FinshedProduct_User")]
    public class FinshedProduct_User : EntityBase
    {
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long FnishedProductId { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(255)]
        public string ContentDes { get; set; }


        public virtual FnishedProductMonad FnishedProductMonad { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}