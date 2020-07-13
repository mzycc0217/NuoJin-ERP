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
    /// 人员需求输出类
    /// </summary>
    public class PersonnelRtsOutputDto:OutputBase
    {
        /// <summary>
        /// 人员需求Id
        /// </summary>
        public string PersonnelRtsId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public string AddbyId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 审核状态Str
        /// </summary>
        public string ApprovalTypeStr { get; set; }

        /// <summary>
        /// 需招人数
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 已招人数
        /// </summary>
        public int RecruitedNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 技能要求
        /// </summary>
        public string SkillRequirements { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public  UserDetails Addby { get; set; }
    }
}