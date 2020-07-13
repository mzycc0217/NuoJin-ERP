using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产类型输出类
    /// </summary>
    public class AssetsTypeOutputDto:OutputBase
    {
        /// <summary>
        /// 资产类型Id
        /// </summary>
        public string AssetsTypeId { get; set; }

        /// <summary>
        /// 资产类型名称
        /// </summary>
        public string AssetsTypeName { get; set; }
    }
}