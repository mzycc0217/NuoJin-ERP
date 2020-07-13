using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut
{
    public class OfficeTypeOutDto : InputBase
    {
        /// <summary>
        /// 办公用品耗材 Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        ///办公用品耗材类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 办公用品耗材描述
        /// </summary>
        public string Dec { get; set; }
    }
}