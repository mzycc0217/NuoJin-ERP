using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产信息输入类
    /// </summary>
    public class AssetsInputDto
    {
        /// <summary>
        /// 资产信息Id
        /// </summary>
        public long AssetsId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 资产类型
        /// </summary>
        public long AssetsTypeId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public long CompanyId { get; set; }
    }
}