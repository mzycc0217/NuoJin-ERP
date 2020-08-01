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
    /// 价格表(可以不用)
    /// </summary>
    [Table("ProductSalePrice")]
    public class ProductSalePrice : EntityBase
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
        /// 销售的人
        /// </summary>
        public long? User_Id { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public double del_Or { get; set; }

        public virtual Z_FnishedProduct z_FnishedProduct {get;set;}


        public virtual UserDetails UserDetails { get; set; }

    }
}