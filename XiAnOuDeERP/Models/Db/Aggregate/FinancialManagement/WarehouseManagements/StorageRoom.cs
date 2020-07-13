using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 库存实体类
    /// </summary>
    [Table("StorageRoom")]
    public class StorageRoom:EntityBase
    {
        /// <summary>
        /// 物料Id
        /// </summary>
     // [Required]
       public long? RawMaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public double Number { get; set; }

        /// <summary>
        /// 库管人Id
        /// </summary>
        public long? WarehouseKeeperId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 物料
        /// </summary>
        public virtual RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public virtual UserDetails WarehouseKeeper { get; set; }
    }
}