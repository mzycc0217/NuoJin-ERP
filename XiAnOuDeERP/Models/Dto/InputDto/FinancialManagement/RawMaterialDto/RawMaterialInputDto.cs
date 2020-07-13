using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 原材料实体类
    /// </summary>
    public class RawMaterialInputDto
    {
        /// <summary>
        /// 原材料Id
        /// </summary>
        public long? RawMaterialId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// 俗称1
        /// </summary>
        public string BeCommonlyCalled1 { get; set; }

        /// <summary>
        /// 俗称2
        /// </summary>
        public string BeCommonlyCalled2 { get; set; }

        /// <summary>
        /// CAS号
        /// </summary>
        public string CASNumber { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public long WarehousingTypeId { get; set; }

        /// <summary>
        /// 物料类别
        /// </summary>
        public string RawMaterialType { get; set; }

        /// <summary>
        /// 分子量
        /// </summary>
        public string MolecularWeight { get; set; }

        /// <summary>
        /// 分子式
        /// </summary>
        public string MolecularFormula { get; set; }

        /// <summary>
        /// 结构式
        /// </summary>
        public string StructuralFormula { get; set; }

        /// <summary>
        /// 外观状态
        /// </summary>
        public string AppearanceState { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public long? CompanyId { get; set; }
    }
}