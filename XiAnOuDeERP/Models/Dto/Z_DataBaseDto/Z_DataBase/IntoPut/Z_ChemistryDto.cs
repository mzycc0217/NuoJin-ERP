using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.IntoPut
{
    public class Z_ChemistryDto : ParentsDelAlls
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public string Name { get; set; }
        /// <summary>
        /// 公司自己的编码
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public string Encoding { get; set; }

        /// <summary>
        /// 录入人Id
        /// </summary>
       // [Required(ErrorMessage = "非空")]
        public long EntryPersonId { get; set; }

        /// <summary>
        /// 化学用品备注
        /// </summary>

        public string Desc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>

        public double? Number { get; set; }
        /// <summary>
        /// 化学用品类型
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public long Z_ChemistryTypeid { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; } = false;


        /// <summary>
        /// 化学用品英文名称
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 化学用品简称
        /// </summary>
        [Required(ErrorMessage = "非空")]
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
      //  [Required(ErrorMessage = "非空")]
        public string CASNumber { get; set; }

        /// <summary>
        /// 分子量
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public string MolecularWeight { get; set; }

        /// <summary>
        /// 分子式
        /// </summary>
        [Required(ErrorMessage = "非空")]
        public string MolecularFormula { get; set; }

        /// <summary>
        /// 结构式
        /// </summary>
        //[Required(ErrorMessage = "非空")]
        public string StructuralFormula { get; set; }
        /// <summary>
        /// 密度
        /// </summary>
        public string Density { get; set; }

        /// <summary>
        /// 危害声明
        /// </summary>

        public string Statement { get; set; }


        /// <summary>
        /// 警示声明
        /// </summary>

        public string Caution { get; set; }

        /// <summary>
        /// 外观状态
        /// </summary>

        public string AppearanceState { get; set; }


        /// <summary>
        /// 入库类型
        /// </summary>
        public long? WarehousingTypeId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public long? Companyid { get; set; }

    }
}