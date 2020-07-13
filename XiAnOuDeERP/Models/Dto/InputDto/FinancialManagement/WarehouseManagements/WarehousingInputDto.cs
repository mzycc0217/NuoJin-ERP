using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 入库输入类
    /// </summary>
    public class WarehousingInputDto
    {
        /// <summary>
        /// 入库Id
        /// </summary>
        public long WarehousingId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public long? PurchaseId { get; set; }

        /// <summary>
        /// 基础资料Id
        /// </summary>
        public long? RawMaterialId { get; set; }

    }
}