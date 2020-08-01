using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Saled.Sale_OutPut
{

    public class ProductSaleOutPut : InputBase
    {


        public string Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string FishProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double ProductNumber { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string Userid { get; set; }
   

        /// <summary>
        /// 用途
        /// </summary>
      //  [StringLength(255)]
        public string Behoof { get; set; }  
        
        /// <summary>
        /// 销售员备注
        /// </summary>
      //  [StringLength(255)]
        public string Des { get; set; }

        /// <summary>
        /// 准销售数量
        /// </summary>
        public double QuasiPurchaseNumber { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal Sale_Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 显示申请流程（1）领导审核物品出库（2）库管员出库（3）出库完成（4）完善销售信息，（6）领导审核（7）可销售状态，（8）销售状态，（12）撤销状态
        /// </summary>
        public int Is_Or { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? Sale_Time { get; set; }

        public  Z_FnishedProduct z_FnishedProduct { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public  Supplier Supplier { get; set; }


        public  UserDetails userDetails { get; set; }


        /// <summary>
        /// 人员姓名（模糊查询）
        /// </summary>
        public string RelName { get; set; }

        /// <summary>
        /// 物品名称（模糊查询）
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 获取自己的id(获取个人出库完成得单子 传1)
        /// </summary>
         public int U_Id { get; set; }

    }
}