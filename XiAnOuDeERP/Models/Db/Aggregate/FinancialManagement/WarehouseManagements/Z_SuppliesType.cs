using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 物料类型
    /// </summary>
        [Table("Z_SuppliesType")]
        public class Z_SuppliesType : EntityBase
        {
            /// <summary>
            /// 出库类型名称
            /// </summary>
            public string Name { get; set; }


            public string Dec { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
            public bool IsDelete { get; set; }
        }
    
}