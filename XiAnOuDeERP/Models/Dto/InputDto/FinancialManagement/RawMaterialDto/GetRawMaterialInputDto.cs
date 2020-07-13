using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 获取物料信息输入类
    /// </summary>
    public class GetRawMaterialInputDto:InputBase
    {
        /// <summary>
        /// 物料Id
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public long? WarehousingTypeId { get; set; }

        /// <summary>
        /// 物料类别
        /// </summary>
        public string RawMaterialType { get; set; }

    }
}