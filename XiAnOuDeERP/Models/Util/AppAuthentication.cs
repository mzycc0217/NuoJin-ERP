using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// 身份验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
    public class AppAuthentication : ActionFilterAttribute
    {
        public bool Cancel { get; set; }

        public string Permission { get; set; }

        public AppAuthentication()
        {
            Cancel = false;
            Permission = "*";
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!Cancel)
            {
                try
                {
                    var authHeader = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
                    var userId = actionContext.Request.Headers.GetValues("UserId").FirstOrDefault();
                    var appFlag = actionContext.Request.Headers.GetValues("AppFlag").FirstOrDefault();

                    if (!String.IsNullOrWhiteSpace(authHeader) &&
                        !String.IsNullOrWhiteSpace(userId) &&
                        authHeader.StartsWith("Bearer"))
                    {
                        string token = authHeader.Substring("Bearer ".Length).Trim();

                        if (!String.IsNullOrWhiteSpace(token))
                        {
                            var data = JwtHelper.Decode(token);//解密token

                            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(data);//将Token转换成实体类

                            var tokenKey = RedisKeyManger.GetWebTokenKey(tokenDto.UserId);

                            if (!String.IsNullOrWhiteSpace(appFlag) && appFlag == "App")
                            {
                                tokenKey = RedisKeyManger.GetAppTokenKey(tokenDto.UserId);
                            }

                            if (RedisHelper.KeyExist(tokenKey))
                            {
                                var oldToken = RedisHelper.StringGet(tokenKey);

                                if (oldToken == token)
                                {
                                    (actionContext.ControllerContext.Controller as ApiController).User = new ApplicationUser("", long.Parse(userId), authHeader, tokenDto);
                                }
                                else
                                {
                                    ResponseApi result = new ResponseApi();
                                    result.Code = Enum.EExceptionType.NoLogin;
                                    result.Message = "用户未登录";
                                    throw new HttpResponseException(new HttpResponseMessage()
                                    {
                                        Content = new StringContent(JsonConvert.SerializeObject(result))
                                    });
                                }
                            }
                            else
                            {
                                ResponseApi result = new ResponseApi();
                                result.Code = Enum.EExceptionType.NoLogin;
                                result.Message = "用户未登录";
                                throw new HttpResponseException(new HttpResponseMessage()
                                {
                                    Content = new StringContent(JsonConvert.SerializeObject(result))
                                });
                            }
                        }
                        else
                        {
                            ResponseApi result = new ResponseApi();
                            result.Code = Enum.EExceptionType.NoLogin;
                            result.Message = "用户未登录";
                            throw new HttpResponseException(new HttpResponseMessage()
                            {
                                Content = new StringContent(JsonConvert.SerializeObject(result))
                            });
                        }
                    }
                    else
                    {
                        ResponseApi result = new ResponseApi();
                        result.Code = Enum.EExceptionType.NoLogin;
                        result.Message = "用户未登录";
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(result))
                        });
                    }
                }
                catch (Exception ex)
                {
                    ResponseApi result = new ResponseApi();
                    result.Code = Enum.EExceptionType.NoLogin;
                    result.Message = "用户未登录";
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(result))
                    });
                }

            }
        }

    }
}