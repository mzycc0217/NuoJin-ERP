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
    [RoutePrefix("api/FinshedProductType")]
    public class FinshedProductTypeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 成品半成品类型
        /// </summary>
        /// <param name="finshedProductTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRowType(FinshedProductTypeDto finshedProductTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(finshedProductTypeDto.Name) && !string.IsNullOrWhiteSpace(finshedProductTypeDto.Dec))
                {

                    Z_FinshedProductType z_FinshedProductType = new Z_FinshedProductType
                    {
                        Id = IdentityManager.NewId(),
                        Name = finshedProductTypeDto.Name,
                        Dec = finshedProductTypeDto.Dec
                    };
                    db.Z_FinshedProductType.Add(z_FinshedProductType);
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
        /// 删除成品半成品类型
        /// </summary>
        /// <param name="finshedProductTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRowType(FinshedProductTypeDto finshedProductTypeDto)
        {
            try
            {
                if (finshedProductTypeDto.del_Id !=null)
                {
                    foreach (var item in finshedProductTypeDto.del_Id)
                    {
                      var result = db.Z_FinshedProductType.AsNoTracking().First(m => m.Id == item);
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
        /// 修改成品半成品类型
        /// </summary>
        /// <param name="finshedProductTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRowType(FinshedProductTypeDto finshedProductTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(finshedProductTypeDto.Name) && !string.IsNullOrWhiteSpace(finshedProductTypeDto.Dec) && !string.IsNullOrWhiteSpace(finshedProductTypeDto.Id))
                {
                    var res = long.Parse(finshedProductTypeDto.Id);
                    var result = await Task.Run(() => db.Z_FinshedProductType.Where(m => m.Id == res));
                    //  db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                    foreach (var item in result)
                    {
                        item.Name = finshedProductTypeDto.Name;
                        item.Dec = finshedProductTypeDto.Dec;
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
        /// 获取成品半成品数据
        /// </summary>
        /// <param name="finshedProductTypeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRowType(FinshedProductTypeOutDto finshedProductTypeOutDto)
        {
            /*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
            try
            {
                if (finshedProductTypeOutDto.PageIndex != null && finshedProductTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(finshedProductTypeOutDto.Name))
                {
                    var result = await Task.Run(() => (from p in db.Z_FinshedProductType.Where(p => p.Name.Contains(finshedProductTypeOutDto.Name))
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                     .Skip((finshedProductTypeOutDto.PageIndex * finshedProductTypeOutDto.PageSize) - finshedProductTypeOutDto.PageSize).Take(finshedProductTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (finshedProductTypeOutDto.PageIndex == -1 && finshedProductTypeOutDto.PageSize == -1)
                {
                    var result = await Task.Run(() => (from p in db.Z_FinshedProductType.Where(p => true)
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                                                       );



                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (finshedProductTypeOutDto.PageIndex != null && finshedProductTypeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (from p in db.Z_FinshedProductType.Where(p => true)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })
                    .Skip((finshedProductTypeOutDto.PageIndex * finshedProductTypeOutDto.PageSize) - finshedProductTypeOutDto.PageSize).Take(finshedProductTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (finshedProductTypeOutDto.PageIndex != null && finshedProductTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(finshedProductTypeOutDto.Id))
                {
                    var result = await Task.Run(() => (from p in db.Z_FinshedProductType.Where(p => p.Id == long.Parse(finshedProductTypeOutDto.Id))
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
