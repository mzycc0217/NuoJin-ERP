using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 获取设备信息输入类
    /// </summary>
    public class GetDeviceInputDto:InputBase
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public long? DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 使用情况
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 使用年限
        /// </summary>
        public double? ServiceLife { get; set; }
    }
}