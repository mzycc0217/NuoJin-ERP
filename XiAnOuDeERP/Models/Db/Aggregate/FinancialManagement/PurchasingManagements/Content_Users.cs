using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements
{
    //内容和人的关联表
    [Table("Content_Usersd")]
    public class Content_Usersd : EntityBase
    {


        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long Purchase_Id { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(255)]
        public string ContentDes { get; set; }


        public virtual Purchase Purchase { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}