using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 入库申请数据输出表
    /// </summary>
    public class WarehousingOutputDto:OutputBase
    {
        /// <summary>
        /// 入库Id
        /// </summary>
        public string WarehousingId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public UserDetails Applicant { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public string WarehouseKeeperId { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public UserDetails WarehouseKeeper { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public Purchase Purchase { get; set; }
    }
}