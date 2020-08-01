using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.UserManage;
using XiAnOuDeERP.Models.Dto.Position;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.UserDepartMent
{

  //  [ExperAuthentication]
    [AppAuthentication]
    [RoutePrefix("api/Position")]
    public class PositionController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加职位
        /// </summary>
        /// <param name="postionInDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> AddPostion(PostionInDto postionInDto)
        {
            Position_User position_User = new Position_User
            {
                Id = IdentityManager.NewId(),
                PositionName = postionInDto.PositionName,
                PositionDes = postionInDto.PositionDes,
                Order = postionInDto.Order

            };
            db.Position_Users.Add(position_User);
            if (await db.SaveChangesAsync()>0)
            {
                return Json(new { code = 200, msg = "添加成功" });
            }
            return Json(new { code = 200, msg = "添加失败" });
        }

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="postionInDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> EditPostion(PostionInDto postionInDto)
        {
            Position_User type_res = new Position_User() { Id = postionInDto.Id };
             type_res.PositionName = postionInDto.PositionName;
             type_res.PositionDes =postionInDto.PositionDes;
            type_res.Order = postionInDto.Order;

            db.Configuration.ValidateOnSaveEnabled = false;
           db.Entry(type_res).State = System.Data.Entity.EntityState.Unchanged;  
           

           // Type type = typeof(Position_User);

          //  Assembly assembly = Assembly.GetExecutingAssembly();
           // object o = assembly.CreateInstance(type.FullName);
          //  PropertyInfo[] propertyInfos = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //string str = string.Empty;
            // str += string.Format("类名称：{0}", type.Name);
            foreach (var item in type_res.GetType().GetProperties())
            {
                string name = item.Name;
                object value = item.GetValue(type_res);
                if (item.GetValue(type_res) != null&&value.ToString()!="" && item.Name != "Id")
                {
                    Console.WriteLine(item.Name);
                    db.Entry(type_res).Property(item.Name).IsModified = true;
                }
                else if(item.Name != "Id")
                {
                    Console.WriteLine(item.Name);
                    db.Entry(type_res).Property(item.Name).IsModified = false;
                }
            }
            if (await db.SaveChangesAsync() > 0)
            {
                db.Configuration.ValidateOnSaveEnabled = true;
                return Json(new { code = 200, msg = "修改成功" });
            }

            return Json(new { code = 301, msg = "修改失败" });
        }


        /// <summary>
        /// 获取职位
        /// </summary>
        /// <param name="positionOutDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<PositionOutDto>> GetPostion(PositionOutDto positionOutDto)
        {

        

         var res = await Task.Run(() => db.Position_Users.AsNoTracking()
             .Where(p => p.Id > 0&&p.del_Or==false)
            .Select(p=>new PositionOutDto
            {
                Id = p.Id.ToString(),
                PositionName = p.PositionName,
                PositionDes = p.PositionDes,
                Order = p.Order
               // Count=
            }  
                ));

         if (positionOutDto==null)
         {
               return await res.ToListAsync();
         }
                if ( positionOutDto.PositionName != null)
            {
                res = await Task.Run(() => res.Where(p=> p.PositionName.Contains(positionOutDto.PositionName))
                .OrderBy(p => p.Id)
                 .Skip((positionOutDto.PageIndex * positionOutDto.PageSize) - positionOutDto.PageSize).Take(positionOutDto.PageSize));


                var re = await res.ToListAsync();
                foreach (var item in re)
                {
                    item.Counts = res.Count();
                }

                return re;
            }

            if (positionOutDto.PageIndex!=null&&positionOutDto.PageSize!=null)
            {
               res = await Task.Run(() => res.OrderBy(p=>p.Id)
               .Skip((positionOutDto.PageIndex * positionOutDto.PageSize) - positionOutDto.PageSize).Take(positionOutDto.PageSize));
                var re = await res.ToListAsync();
                foreach (var item in  re)
                {
                    item.Counts = res.Count();
                }

                return re;
            }

            return await res.ToListAsync();

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postionInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemovePostion(PostionInDto postionInDto)
        {
            foreach (var item in postionInDto.Del_Id)
            {
               var type_res = new Position_User() { Id = item };
                db.Entry(type_res).State = System.Data.Entity.EntityState.Unchanged;
                type_res.del_Or = true;
            }
        
          
            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "修改成功" });
            }

            return Json(new { code = 301, msg = "修改失败" });
        }
    }
}
