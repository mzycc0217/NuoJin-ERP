using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 获取资产收入输入类
    /// </summary>
    public class GetAssetIncomeListInputDto:InputBase
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 资产收入Id
        /// </summary>
        public long? AssetIncomeId { get; set; }

        /// <summary>
        /// 收入人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 销售Id
        /// </summary>
        public long? SaleId { get; set; }

    }
}