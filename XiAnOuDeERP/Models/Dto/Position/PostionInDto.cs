using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Position
{
    public class PostionInDto
    {
        /// <summary>
        /// 职位Id
        /// </summary>
        public long Id { get; set; }


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
        /// 删除
        /// </summary>
        public List<long> Del_Id { get; set; }
    }
}