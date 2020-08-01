using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.UserManage
{

    /// <summary>
    ///职位表
    /// </summary>
    [Table("Position_User")]
    public class Position_User : EntityBase
    {


        /// <summary>
        /// 部门
        /// </summary>
      //  public long? DepatmentId { get; set; }

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
        public bool del_Or { get; set; }
            

      //  public virtual Department Department { get; set; }


      

    }
}