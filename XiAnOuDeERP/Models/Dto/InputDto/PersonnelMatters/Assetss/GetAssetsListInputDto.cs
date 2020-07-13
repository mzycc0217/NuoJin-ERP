using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 获取资产信息列表输入类
    /// </summary>
    public class GetAssetsListInputDto:InputBase
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 资产类型Id
        /// </summary>
        public long? AssetsTypeId { get; set; }
    }
}