using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WageManagementDto
{
    /// <summary>
    /// 获取工资输入类
    /// </summary>
    public class GetWageInputDto:InputBase
    {
        /// <summary>
        /// 工资Id
        /// </summary>
        public long? WageId { get; set; }

        /// <summary>
        /// 收款人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 收款人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 签字人Id
        /// </summary>
        public long? SignId { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 截至时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}