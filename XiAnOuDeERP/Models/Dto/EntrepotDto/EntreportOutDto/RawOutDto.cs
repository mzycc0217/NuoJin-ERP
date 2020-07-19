using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.EntrepotDto.EntreportOutDto
{
    public class RawOutDto:InputBase
    {

        public string Id { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public string RawId { get; set; }

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
        public double RawOutNumber { get; set; }

        /// <summary>
        /// 预警
        /// </summary>
        public double Warning_RawNumber { get; set; }

        /// <summary>
        /// 物品描述
        /// </summary>
        public string RoomDes { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string EntrepotId { get; set; }


        public virtual UserDetails userDetails { get; set; }


        public virtual Entrepot entrepot { get; set; }

        public virtual Z_Raw Z_Raw { get; set; }

        /// <summary>
        /// 原材料名称（模糊查询）
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 录入人的名称（模糊查询）
        /// </summary>

        public string relName { get; set; }


    }
}