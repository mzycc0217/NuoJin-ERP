using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut
{
    public class OfficeTypeDto : ParentsDelAll
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