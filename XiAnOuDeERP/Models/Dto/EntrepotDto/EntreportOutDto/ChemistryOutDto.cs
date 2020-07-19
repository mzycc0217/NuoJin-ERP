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
    public class ChemistryOutDto:InputBase
    {


        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 化学用品id
        /// </summary>
        public string ChemistryId { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// 化学用品数量
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

        /// <summary>
        /// 模糊查询（根据化学用品名称）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  模糊查询（根据录入人姓名）
        /// </summary>
        public string relName { get; set; }

        public  UserDetails userDetails { get; set; }


        public  Entrepot entrepot { get; set; }

        public  Z_Chemistry Z_Chemistry { get; set; }



    }
}