using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut
{
    public class FinshedProductTypeOutDto:InputBase
    {
        /// <summary>
        /// 成品半成品Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 成品半成品类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 成品半成品描述
        /// </summary>
        public string Dec { get; set; }
    }
}