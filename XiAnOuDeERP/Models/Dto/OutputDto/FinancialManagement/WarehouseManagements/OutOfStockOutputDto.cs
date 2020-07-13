using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 出库数据输出类
    /// </summary>
    public class OutOfStockOutputDto: OutputBase
    {
        /// <summary>
        /// 出库Id
        /// </summary>
        public string OutOfStockId { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public string RawMaterialId { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double? Number { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public EApprovalType ApprovalType { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public UserDetails Applicant { get; set; }

        /// <summary>
        /// 出库类型Id
        /// </summary>
        public string OutOfStockTypeId { get; set; }

        /// <summary>
        /// 出库类型
        /// </summary>
        public OutOfStockType OutOfStockType { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public string WarehouseKeeperId { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public UserDetails WarehouseKeeper { get; set; }
    }
}