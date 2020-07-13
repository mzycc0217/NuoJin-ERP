using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut
{
    public class FinshedProductTypeDto : ParentsDelAll
    {
        /// <summary>
        /// 产品 类型Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品 类型描述
        /// </summary>
        public string Dec { get; set; }
    }
}