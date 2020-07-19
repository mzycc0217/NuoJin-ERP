using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.EntrepotDto.EntrepotInDto
{
    public class OfficeIntDto
    {


        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 办公用品id
        /// </summary>
        public long OfficeId { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 办公用品数量
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
        public long EntrepotId { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public List<long> del_Id { get; set; }

    }
}