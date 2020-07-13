using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut
{
    public class ChemistryTypeDto : ParentsDelAll
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