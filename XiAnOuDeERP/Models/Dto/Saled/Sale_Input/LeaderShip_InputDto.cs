using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Saled.Sale_Input
{
    public class LeaderShip_InputDto
    {
        /// <summary>
        /// 申请单id
        /// </summary>
        public long Sale_Id { get; set; }

        /// <summary>
        /// 签核人id
        /// </summary>
        public long User_DId { get; set; }

        /// <summary>
        /// 个人id（不需要，当需要获取所有某个人审核的内容时传递）
        /// </summary>
        public string user_Id { get; set; }

        /// <summary>
        /// 签核描述
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 签核状态(1)代表出库申请，2代表销售状态
        /// </summary>
        public int Finsh_Start { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Price_Time { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public double del_Or { get; set; }

        /// <summary>
        /// 仓库id
        /// </summary>
        public long enportid { get; set; }

    }
}