using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace XiAnOuDeERP.Models.Dto
{
    public class ResponseOut
    {
        public string uid { get; set; }
        public HttpResponseMessage httpResponseMessage { get; set; }
    }
}