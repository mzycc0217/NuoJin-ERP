using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.RawMaterialDto
{
    /// <summary>
    /// 原材料输出类
    /// </summary>
    public class RawMaterialOutputDto:OutputBase
    {
        /// <summary>
        /// 原材料Id
        /// </summary>
        public string RawMaterialId { get; set; }

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
        /// 入库类型Id
        /// </summary>
        public string WarehousingTypeId { get; set; }

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
        /// 录入人Id
        /// </summary>
        public string EntryPersonId { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public UserDetails EntryPerson { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public WarehousingType WarehousingType { get; set; }
    }
}