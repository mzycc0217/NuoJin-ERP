using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using XiAnOuDeERP.MethodWay;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto.My_FlowDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.PersonnelMatters.Controllers.UserAppService
{
    /// <summary>
    /// 用户
    /// </summary>
    [AppAuthentication]
    public class UserController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthentication(Cancel = true)]
        public async Task<LoginOutputDto> Login(LoginInputDto input)
        {
            if (input.VerificationCode!=null&&input.Verification!=null)
            {
               var yzm= RedisHelper.StringGet(input.Verification);
                if (yzm.ToUpper()!= input.VerificationCode.ToUpper())
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "验证码错误" }))
                    });
                }
                if (yzm == input.VerificationCode)
                {

                    var passWord = NetCryptoHelper.EncryptAes(input.Password, NetCryptoHelper.AesKey);

                    var user = await db.UserDetails
                        .Include(m => m.User)
                        .SingleOrDefaultAsync(m => m.User.UserName == input.UserName && m.User.Password == passWord && !m.IsDelete);

                    if (user == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "用户名或密码错误" }))
                        });
                    }

                    var userDetailsType = await db.UserDetailsTypes.Include(m => m.User).Include(m => m.UserType).Where(m => m.UserId == user.Id).ToListAsync();

                    if (userDetailsType == null && userDetailsType.Count < 1)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "用户未绑定该类型" }))
                        });
                    }

                    var userTypes = await db.UserDetailsTypes.Include(m => m.UserType).Where(m => m.UserId == user.Id).Select(m => m.UserType).ToListAsync();

                    var userTypeKeys = userTypes.Select(m => m.Key).ToList();
                    var userTypeIds = userTypes.Select(m => m.Id).ToList();
                    var userTypeNames = userTypes.Select(m => m.Name).ToList();

                    var payload = new Dictionary<string, object>
                        {
                            {"userId", user.Id},
                            {"userName",user.User.UserName},
                            //{"userTypeId",userDetailsType.UserTypeId},
                            //{"userTypeKey",userDetailsType.UserType.Key},
                            {"userTypeId",userTypeIds},
                            {"userTypeKey",userTypeKeys},
                            {"exp", DateTimeOffset.UtcNow.AddDays(15).ToFileTime()}
                        };//生成Token

                    var token = JwtHelper.CreateToken(payload);//加密Token

                    RedisHelper.StringSet(RedisKeyManger.GetWebTokenKey(user.Id), token, TimeSpan.FromMinutes(60)/*null*/);//将Token存入Redis中

                    return new LoginOutputDto()
                    {
                        Token = token,
                        User = new UserOutputDto()
                        {
                            UserId = user.Id.ToString(),
                            CreateDate = user.CreateDate,
                            Department = user.User.Department,
                            DepartmentId = user.User.DepartmentId.ToString(),
                            UserName = user.User.UserName,
                            UpdateDate = user.UpdateDate,
                            UserTypeStr = userTypeNames,
                            UserTypeId = userTypeIds.Select(m => m.ToString()).ToList(),
                            Address = user.Address,
                            DateOfBirth = user.DateOfBirth,
                            Education = user.Education,
                            Email = user.Email,
                            IdCard = user.IdCard,
                            Nation = user.Nation,
                            Phone = user.Phone,
                            PortraitPath = user.PortraitPath,
                            RealName = user.RealName,
                            SexType = user.SexType,
                            SexTypeStr = user.SexType.GetDescription(),
                            WeiXin = user.WeiXin,
                            PositionType = user.User.PositionType,
                            PositionTypeStr = user.User.PositionType.GetDescription(),
                            IsCancellation = user.IsCancellation
                        }
                    };


                }

                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "系统异常请联系管理员" }))
                });

            }

            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请填写验证码" }))
                });
            }
        }



        /// <summary>
        /// 验证码
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [AppAuthentication(Cancel = true)]
        public async Task<IHttpActionResult> GetVerCode()
        {
          
            Creatcodes code2 = CreatCode.CreatEnconds;
            string yzm = code2();
            YzmCode yzmCode = new YzmCode();
            var uid = Guid.NewGuid().ToString("N");
            //  result.Headers.Add("Verification", uid); 
            RedisHelper.StringSet(uid, yzm, TimeSpan.FromSeconds(60));
            var s = await Task.Run(()=>(  yzmCode.CreateCheckCodeImage(yzm))); 
          
            return Json(new {uid=uid,ver=s });
        
        }

        /// <summary>
        /// 登录时获取用户绑定用户类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthentication(Cancel = true)]
        public async Task<List<UserDetailsTypeOutputDto>> LoginGetUserDetailsTypeList(LoginInputDto input)
        {
            var passWord = NetCryptoHelper.EncryptAes(input.Password, NetCryptoHelper.AesKey);

            var user = await db.UserDetails
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.User.UserName == input.UserName && m.User.Password == passWord && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "用户名或密码错误" }))
                });
            }

            var data = await db.UserDetailsTypes
                .Include(m => m.UserType)
                .Where(m => m.UserId == user.Id)
                .ToListAsync();

            var list = new List<UserDetailsTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new UserDetailsTypeOutputDto()
                {
                    UserId = user.Id.ToString(),
                    UserTypeId = item.UserTypeId.ToString(),
                    UserType = new UserTypeOutputDto()
                    {
                        UserTypeId = item.UserType.Id.ToString(),
                        CreateDate = item.UserType.CreateDate,
                        Desc = item.UserType.Desc,
                        Key = item.UserType.Key,
                        Name = item.UserType.Name,
                        UpdateDate = item.UserType.UpdateDate
                    }
                });
            }

            if (list.Count == 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户未绑定用户类型" }))
                });
            }

            return list;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(UserInputDto input)
        {
            var Password = NetCryptoHelper.EncryptAes(input.Password == null ? "666666" : input.Password, NetCryptoHelper.AesKey);

            var user = await db.User.SingleOrDefaultAsync(m => m.UserName == input.UserName && m.PositionType != EPositionType.Quit && !m.IsDelete);

            if (user != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "用户名已存在" }))
                });
            }

            if (!await db.UserTypes.AnyAsync(m=>m.Id == input.UserTypeId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户类型不存在" }))
                });
            }

            var data = new User()
            {
                Id = IdentityManager.NewId(),
                Password = Password,
                UserName = input.UserName,
                PositionType = EPositionType.ToBeTrain,
                IsDelete = false
            };

            var Department = await db.Departments.SingleOrDefaultAsync(m => m.Id == input.DepartmentId && !m.IsDelete);

            if (Department != null)
            {
                data.DepartmentId = Department.Id;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "部门不存在" }))
                });
            }

            db.User.Add(data);

            var data1 = new UserDetails()
            {
                Id = IdentityManager.NewId(),
                UserId = data.Id,
                Address = input.Address,
                DateOfBirth = input.DateOfBirth,
                Education = input.Education,
                Email = input.Email,
                Nation = input.Nation,
                Phone = input.Phone,
                RealName = input.RealeName,
                SexType = input.SexType,
                WeiXin = input.WeiXin,
                IdCard = input.IdCard,
                IsDelete = false,
                IsCancellation = false
            };

            db.UserDetails.Add(data1);

            var userDetailsType = new UserDetailsType()
            {
                Id = IdentityManager.NewId(),
                UserId = data1.Id,
                UserTypeId = (long)input.UserTypeId
            };

            db.UserDetailsTypes.Add(userDetailsType);
            
            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 用户绑定用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddUserDetailsType(AddUserDetailsType input)
        {
            if (!await db.UserDetails.AnyAsync(m=>m.Id == input.UserId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在" }))
                });
            }

            if (!await db.UserTypes.AnyAsync(m=>m.Id == input.UserTypeId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户类型不存在" }))
                });
            }

            if (await db.UserDetailsTypes.AnyAsync(m=>m.UserId == input.UserId && m.UserTypeId == input.UserTypeId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户已绑定当前用户类型" }))
                });
            }

            var userDetailsType = new UserDetailsType()
            {
                Id = IdentityManager.NewId(),
                UserId = input.UserId,
                UserTypeId = input.UserTypeId
            };

            db.UserDetailsTypes.Add(userDetailsType);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除用户绑定用户类型
        /// </summary>
        /// <param name="UserDetailsTypeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteUserDetailsTypeList(List<long> UserDetailsTypeIds)
        {
            var list = new List<string>();

            foreach (var item in UserDetailsTypeIds)
            {
                var data = await db.UserDetailsTypes.SingleOrDefaultAsync(m => m.Id == item);

                if (data != null)
                {
                    db.UserDetailsTypes.Remove(data);
                }
                else
                {
                    list.Add(item+"不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取用户绑定身份集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<UserDetailsTypeOutputDto>> GetUserDetailsTypeList(GetUserDetailsTypeListInputDto input)
        {
            var data = await db.UserDetailsTypes
                .Include(m=>m.User)
                .Include(m=>m.UserType)
                    .Where(m => !m.User.IsDelete)
                    .ToListAsync();

            if (input.UserDetailsTypeId != null)
            {
                data = data.Where(m => m.Id == input.UserDetailsTypeId).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (input.UserTypeId != null)
            {
                data = data.Where(m => m.UserTypeId == input.UserTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserRealName))
            {
                data = data.Where(m => m.User.RealName != null && m.User.RealName.Contains(input.UserRealName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeName))
            {
                data = data.Where(m => m.UserType.Name != null && m.UserType.Name.Contains(input.UserTypeName)).ToList();
            }

            var list = new List<UserDetailsTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new UserDetailsTypeOutputDto
                {
                    UserDeatilsTypeId = item.Id.ToString(),
                    UserId = item.UserId.ToString(),
                    UserTypeId = item.UserTypeId.ToString(),
                    UserType = new UserTypeOutputDto { 
                    UserTypeId = item.UserType.Id.ToString(),
                    CreateDate = item.UserType.CreateDate,
                    Desc = item.UserType.Desc,
                    Key = item.UserType.Key,
                    Name = item.UserType.Name,
                    UpdateDate = item.UserType.UpdateDate
                    },
                    User = new UserOutputDto { 
                    UpdateDate = item.User.UpdateDate,
                    CreateDate = item.User.CreateDate,
                    Address = item.User.Address,
                    DateOfBirth = item.User.DateOfBirth,
                    Department = item.User.User.Department,
                    DepartmentId = item.User.User.DepartmentId.ToString(),
                    Education = item.User.Education,
                    Email = item.User.Email,
                    IdCard = item.User.IdCard,
                    IsCancellation = item.User.IsCancellation,
                    Nation = item.User.Nation,
                    Phone = item.User.Phone,
                    PortraitPath = item.User.PortraitPath,
                    PositionType = item.User.User.PositionType,
                    PositionTypeStr = item.User.User.PositionType.GetDescription(),
                    RealName = item.User.RealName,
                    SexType = item.User.SexType,
                    SexTypeStr = item.User.SexType.GetDescription(),
                    UserId = item.UserId.ToString(),
                    UserName = item.User.User.UserName,
                    WeiXin = item.User.WeiXin
                    }
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.UserId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

        /// <summary>
        /// 添加用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddUserType(UserTypeInputDto input)
        {
            if (await db.UserTypes.AnyAsync(m => m.Key == input.Key))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "Key【" + input.Key + "】已存在" }))
                });
            }

            db.UserTypes.Add(new UserType()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                Desc = input.Desc,
                Key = input.Key,
                IsDelete = false
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateUserType(UserTypeInputDto input)
        {
            var data = await db.UserTypes.SingleOrDefaultAsync(m => m.Id == input.UserTypeId);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.UserTypeId+"用户类型不存在" }))
                });
            }

            if (await db.UserTypes.AnyAsync(m=>m.Key == input.Key && !m.IsDelete && m.Id != input.UserTypeId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Key + "已存在" }))
                });
            }

            if (data.Key != input.Key)
            {
                var approval = await db.Approvals.Where(m => m.UserTypeKey == data.Key).ToListAsync();

                foreach (var item in approval)
                {
                    item.UserTypeKey = input.Key;
                }

                data.Key = input.Key;
            }

            data.Desc = input.Desc;
            data.Name = input.Name;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除用户类型
        /// </summary>
        /// <param name="UserTypeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteUserTypeList(List<long> UserTypeIds)
        {
            var list = new List<string>();

            foreach (var item in UserTypeIds)
            {
                var userType = await db.UserTypes.SingleOrDefaultAsync(m => m.Id == item);
                if (userType != null)
                {
                    var approval = await db.Approvals.Where(m => m.UserTypeKey == userType.Key).ToListAsync();

                    foreach (var item1 in approval)
                    {
                        var approvalList = await db.Approvals.Where(m => m.Key == item1.Key && m.Deis > item1.Deis).ToListAsync();

                        foreach (var item2 in approvalList)
                        {
                            item2.Deis -= 1;
                        }

                        db.Approvals.Remove(item1);
                    }

                    var userTypes = await db.UserDetailsTypes.Where(m => m.UserTypeId == userType.Id).ToListAsync();

                    foreach (var item2 in userTypes)
                    {
                        db.UserDetailsTypes.Remove(item2);
                    }

                    userType.IsDelete = true;
                }
                else
                {
                    list.Add("【" + item + "】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取用户类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<UserTypeOutputDto>> GetUserTypeList(GetUserTypeInputDto input)
        {
            var data = await db.UserTypes
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.UserTypeId != null)
            {
                data = data.Where(m => m.Id == input.UserTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.UserTypeName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeKey))
            {
                data = data.Where(m => m.Key != null && m.Key.Contains(input.UserTypeKey)).ToList();
            }

            var list = new List<UserTypeOutputDto>();

            foreach (var item in data)
            {
                //if (item.Key == "admin")
                //{
                //    continue;
                //}

                list.Add(new UserTypeOutputDto
                {
                    CreateDate = item.CreateDate,
                    Key = item.Key,
                    Desc = item.Desc,
                    Name = item.Name,
                    UpdateDate = item.UpdateDate,
                    UserTypeId = item.Id.ToString()
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.UserTypeId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(UserInputDto input)
        {
            var user = await db.UserDetails.Include(m => m.User).SingleOrDefaultAsync(m => m.Id == input.UserId && m.User.PositionType != EPositionType.Quit && !m.IsDelete);

            var department = new Department();

            if (user != null)
            {
                if (input.DepartmentId != null)
                {
                    department = await db.Departments.SingleOrDefaultAsync(m => m.Id == input.DepartmentId && !m.IsDelete);
                    if (department == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该部门不存在" }))
                        });
                    }

                    if (user != null)
                    {
                        user.User.DepartmentId = (long)input.DepartmentId;
                    }
                }

                user.User.PositionType = input.PositionType;

                user.Nation = input.Nation;
                user.Phone = input.Phone;
                user.RealName = input.RealeName;
                user.SexType = input.SexType;
                user.WeiXin = input.WeiXin;
                user.Address = input.Address;
                user.DateOfBirth = input.DateOfBirth;
                user.Education = input.Education;
                user.Email = input.Email;
                user.IdCard = input.IdCard;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在或已注销" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }
        }

        /// <summary>
        /// 修改用户职位状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdatePositionType(UserInputDto input)
        {
            var user = await db.UserDetails
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.Id == input.UserId && m.User.PositionType != EPositionType.Quit && !m.IsDelete);

            if (user != null)
            {
                user.User.PositionType = input.PositionType;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdatePassword(UpdateUserPassWordInputDto input)
        {

            var userId = ((UserIdentity)User.Identity).UserId;


            var oldPassWord = NetCryptoHelper.EncryptAes(input.OldPassWord, NetCryptoHelper.AesKey);
            //  var users=await Task.Run(()=>db.User.SingleOrDefaultAsync(p=>p.))
            var resu = await db.UserDetails.Where(p => p.Id == userId).ToListAsync();
            long? us_id = null;
            foreach (var item in resu)
            {
                us_id = item.UserId;
            }
            
            var user = await db.User.SingleOrDefaultAsync(m => m.Id == us_id && m.PositionType != EPositionType.Quit && !m.IsDelete);

            if (string.IsNullOrWhiteSpace(input.PassWord))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "密码不能为空" }))
                });
            }

            if (user.Password != oldPassWord)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "旧密码错误" }))
                });
            }

            if (user != null)
            {
                user.Password = NetCryptoHelper.EncryptAes(input.PassWord, NetCryptoHelper.AesKey);
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> UserIds)
        {
            var list = new List<string>();

            foreach (var item in UserIds)
            {
                var user = await db.User.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                var userDetail = await db.UserDetails.SingleOrDefaultAsync(m => m.UserId == user.Id && !m.IsDelete);

                if (user != null)
                {
                    user.PositionType = EPositionType.Quit;
                    user.IsDelete = true;
                    userDetail.IsDelete = true;
                }
                else
                {
                    list.Add(item + "用户不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserListOuputDto>> GetUserList(GetUserInputDto input)
        {
            var data = await db.UserDetails
                .Include(m => m.User)
                .Where(m => !m.IsDelete && !m.User.IsDelete && !m.User.Department.IsDelete)
                .ToListAsync();

            if (input.UserTypeId != null)
            {
                data = await db.UserDetailsTypes.Where(m => m.UserTypeId == input.UserTypeId).Select(m=>m.User).ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(input.UserTypeName))
            {
                data = await db.UserDetailsTypes.Where(m => m.UserType.Name != null && m.UserType.Name.Contains(input.UserTypeName)).Select(m => m.User).ToListAsync();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.User.DepartmentId == input.DepartmentId).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.Id == input.UserId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RealName))
            {
                data = data.Where(m => m.RealName != null && m.RealName.Contains(input.RealName)).ToList();
            }

            if (input.PositionType != null)
            {
                data = data.Where(m => m.User.PositionType == input.PositionType).ToList();
            }

            var list = new List<GetUserListOuputDto>();

            foreach (var item in data)
            {
                var userTypeList = await db.UserDetailsTypes.Where(m => m.UserId == item.Id).ToListAsync();

                var userType = new List<UserDetailsTypeOutputDto>();

                foreach (var typeList in userTypeList)
                {
                    userType.Add(new UserDetailsTypeOutputDto
                    {
                        UserDeatilsTypeId = typeList.Id.ToString(),
                        UserType = new UserTypeOutputDto { 
                        CreateDate = typeList.UserType.CreateDate,
                        Desc = typeList.UserType.Desc,
                        Key = typeList.UserType.Key,
                        Name = typeList.UserType.Name,
                        UpdateDate = typeList.UserType.UpdateDate,
                        UserTypeId = typeList.UserType.Id.ToString()
                        },
                        UserTypeId = typeList.UserType.Id.ToString(),
                        UserId = typeList.UserId.ToString()
                    });
                }

                list.Add(new GetUserListOuputDto()
                {
                    UserId = item.Id.ToString(),
                    UserName = item.User.UserName,
                    CreateDate = item.CreateDate,
                    Department = item.User.Department,
                    DepartmentId = item.User.DepartmentId.ToString(),
                    UpdateDate = item.UpdateDate,
                    Address = item.Address,
                    DateOfBirth = item.DateOfBirth,
                    Education = item.Education,
                    Email = item.Email,
                    Nation = item.Nation,
                    Phone = item.Phone,
                    PortraitPath = item.PortraitPath,
                    RealName = item.RealName,
                    SexType = item.SexType,
                    WeiXin = item.WeiXin,
                    SexTypeStr = item.SexType.GetDescription(),
                    IdCard = item.IdCard,
                    PositionType = item.User.PositionType,
                    PositionTypeStr = item.User.PositionType.GetDescription(),
                    IsCancellation = item.IsCancellation,
                    UserType = userType
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.UserId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }
         #region 加班

        /// <summary>
        /// 添加加班
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddOvertime(OvertimeInputDto input)
        {
            var data = await db.Overtimes.SingleOrDefaultAsync(m => m.UserId == input.UserId && m.OverTimeDate == input.OverTimeDate && !m.IsDelete);

            if (data != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "【" + data.OverTimeDate + "】已提交加班申请" }))
                });
            }

            db.Overtimes.Add(new Overtime()
            {
                Id = IdentityManager.NewId(),
                ApprovalType = input.ApprovalType,
                Duration = input.Duration,
                IsDelete = false,
                UserId = input.UserId,
                OverTimeDate = input.OverTimeDate
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新加班
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateOvertime(OvertimeInputDto input)
        {
            var data = await db.Overtimes.SingleOrDefaultAsync(m => m.Id == input.OvertimeId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "加班数据不存在" }))
                });
            }

            data.UserId = input.UserId;
            data.ApprovalType = input.ApprovalType;
            data.DepartmentLeaderId = input.DepartmentLeaderId;
            data.Duration = input.Duration;
            data.OverTimeDate = input.OverTimeDate;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }

        }

        /// <summary>
        /// 批量删除加班
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteOvertime(List<long> Ids)
        {
            var list = new List<string>();
            foreach (var item in Ids)
            {
                var data = await db.Overtimes.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add("【" + item + "】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取加班列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<OvertimeOutputDto>> GetOvertimeList(GetOvertimeInputDto input)
        {
            var data = await db.Overtimes
                    .Include(m => m.User)
                    .Include(m => m.DepartmentLeader)
                    .Where(m => !m.IsDelete)
                    .ToListAsync();

            if (!string.IsNullOrWhiteSpace(input.DepartmentLeaderName))
            {
                data = data.Where(m => m.DepartmentLeader.RealName != null && m.DepartmentLeader.RealName.Contains(input.DepartmentLeaderName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.DepartmentName))
            {
                data = data.Where(m => m.DepartmentLeader.User.Department.Name != null && m.DepartmentLeader.User.Department.Name.Contains(input.DepartmentLeaderName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                data = data.Where(m => m.User.RealName != null && m.User.RealName.Contains(input.UserName)).ToList();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.User.User.DepartmentId == input.DepartmentId).ToList();
            }

            if (input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            var list = new List<OvertimeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new OvertimeOutputDto()
                {
                    OvertimeId = item.Id.ToString(),
                    ApprovalType = item.ApprovalType,
                    ApprovalTypeStr = item.ApprovalType.GetDescription(),
                    DepartmentLeaderId = item.DepartmentLeaderId.ToString(),
                    CreateDate = item.CreateDate,
                    DepartmentLeader = item.DepartmentLeader,
                    Duration = item.Duration,
                    UpdateDate = item.UpdateDate,
                    User = item.User,
                    UserId = item.UserId.ToString()
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.OvertimeId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

        #endregion

        [AppAuthentication(Cancel =true)]
        [HttpPost]
        public async Task<List<User>> Get()
        {
            var data = await db.User.ToListAsync();


            return data;
        }
    
        /// <summary>
        /// 添加用户对应的类型
        /// </summary>
        /// <param name="userTypelisstDto"></param>
        /// <returns></returns>
        [HttpPost]
         public async Task<IHttpActionResult> AddUserTypes(UserTypelisstDto userTypelisstDto)
        {
            try
            {
                if (userTypelisstDto.user_Typeid != null)
                {
                    //查看是这个是否已有这个人已经有这个身份

                    var res =db.User_User_Types.Where(p => p.u_Id == userTypelisstDto.id);
                    if (res.Count() > 0)
                    {
                        List<long> vs = new List<long>();

                        foreach (var itemt in res)
                        {
                            vs.Add(itemt.User_Type_ID);
                        }
                      
                      foreach (var item in userTypelisstDto.user_Typeid)
                        {
                            var itemd = vs.Contains(item);
                            if (itemd)
                            {
                                continue;
                            }  
                            else
                            { 

                                    User_User_Type user_User_Type = new User_User_Type()
                                    {
                                        Id = IdentityManager.NewId(),
                                        u_Id = userTypelisstDto.id,
                                        User_Type_ID = item,
                                        Type_id = 2

                                    };
                                    db.User_User_Types.Add(user_User_Type);

                                }
                             
                              } 

                            
                            
                            await db.SaveChangesAsync();
                            return Json(new { code = 200 });
                    

            
 

                    }
                    if (await res.CountAsync() <= 0)
                    {
                        foreach (var item in userTypelisstDto.user_Typeid)
                        { 
                                    User_User_Type user_User_Type = new User_User_Type()
                                    {
                                        Id= IdentityManager.NewId(),
                                        u_Id = userTypelisstDto.id,
                                        User_Type_ID = item,
                                        Type_id = 2
                                    };
                                    db.User_User_Types.Add(user_User_Type);    
                                
                         
                        } 
                        await db.SaveChangesAsync();  
                        return Json(new { code = 200 });

                    }
                    return Json(new { code = 400 });

                }
                return Json(new { code = 400 });


            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除用户类型
        /// </summary>
        /// <param name="userTypelisstDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUserType(UserTypelisstDto userTypelisstDto)
        {

            try
            {
                if (userTypelisstDto !=null)
                {


                    var result =await Task.Run(()=>( db.User_User_Types.Where(s => s.u_Id == userTypelisstDto.id && s.Type_id == 2)));
            if (result.Count()>0)
                       
            {
                        
                        foreach (var items in userTypelisstDto.Del_ID)
                        {
                            var res = await Task.Run(() => (db.User_User_Types.Where(p=>p.Id==items).FirstAsync()));
                            db.User_User_Types.Remove(res);
                        }

                        await db.SaveChangesAsync();

                return Json(new { code = 200 });

            }


            return Json(new { code = 200,msg="没有这个人" });
        
            }
                return Json(new { code = 400 });
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        /// <summary>
        /// 根据个人 获取此用户身份类型
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetUserType(long user_id)
        {
            try
            {
                if (user_id !=null)
                {
                   var result =await Task.Run(()=>from p in db.User_User_Types.Where(p => p.u_Id == user_id) 
                                                  
                                                  select new user_userTypeDTO{
                                                      Id=(p.Id).ToString() ,
                                                      User_Type_ID= (p.User_Type_ID).ToString(),
                                                      u_Id= (p.u_Id).ToString(),
                                                      Type_id= p.Type_id,
                                                  });
                 
                   
                if (await result.CountAsync()>0)
                {
                    return Json(new { code = 200, data = result });
                }
             return Json(new { code = 200, msg = "没有数据" });
                }
                return Json(new { code = 400, msg = "没有参数" });
            }
            catch (Exception)
            {

                throw;
            }
          
            

        }





        /// <summary>
        /// 获取用户列表(销售部门)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserListOuputDto>> GetUserListst(GetUserListOuputDto input)
        {

            var userId = ((UserIdentity)User.Identity).UserId;

            var result = await Task.Run(() => db.UserDetails.AsNoTracking().SingleOrDefaultAsync(p => p.Id == userId));

            var departmentid = result.User.DepartmentId;
            //   result.User.DepartmentId;
            var res = await Task.Run(() => db.UserDetails
            .Where(p => p.User.DepartmentId == departmentid)
            .Select(p => new GetUserListOuputDto
            {
                UserId = p.Id.ToString(),
                UserName=p.RealName,
                Department=p.User.Department
            })) ;


            if (!string.IsNullOrWhiteSpace(input.RealName))
            {
                res = await Task.Run(() => res.Where(p => p.RealName.Contains(input.RealName))
                .OrderBy(p => p.RealName)
                 .Skip((input.PageIndex * input.PageSize) - input.PageSize).Take(input.PageSize));
                return await res.ToListAsync();
            }

            return  await res.ToListAsync();
        }



    }
}
