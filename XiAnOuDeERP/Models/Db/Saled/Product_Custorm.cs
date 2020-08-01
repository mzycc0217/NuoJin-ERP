using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Saled
{


    /// <summary>
    ///客户表
    /// </summary>
    [Table("Product_Custorm")]
    public class Product_Custorm : EntityBase
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [StringLength(40)]
        public string CustomName { get; set; }


        /// <summary>
        /// 公司名称
        /// </summary>
        [StringLength(40)]
        public string CustomCompany { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [StringLength(40)]
        public string CustomAddress { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(20)]
        public string CustommePhnes { get; set; }


        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20)]
        public string CoustomPhone { get; set; }

        /// <summary>
        /// 客户业务
        /// </summary>
        [StringLength(200)]
        public string Business { get; set; }

        
    }
}