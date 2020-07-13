using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 获取采购申请列表输入类
    /// </summary>
    public class GetPurchaseListInputDto:InputBase
    {
        /// <summary>
        /// 申请采购Id
        /// </summary>
        public long? PurchaseId { get; set; }

        /// <summary>
        /// 审核人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 是否获取待审核数据
        /// </summary>
        public bool IsApproval { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 原材料Id
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 原材料名称
        /// </summary>
        public string RawMaterialName { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long? ApplicantId { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public long? WarehousingTypeId { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 采购部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 审核人id
        /// </summary>
        public long? user_id { get; set; }
    }
}