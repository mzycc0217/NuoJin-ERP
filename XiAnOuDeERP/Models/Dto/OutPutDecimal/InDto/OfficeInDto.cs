using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutPutDecimal.InDto
{
    public class OfficeInDto : InputBase
    {
        /// <summary>
        /// 明细Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 办公用品id
        /// </summary>
        public long OfficeId { get; set; }

        /// <summary>
        /// 物料人id
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public double ChemistryNumber { get; set; }


        /// <summary>
        /// 是否退库
        /// </summary>
        public double OutIutRoom { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>
        public long? entrepotid { get; set; }


        /// <summary>
        /// 是否删除(不用管这个字段)
        /// </summary>
        public bool del_or { get; set; }
        /// <summary>
        ///领料时间
        /// </summary>
        public DateTime? GetRawTime
        {
            get; set;
        }

        /// <summary>
        /// 名字（模糊查询）
        /// </summary>
        public string RelName { get; set; }

        /// <summary>
        /// 物品名称（模糊查询）
        /// </summary>
        public string Name { get; set; }
    }
}