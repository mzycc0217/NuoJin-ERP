using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    //人员角色对应表

    [Table("User_User_Type")]
    public class User_User_Type : EntityBase
    {
        /// <summary>
        ///人员ID
        /// </summary>
     // [ForeignKey(nameof(User))]
        public long u_Id { get; set; } 
      
        /// <summary>
        /// 人员身份id
        /// </summary>
      // [ForeignKey(nameof(UserType))]
        public long User_Type_ID{get;set;}   
       

        /// <summary>
        /// 人员对应身份id
        /// </summary>
        public int Type_id { get; set; }
      


      
    
    }
}