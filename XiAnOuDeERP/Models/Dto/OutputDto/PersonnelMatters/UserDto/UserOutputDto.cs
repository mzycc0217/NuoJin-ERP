using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户输出类
    /// </summary>
    public class UserOutputDto:OutputBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public List<string> UserTypeId { get; set; }

        /// <summary>
        /// 用户类型Str
        /// </summary>
        public List<string> UserTypeStr { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public EPositionType PositionType { get; set; }

        /// <summary>
        /// 在职状体Str
        /// </summary>
        public string PositionTypeStr { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }

        #region 用户详情

        /// <summary>
        /// 姓名
        /// </summary>
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
        /// 性别Str
        /// </summary>
        public string SexTypeStr { get; set; }

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
        /// 是否注销
        /// </summary>
        public bool IsCancellation { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string PortraitPath { get; set; }
        #endregion

    }
}