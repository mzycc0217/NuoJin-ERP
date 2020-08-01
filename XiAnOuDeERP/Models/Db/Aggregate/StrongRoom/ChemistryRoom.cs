using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.StrongRoom
{
    /// <summary>
    /// 化学用品仓库对应表
    /// </summary>
    [Table("ChemistryRoom")]
    public class ChemistryRoom : EntityBase
    {
        
        /// <summary>
        /// 化学用品id
        /// </summary>
        public long ChemistryId { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public long? User_id { get; set; }

        /// <summary>
        /// 化学用品数量
        /// </summary>
        public double? RawNumber { get; set; }


        /// <summary>
        /// 出库数量
        /// </summary>
        public double? RawOutNumber { get; set; }

        /// <summary>
        /// 预警
        /// </summary>
        public double? Warning_RawNumber { get; set; }

        /// <summary>
        /// 物品描述
        /// </summary>
        public string RoomDes { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public long? EntrepotId { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool del_or { get; set; }

        public virtual UserDetails userDetails { get; set; }


        public virtual Entrepot entrepot { get; set; }

        public virtual Z_Chemistry Z_Chemistry { get; set; }


    }
}