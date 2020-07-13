using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产信息输出类
    /// </summary>
    public class AssetsOutputDto : OutputBase
    {
        /// <summary>
        /// 资产信息Id
        /// </summary>
        public string AssetsId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 资产类型Id
        /// </summary>
        public string AssetsTypeId { get; set; }

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
        public string CompanyId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// 资产类型
        /// </summary>
        public AssetsType AssetsType { get; set; }
    }
}