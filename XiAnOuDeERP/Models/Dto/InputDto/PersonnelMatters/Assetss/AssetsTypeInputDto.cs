using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产类型输入类
    /// </summary>
    public class AssetsTypeInputDto
    {
        /// <summary>
        /// 资产类型Id
        /// </summary>
        public long AssetsTypeId { get; set; }

        /// <summary>
        /// 资产类型名称
        /// </summary>
        public string AssetsTypeName { get; set; }
    }
}