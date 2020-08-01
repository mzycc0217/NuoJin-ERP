using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Dto.EntrepotDto.EntreportOutDto;
using XiAnOuDeERP.Models.Dto.EntrepotDto.EntrepotInDto;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.OutputWarehouse
{
    /// <summary>
    /// 操作原材料入库
    /// </summary>
    [AppAuthentication]
    [RoutePrefix("api/RawRoom")]
    public class RawRoomController : ApiController
    {

        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 原材料入库
        /// </summary>
        /// <param name="rawInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRawRoom(RawInDto rawInDto)
        {
            try
            {
                RawRoom rawRoom = new RawRoom
                {
                    Id = IdentityManager.NewId(),
                    RawId = rawInDto.RawId,
                    User_id = rawInDto.User_id,
                    EntrepotId= rawInDto.EntrepotId,
                    RawNumber = rawInDto.RawNumber,
                    RawOutNumber = rawInDto.RawOutNumber,
                    Warning_RawNumber= rawInDto.Warning_RawNumber,
                    RoomDes= rawInDto.RoomDes,

                };
                db.RawRooms.Add(rawRoom);
                if (await db.SaveChangesAsync() > 0)
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
        /// 修改材料入库
        /// </summary>
        /// <param name="rawInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRawRoom(RawInDto rawInDto)
        {
            try
            {
                var RawRoom = new RawRoom { Id = rawInDto.Id };
                db.Entry(RawRoom).State = System.Data.Entity.EntityState.Unchanged;








                if (rawInDto.RawId != null)
                {
                    RawRoom.RawId = rawInDto.RawId;
                }
                if (rawInDto.User_id != null)
                {
                    RawRoom.User_id = rawInDto.User_id;
                }
                if (rawInDto.EntrepotId != null)
                {
                    RawRoom.EntrepotId = rawInDto.EntrepotId;
                }
                if (rawInDto.RawNumber != null)
                {
                    RawRoom.RawNumber = rawInDto.RawNumber;
                }
                if (rawInDto.RawOutNumber != null)
                {
                    RawRoom.RawOutNumber = rawInDto.RawOutNumber;
                }
                if (rawInDto.Warning_RawNumber != null)
                {
                    RawRoom.Warning_RawNumber = rawInDto.Warning_RawNumber;
                }

                if (!string.IsNullOrWhiteSpace(rawInDto.RoomDes))
                {
                    RawRoom.RoomDes = rawInDto.RoomDes;
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
        /// 删除库存中的原材料
        /// </summary>
        /// <param name="rawInDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRawRoom(RawInDto rawInDto)
        {
            try
            {
                if (rawInDto.del_Id != null)
                {
                    foreach (var item in rawInDto.del_Id)
                    {
                        var result = new RawRoom { Id = item };
                        //  var result = Task.Run(() => (db.Z_Office.AsNoTracking().FirstOrDefault(m => m.Id == item)));
                        db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
                    }
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "删除成功" });
                    }
                    else
                    {
                        return Json(new { code = 400, msg = "删除失败" });
                    }
                }
                else
                {
                    return Json(new { code = 201, msg = "请勿传递空数据" });
                }
            }
            catch (Exception)
            {

                throw;
            }


        }


        /// <summary>
        /// 获取库存中的原材料
        /// </summary>
        /// <param name="rawOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRawRoom(RawOutDto rawOutDto)
        {
            try
            {

                var resultc = await Task.Run(() => db.RawRooms.Where
                      (p => p.Id > 0));

                // var result = db.RawRooms.Where(p => p.Id > 0); 
                if ( rawOutDto.PageIndex == -1 && rawOutDto.PageSize == -1)
                {

                    var result = await Task.Run(() => db.RawRooms.Where
                    (p => p.Id>0)
                    .Select(p => new RawOutDto
                    {
                        Id = (p.Id).ToString(),
                        RawId = (p.z_Raw.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_Raw = p.z_Raw,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).ToList().Distinct());

                   



                    return Json(new { code = 200, data = result, Count = resultc.Count() });
                }

                // var result = db.RawRooms.Where(p => p.Id > 0); 
                if (rawOutDto.relName != null|| rawOutDto.Name != null&& rawOutDto.PageIndex != null && rawOutDto.PageSize != null)
                {

                    var result = await Task.Run(() => db.RawRooms.Where
                    (p=> p.z_Raw.Name.Contains(rawOutDto.Name) || p.userDetails.RealName.Contains(rawOutDto.relName))
                    .Select(p => new RawOutDto
                    {
                        Id = (p.Id).ToString(),
                        RawId = (p.z_Raw.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_Raw = p.z_Raw,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).OrderBy(p => p.Id).Skip((rawOutDto.PageIndex * rawOutDto.PageSize) - rawOutDto.PageSize).Take(rawOutDto.PageSize).ToList());

                    return Json(new { code = 200, data = result, Count = resultc.Count() });
                }

                if (rawOutDto.PageIndex != null && rawOutDto.PageSize != null)
                {

                    var result = await Task.Run(() => db.RawRooms.Where
                    (p => p.Id > 0)
                    .Select(p => new RawOutDto
                    {
                      Id= (p.Id).ToString(),
                      RawId =(p.z_Raw.Id).ToString(),
                      User_id=(p.userDetails.Id).ToString(),
                      EntrepotId=(p.entrepot.Id).ToString(),
                      Z_Raw=p.z_Raw,
                      entrepot=p.entrepot,
                      userDetails=p.userDetails

                    }).OrderBy(p => p.Id).Skip((rawOutDto.PageIndex * rawOutDto.PageSize) - rawOutDto.PageSize).Take(rawOutDto.PageSize).ToList());

                    return Json(new { code = 200, data = result, Count = resultc.Count() });

                }
              
                return Json(new { code = 400,msg="获取失败" });



            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
