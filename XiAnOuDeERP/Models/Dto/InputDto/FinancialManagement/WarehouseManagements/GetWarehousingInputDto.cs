using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 获取入库申请输入类
    /// </summary>
    public class GetWarehousingInputDto:InputBase
    {
        /// <summary>
        /// 入库Id
        /// </summary>
        public long? WarehousingId { get; set; }

        /// <summary>
        /// 原材料名称
        /// </summary>
        public string RawMaterialName { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public long? WarehousingTypeId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EApprovalType? ApprovalType { get; set; }

        /// <summary>
        /// 审核人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 是否获取待审核数据
        /// </summary>
        public bool IsApproval { get; set; }


    }
}