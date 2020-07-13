using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 获取资产支出列表输入类
    /// </summary>
    public class GetAssetExpenditureListInputDto:InputBase
    {
        /// <summary>
        /// 资产支出Id
        /// </summary>
        public long? AssetExpenditureId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 支出人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public long? PurchaseId { get; set; }
    }
}