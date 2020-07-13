using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 人员需求输入类
    /// </summary>
    public class PersonnelRtsInputDto
    {
        /// <summary>
        /// 人员需求Id
        /// </summary>
        public long PersonnelRtsId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public long AddbyId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

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
    }
}