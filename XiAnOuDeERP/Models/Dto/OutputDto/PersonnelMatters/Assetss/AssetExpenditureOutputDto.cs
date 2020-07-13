using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产支出输出类
    /// </summary>
    public class AssetExpenditureOutputDto:OutputBase
    {
        /// <summary>
        /// 资产支出Id
        /// </summary>
        public string AssetExpenditureId { get; set; }

        /// <summary>
        /// 支出人Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 支出人
        /// </summary>
        public virtual UserDetails User { get; set; }

        /// <summary>
        /// 采购
        /// </summary>
        public virtual Purchase Purchase { get; set; }
    }
}