using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Saled
{
    public class FuzzySurch
    {
        /// <summary>
        /// 人员姓名（模糊查询）
        /// </summary>
        public string RelName { get; set; }

        /// <summary>
        /// 物品名称（模糊查询）
        /// </summary>
        public string Name { get; set; }

    }
}