using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 获取请假列表输入类
    /// </summary>
    public class GetLeaveInputDto:InputBase
    {
        /// <summary>
        /// 请假Id
        /// </summary>
        public long? LeaveId { get; set; }

        /// <summary>
        /// 请假人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 请假人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public long? ApprovelId { get; set; }

        /// <summary>
        /// 获取当前登录人待审核数据
        /// </summary>
        public bool IsApproval { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

    }
}