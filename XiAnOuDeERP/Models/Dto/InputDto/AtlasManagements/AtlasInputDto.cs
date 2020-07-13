using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.AtlasManagements
{
    /// <summary>
    /// 图谱输入类
    /// </summary>
    public class AtlasInputDto
    {
        /// <summary>
        /// 图谱Id
        /// </summary>
        public long AtlasId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [Required]
        public string BatchNumber { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 图谱文件路径
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        [Required]
        public DateTime TestingTime { get; set; }
    }
}