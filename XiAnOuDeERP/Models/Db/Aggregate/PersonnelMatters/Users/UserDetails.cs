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
    /// 用户详细信息
    /// </summary>
    [Table("UserDetails")]
    public class UserDetails:EntityBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(255)]
        public string RealName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESexType SexType { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [StringLength(255)]
        public string IdCard { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [StringLength(255)]
        public string Nation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [StringLength(255)]
        public string Education { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(255)]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        [StringLength(255)]
        public string WeiXin { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        [StringLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(255)]
        public string PortraitPath { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否注销
        /// </summary>
        public bool IsCancellation { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
    }
}