using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// 获取人员需求输入类
    /// </summary>
    public class GetPersonnelRtsInputDto:InputBase
    {
        /// <summary>
        /// 人员需求Id
        /// </summary>
        public long? PersonnelRtsId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public long? AddbyId { get; set; }

        /// <summary>
        /// 添加人姓名
        /// </summary>
        public string AddbyName { get; set; }

        /// <summary>
        /// 审核人Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 获取当前登录人待审批数据
        /// </summary>
        public bool IsApproval { get; set; }
    }
}