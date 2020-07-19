using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.PurchaseInformation
{
    public class PurshDto
    {
        /// <summary>
        /// 采购单id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
     
        public string Purpose { get; set; }

        /// <summary>
        /// 期望到货日期
        /// </summary>
        public DateTime? ExpectArrivalTime { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public double? ApplyNumber { get; set; }

        /// <summary>
        /// 准购数量
        /// </summary>
        public double? QuasiPurchaseNumber { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? Amount { get; set; }


        /// <summary>
        /// 附件
        /// </summary>
       
        public string Enclosure { get; set; }

        /// <summary>
        /// 申请人备注说明
        /// </summary>

        public string ApplicantRemarks { get; set; }


        /// <summary>
        /// 采购数量
        /// </summary>

        public double PurchaseAmount { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 申请是否完成 0代表完成
        /// </summary>
        public int is_or { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime? PurchaseTime { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { get; set; }

        /// <summary>
        /// 采购合同
        /// </summary>
      
        public string PurchaseContract { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
      

        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApprovalDesc { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long? ApplicantId { get; set; }

        /// <summary>
        /// 审阅人id
        /// </summary>
        public long? User_Id { get; set; }
        /// <summary>
        /// 删除的（原来的）
        /// </summary>
    
        public long? RawMaterialId { get; set; }
        /// <summary>
        /// 原材料Id
        /// </summary>
     
        public long? RawId { get; set; }

    

        /// <summary>
        /// 项目Id
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// 供货商Id
        /// </summary>
        public long? SupplierId { get; set; }


     

   
    }
}