using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut
{
    public class SuppliesTypeOutDto : InputBase
    {
        /// <summary>
        /// 物料Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        public string Dec { get; set; }
    }
}