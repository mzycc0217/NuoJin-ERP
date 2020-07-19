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
    //钱和表化学用品
    [Table("Chmistry_User")]
    public class Chmistry_User : EntityBase
    {


        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long ChemistryId { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(255)]
        public string ContentDes { get; set; }


        public virtual ChemistryMonad ChemistryMonad { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
    }