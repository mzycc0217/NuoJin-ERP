using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.RawMaterialDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.PurchasingManagementDto
{
    /// <summary>
    /// 供应商输出类
    /// </summary>
    public class SupplierOutputDto:OutputBase
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Fraction { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 基础资料Id
        /// </summary>
        public string RawMaterialId { get; set; }

        /// <summary>
        /// 基础资料
        /// </summary>
        public RawMaterialOutputDto RawMaterial { get; set; }
    }
}