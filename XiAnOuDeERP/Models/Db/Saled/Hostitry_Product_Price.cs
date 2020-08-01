using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Saled
{

    /// <summary>
    ///历史产品价格记录表
    /// </summary>
     [Table("Hostitry_Product_Price")]
    public class Hostitry_Product_Price : EntityBase
    {

        /// <summary>
        /// 产品id
        /// </summary>
        public long FinshProductId { get; set; }


        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string FinshProductDes { get; set; }

        /// <summary>
        /// 销售专员
        /// </summary>
        public long User_Id { get; set; }


        /// <summary>
        /// 客户id
        /// </summary>
        public long CustomNameId{ get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Price_Time { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool del_Or{ get; set; }


        public virtual Z_FnishedProduct z_FnishedProduct { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        
        public virtual Product_Custorm Product_Custorm { get; set; }




       



    }
}