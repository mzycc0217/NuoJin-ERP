using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.AtlasManagements
{
    /// <summary>
    /// 获取图谱输入类
    /// </summary>
    public class GetAtlasInputDto:InputBase
    {
        /// <summary>
        /// 图谱Id
        /// </summary>
        public long? AtlasId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public long? ProjectId { get; set; }
    }
}