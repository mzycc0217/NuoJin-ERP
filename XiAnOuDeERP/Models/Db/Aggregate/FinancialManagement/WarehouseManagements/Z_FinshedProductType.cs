using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    
        // <summary>
        /// 成品半成品类型
        /// </summary>
        [Table("Z_FinshedProductType")]
        public class Z_FinshedProductType : EntityBase
        {
        /// <summary>
        /// 成品半成品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 成品半成品类型描述
        /// </summary>
        public string Dec { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
         public bool IsDelete { get; set; }
        }
    }
