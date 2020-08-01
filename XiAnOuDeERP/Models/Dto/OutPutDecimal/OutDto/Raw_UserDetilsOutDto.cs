using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;

namespace XiAnOuDeERP.Models.Dto.OutPutDecimal.OutDto
{
    public class Raw_UserDetilsOutDto
    {

        /// <summary>
        /// 物料id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 物料id
        /// </summary>
        public string RawId { get; set; }

        /// <summary>
        /// 物料人id
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public double RawNumber { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 是否退库
        /// </summary>
        public double OutIutRoom { get; set; }

        /// <summary>
        /// 是否删除(不用管这个字段)
        /// </summary>
        public bool del_or { get; set; }
        /// <summary>
        ///领料时间
        /// </summary>
        public DateTime? GetRawTime { get; set; }

        /// <summary>
        /// 仓库id
        /// </summary>
        public long? entrepotid { get; set; }
        public  UserDetails userDetails { get; set; }
        public  Entrepot entrepot { get; set; }



        public  Z_Raw z_Raw { get; set; }


    }
}