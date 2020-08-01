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
    /// 操作产成品入库
    /// </summary>
    [AppAuthentication]
    [RoutePrefix("api/FnishedProductRoom")]
    public class FnishedProductRoomController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 产成品入库
        /// </summary>
        /// <param name="fnishedProductRoomInt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddFnishedProductRoom(FnishedProductRoomIntDto fnishedProductRoomInt)
        {
            try
            {
                FnishedProductRoom fnishedProductRoom = new FnishedProductRoom
                {
                    Id = IdentityManager.NewId(),
                    FnishedProductId = fnishedProductRoomInt.FnishedProductId,
                    User_id = fnishedProductRoomInt.User_id,
                    EntrepotId = fnishedProductRoomInt.EntrepotId,
                    RawNumber = fnishedProductRoomInt.RawNumber==null?0: fnishedProductRoomInt.RawNumber,
                    RawOutNumber = fnishedProductRoomInt.RawOutNumber == null ? 0 : fnishedProductRoomInt.RawNumber,
                    Warning_RawNumber = fnishedProductRoomInt.Warning_RawNumber == null ? 0 : fnishedProductRoomInt.RawNumber,
                    RoomDes = fnishedProductRoomInt.RoomDes,

                };
                db.FnishedProductRooms.Add(fnishedProductRoom);
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
        /// 修改产成品入库
        /// </summary>
        /// <param name="fnishedProductRoomInt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditFnishedProductRoom(FnishedProductRoomIntDto fnishedProductRoomInt)
        {
            try
            {
                var FnishedProductRoom = new FnishedProductRoom { Id = fnishedProductRoomInt.Id };
                db.Entry(FnishedProductRoom).State = System.Data.Entity.EntityState.Unchanged;
                if (fnishedProductRoomInt.FnishedProductId != null)
                {
                    FnishedProductRoom.FnishedProductId = fnishedProductRoomInt.FnishedProductId;
                }
                if (fnishedProductRoomInt.User_id != null)
                {
                    FnishedProductRoom.User_id = fnishedProductRoomInt.User_id;
                }
                if (fnishedProductRoomInt.EntrepotId != null)
                {
                    FnishedProductRoom.EntrepotId = fnishedProductRoomInt.EntrepotId;
                }
                if (fnishedProductRoomInt.RawNumber != null)
                {
                    FnishedProductRoom.RawNumber = fnishedProductRoomInt.RawNumber;
                }
                if (fnishedProductRoomInt.RawOutNumber != null)
                {
                    FnishedProductRoom.RawOutNumber = fnishedProductRoomInt.RawOutNumber;
                }
                if (fnishedProductRoomInt.Warning_RawNumber != null)
                {
                    FnishedProductRoom.Warning_RawNumber = fnishedProductRoomInt.Warning_RawNumber;
                }

                if (!string.IsNullOrWhiteSpace(fnishedProductRoomInt.RoomDes))
                {
                    FnishedProductRoom.RoomDes = fnishedProductRoomInt.RoomDes;
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
        /// 删除库存中的产成品
        /// </summary>
        /// <param name="fnishedProductRoomInt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveFnishedProductRoom(FnishedProductRoomIntDto fnishedProductRoomInt)
        {
            try
            {
                if (fnishedProductRoomInt.del_Id != null)
                {
                    foreach (var item in fnishedProductRoomInt.del_Id)
                    {
                        var result = new FnishedProductRoom { Id = item };
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
        /// 获取库存中的产成品(1)成品（2）产成品
        /// </summary>
        /// <param name="fnishedProductRoomOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRawFnishedProductRoom(FnishedProductRoomOutDto fnishedProductRoomOutDto)
        {
            try
            {

                if (fnishedProductRoomOutDto.PageIndex == -1 && fnishedProductRoomOutDto.PageSize == -1&&!string.IsNullOrWhiteSpace(fnishedProductRoomOutDto.Finshed_Sign))
                {

                    var result = await Task.Run(() => db.FnishedProductRooms.Where
                    (p => p.Id > 0&&p.Z_FnishedProduct.Finshed_Sign.ToString()== fnishedProductRoomOutDto.Finshed_Sign)
                    .Select(p => new FnishedProductRoomOutDto
                    {
                        Id = (p.Id).ToString(),
                        FnishedProductId = (p.Z_FnishedProduct.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_FnishedProduct = p.Z_FnishedProduct,
                        entrepot = p.entrepot,
                        Finshed_Sign = p.Z_FnishedProduct.Finshed_Sign.ToString(),
                        userDetails = p.userDetails

                    }).ToList());

                    return Json(new { code = 200, data = result, Count = result.Count() });

                }
                if (fnishedProductRoomOutDto.PageIndex == -1 && fnishedProductRoomOutDto.PageSize == -1)
                {

                    var result = await Task.Run(() => db.FnishedProductRooms.Where
                    (p => p.Id > 0 )
                    .Select(p => new FnishedProductRoomOutDto
                    {
                        Id = (p.Id).ToString(),
                        FnishedProductId = (p.Z_FnishedProduct.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_FnishedProduct = p.Z_FnishedProduct,
                        Finshed_Sign=p.Z_FnishedProduct.Finshed_Sign.ToString(),
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).ToList());

                    return Json(new { code = 200, data = result, Count = result.Count() });

                }


                if (fnishedProductRoomOutDto.Name != null && fnishedProductRoomOutDto.relName != null)
                {

                    var result = await Task.Run(() => db.FnishedProductRooms.Where
                    (p=> p.Z_FnishedProduct.Name.Contains(fnishedProductRoomOutDto.Name) &&p.userDetails.RealName.Contains(fnishedProductRoomOutDto.relName))
                    .Select(p => new FnishedProductRoomOutDto
                    {
                        Id = (p.Id).ToString(),
                        FnishedProductId = (p.Z_FnishedProduct.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_FnishedProduct = p.Z_FnishedProduct,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).OrderBy(p => p.Id).Skip((fnishedProductRoomOutDto.PageIndex * fnishedProductRoomOutDto.PageSize) - fnishedProductRoomOutDto.PageSize).Take(fnishedProductRoomOutDto.PageSize).ToList());

                    return Json(new { code = 200, data = result, Count = result.Count() });

                }

                // var result = db.RawRooms.Where(p => p.Id > 0);
                if (fnishedProductRoomOutDto.PageIndex != null && fnishedProductRoomOutDto.PageSize != null)
                {

                    var result = await Task.Run(() => db.FnishedProductRooms.Where
                    (p => p.Id > 0 && p.Z_FnishedProduct.Name.Contains(fnishedProductRoomOutDto.Name) || p.userDetails.RealName.Contains(fnishedProductRoomOutDto.relName))
                    .Select(p => new FnishedProductRoomOutDto
                    {
                        Id = (p.Id).ToString(),
                        FnishedProductId = (p.Z_FnishedProduct.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_FnishedProduct = p.Z_FnishedProduct,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).OrderBy(p => p.Id).Skip((fnishedProductRoomOutDto.PageIndex * fnishedProductRoomOutDto.PageSize) - fnishedProductRoomOutDto.PageSize).Take(fnishedProductRoomOutDto.PageSize).ToList());

                    return Json(new { code = 200, data = result, Count = result.Count() });

                }

                return Json(new { code = 400, msg = "获取失败" });



            }
            catch (Exception)
            {

                throw;
            }


        }


    }
}
