using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 获取设备报修输入类
    /// </summary>
    public class GetDeviceRepairInputDto:InputBase
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public long? DeviceId { get; set; }

        /// <summary>
        /// 基础资料Id
        /// </summary>
        public long RawMaterialId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备报修Id
        /// </summary>
        public long? DeviceRepairId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 审核人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 是否获取待审核数据
        /// </summary>
        public bool IsApproval { get; set; }
    }
}