using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements
{
    // <summary>
    /// 化学用品
    /// </summary>
    [Table("Z_ChemistryType")]
    public class Z_ChemistryType :EntityBase
    {
        /// <summary>
        /// 化学用品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 化学用品类型描述
        /// </summary>
        public string Dec { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}