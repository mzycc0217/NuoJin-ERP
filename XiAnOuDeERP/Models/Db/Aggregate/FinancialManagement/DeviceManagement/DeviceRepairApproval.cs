using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 设备报修审核记录表
    /// </summary>
    [Table("DeviceRepairApproval")]
    public class DeviceRepairApproval:EntityBase
    {
        /// <summary>
        /// 设备报修Id
        /// </summary>
        public long DeviceRepairId { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsApproval { get; set; }

        /// <summary>
        /// 审核人用户类型key
        /// </summary>
        public string UserTypeKey { get; set; }

        /// <summary>
        /// 审批顺序
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 设备维修记录
        /// </summary>
        public virtual DeviceRepair DeviceRepair { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual UserDetails User { get; set; }
    }
}