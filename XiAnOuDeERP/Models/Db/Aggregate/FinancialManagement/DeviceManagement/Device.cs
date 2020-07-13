using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.DeviceManagement
{
    /// <summary>
    /// 设备实体类
    /// </summary>
    [Table("Device")]
    public class Device:EntityBase
    {
        /// <summary>
        /// 录入人Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 使用情况
        /// </summary>
        [StringLength(255)]
        public string Usage { get; set; }

        /// <summary>
        /// 是否报废
        /// </summary>
        public bool IsScrap { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 是否报修
        /// </summary>
        public bool IsRepair { get; set; }

        /// <summary>
        /// 基础资料Id
        /// </summary>
        public long RawMaterialId { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 基础资料
        /// </summary>
        public virtual RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}