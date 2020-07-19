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
    /// 操作化学用品入库
    /// </summary>
    [AppAuthentication]
    [RoutePrefix("api/OfficeRoom")]
    public class OfficeRoomController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 化学用品入库
        /// </summary>
        /// <param name="officeIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddOfficeRoom(OfficeIntDto officeIntDto)
        {
            try
            {
                OfficeRoom officeRoom = new OfficeRoom
                {
                    Id = IdentityManager.NewId(),
                    OfficeId = officeIntDto.OfficeId,
                    User_id = officeIntDto.User_id,
                    EntrepotId = officeIntDto.EntrepotId,
                    RawNumber = officeIntDto.RawNumber,
                    RawOutNumber = officeIntDto.RawOutNumber,
                    Warning_RawNumber = officeIntDto.Warning_RawNumber,
                    RoomDes = officeIntDto.RoomDes,

                };
                db.Offices.Add(officeRoom);
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
        /// 修改化学用品入库
        /// </summary>
        /// <param name="officeIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditOfficeRoom(OfficeIntDto officeIntDto)
        {
            try
            {
                var OfficeRoom = new OfficeRoom { Id = officeIntDto.Id };
                db.Entry(OfficeRoom).State = System.Data.Entity.EntityState.Unchanged;
                if (officeIntDto.OfficeId != null)
                {
                    OfficeRoom.OfficeId = officeIntDto.OfficeId;
                }
                if (officeIntDto.User_id != null)
                {
                    OfficeRoom.User_id = officeIntDto.User_id;
                }
                if (officeIntDto.EntrepotId != null)
                {
                    OfficeRoom.EntrepotId = officeIntDto.EntrepotId;
                }
                if (officeIntDto.RawNumber != null)
                {
                    OfficeRoom.RawNumber = officeIntDto.RawNumber;
                }
                if (officeIntDto.RawOutNumber != null)
                {
                    OfficeRoom.RawOutNumber = officeIntDto.RawOutNumber;
                }
                if (officeIntDto.Warning_RawNumber != null)
                {
                    OfficeRoom.Warning_RawNumber = officeIntDto.Warning_RawNumber;
                }

                if (!string.IsNullOrWhiteSpace(officeIntDto.RoomDes))
                {
                    OfficeRoom.RoomDes = officeIntDto.RoomDes;
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
        /// 删除库存中的化学用品
        /// </summary>
        /// <param name="officeIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveOfficeRoom(OfficeIntDto officeIntDto)
        {
            try
            {
                if (officeIntDto.del_Id != null)
                {
                    foreach (var item in officeIntDto.del_Id)
                    {
                        var result = new OfficeRoom { Id = item };
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
        /// 获取库存中的化学用品
        /// </summary>
        /// <param name="officeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOfficeRoom(OfficeOutDto officeOutDto)
        {
            try
            {
                // var result = db.RawRooms.Where(p => p.Id > 0);
                if (officeOutDto.PageIndex != null && officeOutDto.PageSize != null)
                {

                    var result = await Task.Run(() => db.Offices.Where
                    (p => p.Id > 0 || p.Z_Office.Name.Contains(officeOutDto.Name) || p.userDetails.RealName.Contains(officeOutDto.relName))
                    .Select(p => new OfficeOutDto
                    {
                        Id = (p.Id).ToString(),
                        OfficeId = (p.Z_Office.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_Office = p.Z_Office,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).OrderBy(p => p.Id).Skip((officeOutDto.PageIndex * officeOutDto.PageSize) - officeOutDto.PageSize).Take(officeOutDto.PageSize).ToList());

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
