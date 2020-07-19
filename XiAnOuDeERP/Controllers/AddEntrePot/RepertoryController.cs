using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.AddEntrePot
{

    [AppAuthentication]
    [RoutePrefix("api/ZpurChase")]
    public class RepertoryController : ApiController
    {

        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        //[HttpPost]
        //public async Task<IHttpActionResult> GetRepertoryInforMation(InputBase input)
        //{
        //    if (input.PageIndex!=null&&input.PageSize!=null)
        //    {

              


        //    }

        //}
    }
}
