using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Saled.Sale_Input
{
    public class ProductSaleInput
    {

        /// <summary>
        /// 申请单Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public long FishProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double ProductNumber { get; set; }



        /// <summary>
        /// 销售员
        /// </summary>
        public long Userid { get; set; }
        /// <summary>
        /// 销售员备注
        /// </summary>
       
        public string Des { get; set; }

        /// <summary>
        /// 准销售数量
        /// </summary>
        public double QuasiPurchaseNumber { get; set; }


        /// <summary>
        /// 显示申请流程（1）领导审核物品出库（2）库管员出库（3）出库完成（4）完善销售信息，（6）领导审核（7）可销售状态，（8）销售状态，（12）撤销状态
        /// </summary>
        public int Is_Or { get; set; }
        /// <summary>
        /// 用途
        /// </summary>

        public string Behoof { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal Sale_Price { get; set; }


        /// <summary>
        /// 供货商
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? Sale_Time { get; set; } = DateTime.Now;

    }
}