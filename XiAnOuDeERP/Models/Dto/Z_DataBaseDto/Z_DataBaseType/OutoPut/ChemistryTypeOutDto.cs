using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut
{
    public class ChemistryTypeOutDto : InputBase
    {
        /// <summary>
        /// 化学用品 Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        ///化学用品类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 化学描述
        /// </summary>
        public string Dec { get; set; }
    }
}