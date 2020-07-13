using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产收入输出类
    /// </summary>
    public class AssetIncomeOutputDto:OutputBase
    {
        /// <summary>
        /// 资产收入Id
        /// </summary>
        public string AssetIncomeId { get; set; }

        /// <summary>
        /// 收入人Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 销售信息Id
        /// </summary>
        public string SaleId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 销售信息
        /// </summary>
        public Sale Sale { get; set; }

        /// <summary>
        /// 收入人
        /// </summary>
        public UserDetails User { get; set; }
    }
}