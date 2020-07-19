using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements
{
    /// <summary>
    /// 领料表
    /// </summary>
    [Table("Raw_UserDetils")]
    public class Raw_UserDetils : EntityBase
    {

        /// <summary>
        /// 物料id
        /// </summary>
        public long RawId { get; set; }

        /// <summary>
        /// 物料人id
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public double RawNumber { get; set; }


        /// <summary>
        /// 是否退库
        /// </summary>
        public double OutIutRoom { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool del_or { get; set; }


        /// <summary>
        /// 显示状态
        /// </summary>
        public int is_or { get; set; }
        /// <summary>
        ///领料时间
        /// </summary>
        public DateTime? GetRawTime { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public long? entrepotid { get; set; }

        public virtual UserDetails userDetails { get; set; }

        public virtual Entrepot entrepot { get; set; }

        public virtual Z_Raw z_Raw { get; set; }
    }
}