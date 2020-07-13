using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;

namespace XiAnOuDeERP.Models.Dto
{
    public class User_DTO
    { /// <summary>
      /// 用户Id
      /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>

        public string RealName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }



        /// <summary>
        /// 身份证
        /// </summary>

        public string IdCard { get; set; }

        /// <summary>
        /// 民族
        /// </summary>

        public string Nation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>

        public string Education { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>

        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>

        public string WeiXin { get; set; }

        /// <summary>
        /// 住址
        /// </summary>

        public string Address { get; set; }

        /// <summary>
        /// 头像
        /// </summary>

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