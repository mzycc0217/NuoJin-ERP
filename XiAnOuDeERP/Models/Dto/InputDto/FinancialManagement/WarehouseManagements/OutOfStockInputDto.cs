using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 出库输入类
    /// </summary>
    public class OutOfStockInputDto
    {
        /// <summary>
        /// 出库Id
        /// </summary>
        public long OutOfStockId { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 出库类型Id
        /// </summary>
        public long OutOfStockTypeId { get; set; }

        #region 设备字段

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 使用情况
        /// </summary>
        public string Usage { get; set; }

        #endregion
    }
}