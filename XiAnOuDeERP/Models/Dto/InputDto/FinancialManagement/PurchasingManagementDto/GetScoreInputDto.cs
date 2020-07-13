using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 获取供应商分数输入类
    /// </summary>
    public class GetScoreInputDto:InputBase
    {
        /// <summary>
        /// 供应商分数Id
        /// </summary>
        public long? ScoreId { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public long? AddbyId { get; set; }
    }
}