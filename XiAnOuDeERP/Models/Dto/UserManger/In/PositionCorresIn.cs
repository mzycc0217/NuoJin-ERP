using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.UserManger.In
{
    public class PositionCorresIn
    {

        /// <summary>
        /// id
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 个人
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 职位标志
        /// </summary>
        public int? Sign { get; set; }

    }
}