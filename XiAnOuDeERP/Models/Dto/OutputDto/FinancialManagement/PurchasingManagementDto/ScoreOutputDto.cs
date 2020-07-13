using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 供应商分数输出类
    /// </summary>
    public class ScoreOutputDto:OutputBase
    {
        /// <summary>
        /// 供应商分数Id
        /// </summary>
        public string ScoreId { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Fraction { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public string AddbyId { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public UserDetails Addby { get; set; }
    }
}