using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto
{
    public class user_userTypeDTO
    {
        public string Id { get; set; }
        /// <summary>
        ///人员ID
        /// </summary>
        // [ForeignKey(nameof(User))]
        public string u_Id { get; set; }

        /// <summary>
        /// 人员身份id
        /// </summary>
        // [ForeignKey(nameof(UserType))]
        public string User_Type_ID { get; set; }


        /// <summary>
        /// 人员对应身份id
        /// </summary>
        public int Type_id { get; set; }
    }
}