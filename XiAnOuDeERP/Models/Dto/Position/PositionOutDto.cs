using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Position
{
    public class PositionOutDto:InputBase
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string PositionDes { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Counts { get; set; }
    }
}