using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;

namespace XiAnOuDeERP.Models.Dto.OutPutDecimal.OutDto
{
    public class Chiemistry_UserDetilsOutDto
    {

        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 化学id
        /// </summary>
        public string ChemistryId { get; set; }

        /// <summary>
        /// 物料人id
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public double ChemistryNumber { get; set; }


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
        public DateTime? GetTime { get; set; }

        /// <summary>
        /// 仓库id
        /// </summary>
        public long? entrepotid { get; set; }
        public UserDetails userDetails { get; set; }
        public Entrepot entrepot { get; set; }



        public Z_Chemistry z_Chemistry { get; set; }
    }
}