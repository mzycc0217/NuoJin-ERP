using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Dto.EntrepotDto.EntreportOutDto;
using XiAnOuDeERP.Models.Dto.EntrepotDto.EntrepotInDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.AddEntrePot
{
    /// <summary>
    /// 操作仓库
    /// </summary>
    [AppAuthentication]
    [RoutePrefix("api/EntrePose")]
    public class EntrePoseController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="entrpotInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddEntrePose(EntrpotInDto entrpotInDto)
        {
            try
            {
                Entrepot entrepot = new Entrepot
                {
                    Id = IdentityManager.NewId(),
                    User_id= entrpotInDto.User_id,
                    EntrepotName = entrpotInDto.EntrepotName,
                    EntrepotDes = entrpotInDto.EntrepotDes,
                    EntrepotAddress = entrpotInDto.EntrepotAddress

                };
                db.Entrepots.Add(entrepot);
                if (await db.SaveChangesAsync()>0)
                {
                    return Json(new { code = 200, msg = "添加成功" });
                }
               
                    return Json(new { code = 201, msg = "添加失败" });
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entrpotInDto"></param>
        /// <returns></returns>
         [HttpPost]
        public async Task<IHttpActionResult> EditEntrePose(EntrpotInDto entrpotInDto)
        {
            try
            {
                var Entrepot = new Entrepot { Id = entrpotInDto.Id };
                db.Entry(Entrepot).State = System.Data.Entity.EntityState.Unchanged;
                if (!string.IsNullOrWhiteSpace(entrpotInDto.EntrepotName))
                {
                    Entrepot.EntrepotName = entrpotInDto.EntrepotName;
                }
                if (entrpotInDto.User_id!=null)
                {
                    Entrepot.User_id = entrpotInDto.User_id;
                }
                if (!string.IsNullOrWhiteSpace(entrpotInDto.EntrepotDes))
                {
                    Entrepot.EntrepotDes = entrpotInDto.EntrepotDes;
                }
                if (!string.IsNullOrWhiteSpace(entrpotInDto.EntrepotAddress))
                {
                    Entrepot.EntrepotAddress = entrpotInDto.EntrepotAddress;
                }
                if (await db.SaveChangesAsync() > 0)
                {
                    return Json(new { code = 200, msg = "修改成功" });
                }

                return Json(new { code = 201, msg = "修改失败" });

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entrpotInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveEntrePose(EntrpotInDto entrpotInDto)
        {
            try
            {
                foreach (var item in entrpotInDto.del_Id)
                {   
                var Entrepot = new Entrepot { Id = item };
                db.Entry(Entrepot).State = System.Data.Entity.EntityState.Unchanged;
                Entrepot.del_Enpto = 1;

                }
             
                if (await db.SaveChangesAsync() > 0)
                {
                    return Json(new { code = 200, msg = "删除成功" });
                }
                return Json(new { code = 201, msg = "删除失败" });

            }
            catch (Exception)
            {

                throw;
            }



        }
        /// <summary>
        /// 获取仓库
        /// </summary>
        /// <param name="entrePotOut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetEntrePose(EntrePotOut entrePotOut)
        {
            var result = await Task.Run(() => db.Entrepots.Where(p => p.del_Enpto == 0));
           

            if (entrePotOut.PageIndex == -1 && entrePotOut.PageSize == -1)
            {
                var reus = await Task.Run(() => db.Entrepots.Where(p => p.del_Enpto == 0).Select(p => new EntrePotOut
                {
                    Id = (p.Id).ToString(),
                    EntrepotName = p.EntrepotName,
                    EntrepotDes = p.EntrepotDes,
                    EntrepotAddress = p.EntrepotAddress,
                    User_id = (p.userDetails.Id).ToString(),
                    userDetails = p.userDetails

                }).ToList());

                return Json(new { code = 200, Count = result.CountAsync(), data = reus });


            }


           // var reus =await Task.Run(()=> db.Entrepots.Where(p => p.del_Enpto==0));
            if (entrePotOut.PageIndex!=null&&entrePotOut.PageSize!=null)
            {
            var  reus = await Task.Run(() => db.Entrepots.Where(p => p.del_Enpto == 0||p.EntrepotName.Contains(entrePotOut.EntrepotName)).Select(p => new EntrePotOut
                {
                    Id = (p.Id).ToString(),
                    EntrepotName=p.EntrepotName,
                    EntrepotDes=p.EntrepotDes,
                User_id = (p.userDetails.Id).ToString(),
                EntrepotAddress =p.EntrepotAddress,
                  userDetails = p.userDetails
            }) .OrderBy(p=>p.Id).Skip((entrePotOut.PageIndex * entrePotOut.PageSize) - entrePotOut.PageSize).Take(entrePotOut.PageSize).ToList()) ;

                return Json(new { code = 200, Count = result.CountAsync(), data = reus });
            }
            return Json(new { code = 400,msg="获取失败" });

        }



    }
}
