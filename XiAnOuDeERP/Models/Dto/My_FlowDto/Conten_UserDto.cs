using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.My_FlowDto
{
    public class Conten_UserDto
    {
        /// <summary>
        /// 取到内容id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 获取到前和人员id
        /// </summary>
        public long user_id { get; set; }
      


        public string desc { get; set; }
    
    }
}