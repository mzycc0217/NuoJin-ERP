using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 设备输出类
    /// </summary>
    public class DeviceOutputDto:OutputBase
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 录入人Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 使用情况
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 使用年限
        /// </summary>
        public double ServiceLife { get; set; }

        /// <summary>
        /// 技术说明
        /// </summary>
        public string TechnicalDescription { get; set; }

        /// <summary>
        /// 是否报废
        /// </summary>
        public bool IsScrap { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 是否报修
        /// </summary>
        public bool IsRepair { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public UserDetails User { get; set; }

        /// <summary>
        /// 报修次数
        /// </summary>
        public int RepairCount { get; set; }
    }
}