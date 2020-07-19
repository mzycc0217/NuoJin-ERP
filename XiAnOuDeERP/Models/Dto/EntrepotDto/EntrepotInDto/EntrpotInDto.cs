using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.EntrepotDto.EntrepotInDto
{
    public class EntrpotInDto
    {
        /// <summary>
        /// 仓库Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 仓库负责人
        /// </summary>
        public long User_id { get; set; }
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


        /// <summary>
        /// 删除
        /// </summary>

        public List<long> del_Id { get; set; }

    }
}