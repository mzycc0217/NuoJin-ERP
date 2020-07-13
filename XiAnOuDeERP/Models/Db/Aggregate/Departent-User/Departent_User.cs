using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.Departent_User
{
    [Table("Departent_User")]
    public class Departent_User : EntityBase
    {
        /// <summary>
        /// 部门id
        /// </summary>
        
        public long? Department_Id { get; set; }
       // [ForeignKey("Department_Id")]
        public Department Department { get; set; }

        /// <summary>
        /// 人员id对应detil
        /// </summary>
      
        public long? UserDetails_Id { get; set; }
       // [ForeignKey("UserDetails_Id")]
        public UserDetails UserDetails { get; set; }

    }
}