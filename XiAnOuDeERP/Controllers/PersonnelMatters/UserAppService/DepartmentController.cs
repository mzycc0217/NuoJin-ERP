using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.PersonnelMatters.Controllers.UserAppService
{
    /// <summary>
    /// 部门服务
    /// </summary>
    [AppAuthentication]
    public class DepartmentController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(DepartmentInputDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "部门名称不允许为空" }))
                });
            }

            var department = await db.Departments.SingleOrDefaultAsync(m => m.Name == input.Name && !m.IsDelete);

            if (department != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "部门已存在" }))
                });
            }

            var data = new Department()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                IsDelete = false,
                IsCancellation = false,
                Desc = input.Desc
            };

            db.Departments.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(DepartmentInputDto input)
        {
            var data = await db.Departments.SingleOrDefaultAsync(m => m.Id == input.DepartmentId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该部门不存在" }))
                });
            }

            data.Name = input.Name;
            data.Desc = input.Desc;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="DepartmentIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> DepartmentIds)
        {
            var list = new List<string>();

            foreach (var item in DepartmentIds)
            {
                var department = await db.Departments.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (department != null)
                {
                    department.IsDelete = true;
                }
                else
                {
                    list.Add("【"+item+"】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 批量注销部门
        /// </summary>
        /// <param name="DepartmentIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> CancellationList(List<long> DepartmentIds)
        {
            var list = new List<string>();

            foreach (var item in DepartmentIds)
            {
                var department = await db.Departments.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete && !m.IsCancellation);

                if (department != null)
                {
                    department.IsCancellation = true;
                }
                else
                {
                    list.Add("【" + item + "】不存在或已注销");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 取消部门注销
        /// </summary>
        /// <param name="DepartmentIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> CancelList(List<long> DepartmentIds)
        {
            var list = new List<string>();

            foreach (var item in DepartmentIds)
            {
                var department = await db.Departments.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete && m.IsCancellation);

                if (department != null)
                {
                    department.IsCancellation = false;
                }
                else
                {
                    list.Add("【" + item + "】不存在或状态正常");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<DepartmentOutputDto>> GetDepartmentList(GetDepartmentInputDto input)
        {
            var data = await db.Departments.Where(m=>!m.IsDelete).ToListAsync();

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.Id == input.DepartmentId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.DepartmentName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.DepartmentName)).ToList();
            }

            var list = new List<DepartmentOutputDto>();

            foreach (var item in data)
            {
                var userCount = await db.UserDetails.Include(m => m.User)
                    .CountAsync(m => m.User.DepartmentId == item.Id);

                var manager = await db.UserDetailsTypes
                    .Include(m=>m.User)
                    .Include(m=>m.User.User)
                    .Include(m=>m.UserType)
                    .Where(m => m.User.User.DepartmentId == item.Id && m.UserType.Name == "部门经理" && !m.User.IsDelete && m.User.User.PositionType != EPositionType.Quit).ToListAsync();

                var managers = new List<UserOutputDto>();
                var managerIds = new List<string>();
                foreach (var item1 in manager)
                {
                    managerIds.Add(item1.UserId.ToString());

                    managers.Add(new UserOutputDto
                    {
                        Address = item1.User.Address,
                        CreateDate = item1.CreateDate,
                        DateOfBirth = item1.User.DateOfBirth,
                        Education = item1.User.Education,
                        Email = item1.User.Email,
                        IdCard = item1.User.IdCard,
                        IsCancellation = item1.User.IsCancellation,
                        Nation = item1.User.Nation,
                        Phone = item1.User.Phone,
                        PortraitPath = item1.User.PortraitPath,
                        PositionType = item1.User.User.PositionType,
                        PositionTypeStr = item1.User.User.PositionType.GetDescription(),
                        DepartmentId = item1.User.User.DepartmentId.ToString(),
                        Department = item1.User.User.Department,
                        RealName = item1.User.RealName,
                        SexType = item1.User.SexType,
                        SexTypeStr = item1.User.SexType.GetDescription(),
                        UpdateDate = item1.UpdateDate,
                        UserId = item1.User.Id.ToString(),
                        UserName = item1.User.User.UserName,
                        //UserTypeId = manager.UserTypeId.ToString(),
                        //UserTypeStr = manager.UserType.Name,
                        WeiXin = item1.User.WeiXin
                    });
                }

                list.Add(new DepartmentOutputDto()
                {
                    DepartmentId = item.Id.ToString(),
                    Name = item.Name,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    UserCount = userCount,
                    IsCancellation = item.IsCancellation,
                    Desc = item.Desc,
                    ManagerId = managerIds,
                    Manager = managers

                    //ManagerId = manager == null ? "" : manager.Id.ToString(),
                    //Manager = manager == null ? null : new UserOutputDto { 
                    //Address = manager.User.Address,
                    //CreateDate = manager.CreateDate,
                    //DateOfBirth = manager.User.DateOfBirth,
                    //Education = manager.User.Education,
                    //Email = manager.User.Email,
                    //IdCard = manager.User.IdCard,
                    //IsCancellation = manager.User.IsCancellation,
                    //Nation = manager.User.Nation,
                    //Phone = manager.User.Phone,
                    //PortraitPath = manager.User.PortraitPath,
                    //PositionType = manager.User.User.PositionType,
                    //PositionTypeStr = manager.User.User.PositionType.GetDescription(),
                    //DepartmentId = manager.User.User.DepartmentId.ToString(),
                    //Department = manager.User.User.Department,
                    //RealName = manager.User.RealName,
                    //SexType = manager.User.SexType,
                    //SexTypeStr = manager.User.SexType.GetDescription(),
                    //UpdateDate = manager.UpdateDate,
                    //UserId = manager.Id.ToString(),
                    //UserName = manager.User.User.UserName,
                    ////UserTypeId = manager.UserTypeId.ToString(),
                    ////UserTypeStr = manager.UserType.Name,
                    //WeiXin = manager.User.WeiXin
                    //}
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.DepartmentId)
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
        /// 获取当前这个人所在部门的人
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        //[AppAuthentication]
        [HttpGet]
        public async Task<IHttpActionResult> GetDepartmentListUser()//long userId
        {
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                var rest = await Task.Run(()=> db.User.Where(p => p.Id == userId).FirstOrDefault());
                
                if (rest !=null)
                {
               var res = from s in db.User.Where(s => s.DepartmentId == rest.DepartmentId) select new { 
               s.UserName,
               s.Id
               };
                return Json(new { code = 200, data = res });
                }
                return Json(new { code = 200,msg="这个人部门没有签核的人，请联系管理员进行添加"});
          
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
            

        }


        //[HttpGet]
        //public async Task<IHttpActionResult> GetDepartmentListUsers()
        //{
        //    //查询是那个部门的，
        //    //这个部门下可以签单的人
        //    try
        //    {
        //        var userId = ((UserIdentity)User.Identity).UserId;
        //        var rest = await Task.Run(() => db.User.Where(p => p.Id == userId).FirstOrDefault());



        //        var result = from s in db.Departent_Users.Where(s => s.Departrement_ID == rest.DepartmentId)
        //                     select new {s.U_ID };

        //        foreach (var item in result)
        //        {
        //            var results = db.UserDetails.Where(t => t.UserId == item.U_ID);


        //        }

        //            return Json(new { code = 200, data = results });       
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
        /// <summary>
        /// 获取可以自己的部门可以签字的人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAuditPeople()
        {
            /*.Where(s=>s.UserId== userId) on p.Id equals s.UserId*/
            try
            {
                var userId = ((UserIdentity)User.Identity).UserId;
                var res = await Task.Run(()=>(from p in db.UserDetails.Where(p=>p.Id==userId)
                          join s in db.User on p.UserId equals s.Id
                          select new
                          {
                              s.DepartmentId
                          }));
                if (res==null)
                {
                   
                var s =   new Code
                    {
                        code = 200,
                        msg = "没有人员,请联系管理员"
                    };
                    return Json(s) ;
                }
                long? id=null;
                foreach (var item in res)
                {
                    id = item.DepartmentId;
                }
                //拿到userid
                var result = db.Departent_Users.Where(p => p.Department_Id == id).ToList();
                List<User_DTO> userDetails = new List<User_DTO>();

              
                foreach (var item in result)
                {
                    var resul = (from p in db.UserDetails.Where(p => p.Id == item.UserDetails_Id)
                                 select new User_DTO
                                 {
                                     UserId = (p.Id).ToString(),
                                     RealName = p.RealName
                                 }).ToList();
                    foreach (var itemt in resul)
                    {
                      userDetails.Add(new User_DTO{
                          UserId = itemt.UserId,
                          RealName = itemt.RealName

                      });
                    }
                      
                    
                  
                }
                //foreach (var item in result)
                //{

                //    var resul = await Task.Run(() => from p in db.UserDetails.Where(p => p.UserId == item.UserDetails_Id)
                //                                     select new
                //                                     {
                //                                         p.RealName,
                //                                         p.Id


                //                                     }) ;
                //    foreach (var itemt in resul)
                //    {
                //        userDetails.Add(new User_DTO
                //    {
                //        UserId = (itemt.Id).ToString(),
                //        RealName= itemt.RealName
                //    });
                //    }
                    

                //}

                return Json(userDetails) ;
            }
            catch (Exception)
            {

                throw;
            }











        }

      

    }
}
