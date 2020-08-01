using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto
{
    /// <summary>
    /// 化学用品签核
    /// </summary>
    public class Chemistry_UserInDto : InputBase
    {
        /// <summary>
        /// 人员id
        /// </summary>

        public long user_Id { get; set; }
        /// <summary>
        /// 内容id
        /// </summary> 

        public long ChemistryId { get; set; }
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