using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;

namespace XiAnOuDeERP.Models.Dto.My_FlowDto
{
    public class FinshProducRooms
    {
        /// <summary>
        /// 物料id
        /// </summary>
        public long FnishedProductId { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// 物料数量
        /// </summary>
        public double RawNumber { get; set; }


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
        /// 删除
        /// </summary>
        public bool del_or { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string EntrepotId { get; set; }


        public UserDetails userDetails { get; set; }


        public Entrepot entrepot { get; set; }

        public Z_FnishedProduct z_FnishedProduct { get; set; }
    }
}