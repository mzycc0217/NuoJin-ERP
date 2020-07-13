using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 报修信息输出类
    /// </summary>
    public class DeviceRepairOutputDto:OutputBase
    {
        /// <summary>
        /// 报修信息Id
        /// </summary>
        public string DeviceRepairId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 报修人Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 报修备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public DeviceOutputDto Device { get; set; }

        /// <summary>
        /// 报修人
        /// </summary>
        public UserOutputDto User { get; set; }
    }
}