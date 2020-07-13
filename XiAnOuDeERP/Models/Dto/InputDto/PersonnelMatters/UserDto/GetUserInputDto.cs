using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    public class GetUserInputDto:InputBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public long? UserTypeId { get; set; }

        /// <summary>
        /// 用户类型Str
        /// </summary>
        public string UserTypeName { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public EPositionType? PositionType { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DepartmentId { get; set; }
    }
}