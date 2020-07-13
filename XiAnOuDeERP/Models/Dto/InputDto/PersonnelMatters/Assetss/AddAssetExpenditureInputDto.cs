using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 添加资产支出输入类
    /// </summary>
    public class AddAssetExpenditureInputDto
    {

        /// <summary>
        /// 采购Id
        /// </summary>
        public long? PurchaseId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }
    }
}