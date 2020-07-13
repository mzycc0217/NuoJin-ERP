using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Dto.OutputDto.Projects;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.AtlasManagements
{
    /// <summary>
    /// 图谱输出类
    /// </summary>
    public class AtlasOutputDto:OutputBase
    {
        /// <summary>
        /// 图谱Id
        /// </summary>
        public string AtlasId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 图谱文件路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime TestingTime { get; set; }

    }
}