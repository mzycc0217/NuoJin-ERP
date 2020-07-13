using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{

    /// <summary>
    ///办公用品 类型
    /// </summary>
    [Table("Z_RowType")]
    public class Z_RowType : EntityBase
    {

        /// <summary>
        /// 办公用品用品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 办公用品品类型描述
        /// </summary>
        public string Dec { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}