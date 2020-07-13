using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentInputDto
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

    }
}