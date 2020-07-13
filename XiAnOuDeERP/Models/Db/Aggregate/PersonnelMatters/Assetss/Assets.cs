using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss
{
    /// <summary>
    /// 资产实体类
    /// </summary>
    [Table("Assets")]
    public class Assets:EntityBase
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        [Required]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 资产类型Id
        /// </summary>
        [Required]
        public long AssetsTypeId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public double Number { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 资产类型
        /// </summary>
        public virtual AssetsType AssetsType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual Company Company { get; set; }
    }
}