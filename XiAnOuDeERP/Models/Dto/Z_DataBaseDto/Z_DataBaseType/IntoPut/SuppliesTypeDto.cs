using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType
{
    public class SuppliesTypeDto : ParentsDelAll
    {

        /// <summary>
        /// 物料 类型Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 物料  类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 物料  类型描述
        /// </summary>
        public string Dec { get; set; }
    }
}