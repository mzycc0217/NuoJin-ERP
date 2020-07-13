using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 设备维修输入类
    /// </summary>
    public class DeviceRepairInputDto
    {
        /// <summary>
        /// 设备维修Id
        /// </summary>
        public long DeviceRepairId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public long DeviceId { get; set; }

        /// <summary>
        /// 报修人Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 报修备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }
    }
}