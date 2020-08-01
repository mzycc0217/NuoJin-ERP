using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Db.Saled;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Saled.Sale_OutPut
{
    public class Hostiry_Prace_SaleOut : InputBase
    {



        /// <summary>
        /// 历史记录id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public string FinshProductId { get; set; }


        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string FinshProductDes { get; set; }

        /// <summary>
        /// 销售专员
        /// </summary>
        public string User_Id { get; set; }


        /// <summary>
        /// 客户id
        /// </summary>
        public string CustomNameId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Price_Time { get; set; }

  


        public virtual Z_FnishedProduct z_FnishedProduct { get; set; }

        public virtual UserDetails UserDetails { get; set; }

        public virtual Product_Custorm Product_Custorm { get; set; }

        /// <summary>
        ///  模糊查询(客户姓名)
        /// </summary>
        public string CustomName { get; set; }
        /// <summary>
        ///  模糊查询(客户公司)
        /// </summary>
        public string CustomCompany { get; set; }

        /// <summary>
        ///  模糊查询(产品名称)
        /// </summary>
        public string FinshProductName { get; set; }

        /// <summary>
        ///  模糊查询(产品编号)
        /// </summary>
        public string FinshProductEcode { get; set; }

        /// <summary>
        ///  模糊查询(销售员姓名)
        /// </summary>
        public string RelName { get; set; }


        /// <summary>
        /// 开始时间(查询)
        /// </summary>
        public DateTime? start_Time { get; set; }

        /// <summary>
        ///结束时间(查询)
        /// </summary>
        public DateTime? end_Time { get; set; }

    }
}