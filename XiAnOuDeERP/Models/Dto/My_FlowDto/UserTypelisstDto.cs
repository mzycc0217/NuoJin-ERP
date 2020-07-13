using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;

namespace XiAnOuDeERP.Models.Dto.My_FlowDto
{
    public class UserTypelisstDto
    {
        /// <summary>
        /// 个人id
        /// </summary>
        public long id { get; set; }
        
        /// <summary>
        /// 人员对应id
        /// </summary>
        public List<long> user_Typeid { get; set; }

        /// <summary>
        /// 人员身份对应表id---对应删除
        /// </summary>
        public List<long> Del_ID { get; set; }
    }
}