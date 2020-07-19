using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.EntrepotDto.EntreportOutDto
{
    public class EntrePotOut:InputBase
    {
        /// <summary>
        /// 仓库Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 仓库负责人
        /// </summary>
        public string User_id { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>

        public string EntrepotName { get; set; }

        /// <summary>
        /// 仓库描述
        /// </summary>

        public string EntrepotDes { get; set; }

        /// <summary>
        /// 仓库地点
        /// </summary>

        public string EntrepotAddress { get; set; }


        public  UserDetails userDetails { get; set; }
    }
}