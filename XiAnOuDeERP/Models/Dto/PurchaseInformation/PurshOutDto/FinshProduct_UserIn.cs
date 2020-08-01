using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto
{
 //   签核记录
    public class FinshProduct_UserIn : InputBase
    { 
        
        /// <summary>
        /// 申请单id
        /// </summary> 

        public long FinishProductId { get; set; }
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
       
        /// <summary>
        ///描述
        /// </summary>

        public string ContentDes { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>

        public long enportid { get; set; }


    }
}