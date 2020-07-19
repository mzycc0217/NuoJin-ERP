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
    [RoutePrefix("api/ChemistryRoom")]
    public class ChemistryRoomController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 化学用品入库
        /// </summary>
        /// <param name="chemistryIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddChemistryRoom(ChemistryIntDto chemistryIntDto)
        {
            try
            {
                ChemistryRoom chemistryRoom = new ChemistryRoom
                {
                    Id = IdentityManager.NewId(),
                    ChemistryId = chemistryIntDto.ChemistryId,
                    User_id = chemistryIntDto.User_id,
                    EntrepotId = chemistryIntDto.EntrepotId,
                    RawNumber = chemistryIntDto.RawNumber,
                    RawOutNumber = chemistryIntDto.RawOutNumber,
                    Warning_RawNumber = chemistryIntDto.Warning_RawNumber,
                    RoomDes = chemistryIntDto.RoomDes,

                };
                db.ChemistryRooms.Add(chemistryRoom);
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
        /// <param name="chemistryIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditChemistryRoom(ChemistryIntDto chemistryIntDto)
        {
            try
            {
                var ChemistryRoom = new ChemistryRoom { Id = chemistryIntDto.Id };
                db.Entry(ChemistryRoom).State = System.Data.Entity.EntityState.Unchanged;
                if (chemistryIntDto.ChemistryId != null)
                {
                    ChemistryRoom.ChemistryId = chemistryIntDto.ChemistryId;
                }
                if (chemistryIntDto.User_id != null)
                {
                    ChemistryRoom.User_id = chemistryIntDto.User_id;
                }
                if (chemistryIntDto.EntrepotId != null)
                {
                    chemistryIntDto.EntrepotId = chemistryIntDto.EntrepotId;
                }
                if (chemistryIntDto.RawNumber != null)
                {
                    ChemistryRoom.RawNumber = chemistryIntDto.RawNumber;
                }
                if (chemistryIntDto.RawOutNumber != null)
                {
                    ChemistryRoom.RawOutNumber = chemistryIntDto.RawOutNumber;
                }
                if (chemistryIntDto.Warning_RawNumber != null)
                {
                    ChemistryRoom.Warning_RawNumber = chemistryIntDto.Warning_RawNumber;
                }

                if (!string.IsNullOrWhiteSpace(chemistryIntDto.RoomDes))
                {
                    ChemistryRoom.RoomDes = chemistryIntDto.RoomDes;
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
        /// <param name="chemistryIntDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveChemistryRoom(ChemistryIntDto chemistryIntDto)
        {
            try
            {
                if (chemistryIntDto.del_Id != null)
                {
                    foreach (var item in chemistryIntDto.del_Id)
                    {
                        var result = new ChemistryRoom { Id = item };
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
        /// 获取库存中的化学用品(库存信息)
        /// </summary>
        /// <param name="chemistryOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistryRoom(ChemistryOutDto chemistryOutDto)
        {
            try
            {
                // var result = db.RawRooms.Where(p => p.Id > 0);
                if (chemistryOutDto.PageIndex != null && chemistryOutDto.PageSize != null)
                {

                    var result = await Task.Run(() => db.ChemistryRooms.Where
                    (p => p.Id > 0 ||p.Z_Chemistry.Name.Contains(chemistryOutDto.Name) || p.userDetails.RealName.Contains(chemistryOutDto.relName))
                    .Select(p => new ChemistryOutDto
                    {
                        Id = (p.Id).ToString(),
                        ChemistryId = (p.Z_Chemistry.Id).ToString(),
                        User_id = (p.userDetails.Id).ToString(),
                        EntrepotId = (p.entrepot.Id).ToString(),
                        Z_Chemistry = p.Z_Chemistry,
                        entrepot = p.entrepot,
                        userDetails = p.userDetails

                    }).OrderBy(p => p.Id).Skip((chemistryOutDto.PageIndex * chemistryOutDto.PageSize) - chemistryOutDto.PageSize).Take(chemistryOutDto.PageSize).ToList());

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
