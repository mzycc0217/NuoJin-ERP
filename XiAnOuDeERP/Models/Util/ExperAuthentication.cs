using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ExperAuthentication:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            string type = actionExecutedContext.ActionContext.ControllerContext.Controller.GetType().Name;
            //Type type = actionExecutedContext.ActionContext.ControllerContext.Controller.GetType();
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //object o = assembly.CreateInstance(type.FullName);
            //PropertyInfo[] propertyInfos = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //type.GetMethod();
            //controller = controller + "Controller";

            throw new HttpResponseException(new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new Exception{ HelpLink = (MethodBase.GetCurrentMethod()).ToString(), Source = type+"控制器异常" }))
            }) ;
        }
    }
}