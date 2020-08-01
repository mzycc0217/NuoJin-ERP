using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.OutEntropt.Outenport
{
    /// <summary>
    /// 领取化学物品记录表
    /// </summary>
    [Table("Chemistry_UserDetils")]
    public class Chemistry_UserDetils : EntityBase
    {
        /// <summary>
        /// 化学物品id
        /// </summary>
        public long ChemistryId { get; set; }

        /// <summary>
        /// 化学物品人id
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 是否退库
        /// </summary>
        public double OutIutRoom { get; set; }

        /// <summary>
        /// 领取数量（出库数量）
        /// </summary>
        public double ChemistryNumber { get; set; }

        /// <summary>
        /// 入库数量（入库数量）
        /// </summary>
        public double InenportNumber { get; set; }
        /// <summary>
        /// 显示状态
        /// </summary>
        public int is_or { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool del_or { get; set; }
        /// <summary>
        ///领料时间
        /// </summary>
        public DateTime? GetTime { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public long? entrepotid { get; set; }

        public virtual UserDetails userDetails { get; set; }
        public virtual Z_Chemistry Z_Chemistry { get; set; }
        public virtual Entrepot entrepot { get; set; }


    }
}