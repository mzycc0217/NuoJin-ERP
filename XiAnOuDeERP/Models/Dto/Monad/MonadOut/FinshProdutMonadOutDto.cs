using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.Monad.MonadOut
{
    public class FinshProdutMonadOutDto:InputBase
    {

        /// <summary>
        /// 申请单id
        /// </summary>
        public string Id { get; set; }


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
        /// 获取数量
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
        ///成品半成品标志（1）成品（2半成品）
        /// </summary>

        public int Finshed_Sign { get; set; }
        /// <summary>
        /// 附件
        /// </summary>

        public string Enclosure { get; set; }

        /// <summary>
        /// 申请人备注说明
        /// </summary>


        public string ApplicantRemarks { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 申请是否完成 3代表完成
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
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public string ApplicantId { get; set; }


        /// <summary>
        /// 供货商Id
        /// </summary>
        public string SupplierId { get; set; }



        /// <summary>
        /// 删除
        /// </summary>
        public List<long> Del_Id { get; set; }

        /// <summary>
        /// 成品半成品
        /// </summary>

        public string FnishedProductId { get; set; }

        public Z_FnishedProduct z_FnishedProduct { get; set; }


        /// <summary>
        /// 化学用品单位
        /// </summary>



        public Company Company { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public UserDetails Applicant { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// 个兄姓名
        /// </summary>
        public string RelName { get; set; }
    }
}