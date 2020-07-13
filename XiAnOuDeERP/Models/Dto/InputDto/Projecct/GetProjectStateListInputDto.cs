using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.Projecct
{
    /// <summary>
    /// 获取项目状态输入类
    /// </summary>
    public class GetProjectStateListInputDto:InputBase
    {
        /// <summary>
        /// 项目状态Id
        /// </summary>
        public long? ProjectStateId { get; set; }

        /// <summary>
        /// 项目状态名称
        /// </summary>
        public string Name { get; set; }
    }
}