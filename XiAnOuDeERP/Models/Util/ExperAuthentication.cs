using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace XiAnOuDeERP.Models.Util
{
    public class ExperAuthentication:ExceptionFilterAttribute
    {
        public string dynamic()
        {

            return "错误";

        }
    }
}