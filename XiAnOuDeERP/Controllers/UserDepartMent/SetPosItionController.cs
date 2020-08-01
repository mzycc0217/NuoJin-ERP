using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.UserManage;
using XiAnOuDeERP.Models.Dto.UserManger.In;
using XiAnOuDeERP.Models.Dto.UserManger.Out;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.UserDepartMent
{
    /// <summary>
    /// 设置个人职位
    /// </summary>
    /// 

    [ExperAuthentication]
    [AppAuthentication]
   
    [RoutePrefix("api/Position")]
    public class SetPosItionController : ApiController
    {

        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 设置职位
        /// </summary>
        /// <param name="positionCorresIn"></param>
        /// <returns></returns>
       [HttpPost]
        public async Task<IHttpActionResult> SetPosition(PositionCorresIn positionCorresIn)
        {
            Position_Correspond position_Correspond = new Position_Correspond
            {
                Id = IdentityManager.NewId(),
                User_id = positionCorresIn.User_id,
                PositionId = positionCorresIn.PositionId,
                Sign = positionCorresIn.Sign

            };

            db.Position_Corresponds.Add(position_Correspond);
            if (await db.SaveChangesAsync()>0)
            {
                return Json(new { code = 200, msg = "添加成功" });
            }
            return Json(new { code = 200, msg = "添加失败" });
        }


        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="positionCorresIn"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditPosition(PositionCorresIn positionCorresIn)
        {

            Position_Correspond position_Correspond = new Position_Correspond {Id= positionCorresIn.id };
            db.Entry(position_Correspond).State = System.Data.Entity.EntityState.Unchanged;
            position_Correspond.PositionId = positionCorresIn.PositionId;
            position_Correspond.User_id = positionCorresIn.User_id;
            position_Correspond.Sign = positionCorresIn.Sign;

            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(position_Correspond).State = System.Data.Entity.EntityState.Unchanged;
            foreach (var item in position_Correspond.GetType().GetProperties())
            {
                string name = item.Name;
                object value = item.GetValue(position_Correspond);
                if (item.GetValue(position_Correspond) != null && value.ToString() != "" && item.Name != "Id")
                {
                    Console.WriteLine(item.Name);
                    db.Entry(position_Correspond).Property(item.Name).IsModified = true;
                }
                else if (item.Name != "Id")
                {
                    Console.WriteLine(item.Name);
                    db.Entry(position_Correspond).Property(item.Name).IsModified = false;
                }
            }

         
            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "添加成功" });
            }
            return Json(new { code = 200, msg = "添加失败" });
        }



        ///// <summary>
        ///// 获取职位
        ///// </summary>
        ///// <param name="positionCorresIn"></param>
        ///// <returns></returns>
        [HttpPost]
        public async Task<List< PositionCorresOut>> GetPosition(PositionCorresOut positionCorresOut)
        {
            var reult = await Task.Run(() => db.Position_Corresponds.Where(p => p.Id != null&&p.del_Or==false)
              .Select(p => new PositionCorresOut
              {
                  User_id = p.User_id.ToString(),
                  PositionId = p.PositionId.ToString(),
                  Sign = p.Sign,
                  UserDetails = p.UserDetails,
                  Position_User=p.Position_User,
                  
                  


              }));

            if (!string.IsNullOrWhiteSpace(positionCorresOut.relaname))
            {
                reult = await Task.Run(() => reult.Where(p => p.UserDetails.RealName.Contains(positionCorresOut.relaname)));
                return await reult.ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(positionCorresOut.relaname))
            {
                reult = await Task.Run(() => reult.Where(p => p.relaname!=null)
                .OrderBy(p => p.relaname)
                 .Skip((positionCorresOut.PageIndex * positionCorresOut.PageSize) - positionCorresOut.PageSize).Take(positionCorresOut.PageSize));
                return await reult.ToListAsync();
            }

            return await reult.ToListAsync();
        }


    }
}
