using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.My_FlowDto
{
    public class Depertment_User
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public long id { get; set; }


        /// <summary>
        /// 个人id
        /// </summary>
        public List<long> user_id{get;set;}

    }
}