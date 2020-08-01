using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Saled.Sale_Input
{

    /// <summary>
    /// 销售产品前额和记录表
    /// </summary>
    public class Product_Sale_OutRepter_RecordInDto
    {
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long Sale_Id { get; set; }
        /// <summary>
        ///描述
        /// </summary>
      
        public string ContentDes { get; set; }
    }
}