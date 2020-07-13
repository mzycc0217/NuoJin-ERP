using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.IntoPut;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.DataBaser.DataBaseType
{
    [AppAuthentication]
    [RoutePrefix("api/ChemistryType")]
    public class ChemistryTypeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        ///化学类型
        /// </summary>
        /// <param name="chemistryTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRowType(ChemistryTypeDto chemistryTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(chemistryTypeDto.Name) && !string.IsNullOrWhiteSpace(chemistryTypeDto.Dec))
                {

                    Z_ChemistryType z_ChemistryType = new Z_ChemistryType
                    {
                        Id = IdentityManager.NewId(),
                        Name = chemistryTypeDto.Name,
                        Dec = chemistryTypeDto.Dec
                    };
                    db.Z_ChemistryType.Add(z_ChemistryType);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { code = 400, msg = "添加失败" });
                    }

                }
                else
                {
                    return Json(new { code = 201, msg = "请勿添加空数据" });
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除化学类型
        /// </summary>
        /// <param name="chemistryTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRowType(ChemistryTypeDto chemistryTypeDto)
        {
            try
            {
                if (chemistryTypeDto.del_Id !=null)
                {
                    foreach (var item in chemistryTypeDto.del_Id)
                    {
                     var result = db.Z_ChemistryType.AsNoTracking().First(m => m.Id == item);
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
        /// 修改化学类型
        /// </summary>
        /// <param name="chemistryTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRowType(ChemistryTypeDto chemistryTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(chemistryTypeDto.Name) && !string.IsNullOrWhiteSpace(chemistryTypeDto.Dec) && !string.IsNullOrWhiteSpace(chemistryTypeDto.Id))
                {
                    var res = long.Parse(chemistryTypeDto.Id);
                    var result = await Task.Run(() => db.Z_ChemistryType.Where(m => m.Id == res));
                    //  db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                    foreach (var item in result)
                    {
                        item.Name = chemistryTypeDto.Name;
                        item.Dec = chemistryTypeDto.Dec;
                    }


                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "修改成功" });
                    }
                    else
                    {
                        return Json(new { code = 400, msg = "修改失败" });
                    }

                }
                else
                {
                    return Json(new { code = 201, msg = "请勿修改空数据" });
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取化学数据
        /// </summary>
        /// <param name="chemistryTypeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRowType(ChemistryTypeOutDto chemistryTypeOutDto)
        {
            /*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
            try
            {
                if (chemistryTypeOutDto.PageIndex != null && chemistryTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(chemistryTypeOutDto.Name))
                {
                    var result = await Task.Run(() => (from p in db.Z_ChemistryType.AsNoTracking().Where(p => p.Name.Contains(chemistryTypeOutDto.Name))
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                     .Skip((chemistryTypeOutDto.PageIndex * chemistryTypeOutDto.PageSize) - chemistryTypeOutDto.PageSize).Take(chemistryTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (chemistryTypeOutDto.PageIndex == -1 && chemistryTypeOutDto.PageSize == -1)
                {
                    var result = await Task.Run(() => (from p in db.Z_ChemistryType.AsNoTracking().Where(p => true)
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                                                       );



                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (chemistryTypeOutDto.PageIndex != null && chemistryTypeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (from p in db.Z_ChemistryType.AsNoTracking().Where(p => true)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })
                    .Skip((chemistryTypeOutDto.PageIndex * chemistryTypeOutDto.PageSize) - chemistryTypeOutDto.PageSize).Take(chemistryTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (chemistryTypeOutDto.PageIndex != null && chemistryTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(chemistryTypeOutDto.Id))
                {
                    var result = await Task.Run(() => (from p in db.Z_ChemistryType.AsNoTracking().Where(p => p.Id == long.Parse(chemistryTypeOutDto.Id))
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       }));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                return Json(new { code = 400, msg = "添加失败" });
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
