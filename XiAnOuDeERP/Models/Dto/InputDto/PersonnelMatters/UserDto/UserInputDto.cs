using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 用户输入类
    /// </summary>
    public class UserInputDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户类型Id
        /// </summary>
        public long? UserTypeId { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public EPositionType PositionType { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        #region 用户详情

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealeName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESexType SexType { get; set; }

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
        #endregion
    }
}