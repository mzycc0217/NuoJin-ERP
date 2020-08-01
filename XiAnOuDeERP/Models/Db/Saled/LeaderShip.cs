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
    ///签核表
    /// </summary>
    [Table("LeaderShip")]
    public class LeaderShip : EntityBase
    {
        /// <summary>
        /// 销售申请单id
        /// </summary>
        public long Sale_Id { get; set; }

        /// <summary>
        /// 签核人id
        /// </summary>
        public long User_DId { get; set; }

        /// <summary>
        /// 签核描述
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 签核状态(1)代表出库申请，2代表销售状态
        /// </summary>
        public int Finsh_Start { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Price_Time { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public double del_Or { get; set; }

      


        public virtual Product_Sale Product_Sale {get;set;}



        public virtual UserDetails user_Detils { get; set; }
    }
}