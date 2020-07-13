using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 设备维修实体类
    /// </summary>
    [Table("DeviceRepair")]
    public class DeviceRepair:EntityBase
    {
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
        [StringLength(255)]
        public string Desc { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 审核流Key
        /// </summary>
        public string ApprovalKey { get; set; }

        /// <summary>
        /// 当前审核
        /// </summary>
        public int ApprovalIndex { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public virtual Device Device { get; set; }

        /// <summary>
        /// 报修人
        /// </summary>
        public virtual UserDetails  User { get; set; }
    }
}