using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Table("User")]
    public class User :EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Password { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [Required]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public EPositionType PositionType { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual Department Department { get; set; }



        
    }
}