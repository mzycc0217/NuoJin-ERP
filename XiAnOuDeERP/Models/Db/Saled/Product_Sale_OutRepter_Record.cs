using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Saled
{

    /// <summary>
    /// 销售产品前额和记录表
    /// </summary>
    [Table("Product_Sale_OutRepter_Record")]
    public class Product_Sale_OutRepter_Record : EntityBase
    {


      
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long Sale_Id { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(255)]
        public string ContentDes { get; set; }

        public virtual Product_Sale Product_Sale { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}