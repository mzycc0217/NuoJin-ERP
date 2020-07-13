using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 加班输入类
    /// </summary>
    public class OvertimeInputDto
    {
        /// <summary>
        /// 加班Id
        /// </summary>
        public long OvertimeId { get; set; }

        /// <summary>
        /// 加班日期
        /// </summary>
        public DateTime OverTimeDate { get; set; }

        /// <summary>
        /// 加班时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 部门领导Id
        /// </summary>
        public long? DepartmentLeaderId { get; set; }

    }
}