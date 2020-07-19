using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.OutEntropt.Outenport
{

    [Table("FnishedProduct_UserDetils")]
    public class FnishedProduct_UserDetils : EntityBase
    {
        /// <summary>
        /// 化学物品id
        ///</summary>
        public long FnishedProductId { get; set; }

        /// <summary>
        /// 化学物品人id
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public double FnishedProductNumber { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool del_or { get; set; }
        /// <summary>
        ///领料时间
        /// </summary>
        public DateTime? GetTime { get; set; }


        public virtual UserDetails userDetails { get; set; }
        public virtual Z_FnishedProduct Z_FnishedProduct { get; set; }
    }
}