using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentOutputDto:OutputBase
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 是否注销
        /// </summary>
        public bool IsCancellation { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门经理Id
        /// </summary>
        public List<string> ManagerId { get; set; }

        /// <summary>
        /// 部门经理
        /// </summary>
        public List<UserOutputDto> Manager { get; set; }

        /// <summary>
        /// 部门人数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }
    }
}