using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 评分输入类
    /// </summary>
    public class ScoreInputDto:InputBase
    {
        /// <summary>
        /// 评分Id
        /// </summary>
        public long ScoreId { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Fraction { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public long SupplierId { get; set; }
    }
}