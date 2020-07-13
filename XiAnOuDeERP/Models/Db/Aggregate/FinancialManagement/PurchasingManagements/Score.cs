using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements
{
    /// <summary>
    /// 评分表
    /// </summary>
    [Table("Score")]
    public class Score:EntityBase
    {
        /// <summary>
        /// 分数
        /// </summary>
        public int Fraction { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public long SupplierId { get; set; }

        /// <summary>
        /// 添加人Id
        /// </summary>
        public long AddbyId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public virtual UserDetails Addby { get; set; }
    }
}