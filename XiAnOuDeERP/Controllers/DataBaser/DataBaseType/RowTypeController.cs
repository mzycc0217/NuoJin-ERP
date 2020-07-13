using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBaseType.OutoPut;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.DataBaser.DataBaseType
{
    [AppAuthentication]
    [RoutePrefix("api/RowType")]
    public class RowTypeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加原材料类型
        /// </summary>
        /// <param name="rowTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRowType(RowTypeDto rowTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(rowTypeDto.Name)&& !string.IsNullOrWhiteSpace(rowTypeDto.Dec))
                {
                
                Z_RowType z_RowType = new Z_RowType
                {
                    Id = IdentityManager.NewId(),
                    Name = rowTypeDto.Name,
                    Dec = rowTypeDto.Dec
                };
                db.Z_RowType.Add(z_RowType);
                    if (await db.SaveChangesAsync()>0)
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
        /// 删除原材料类型
        /// </summary>
        /// <param name="rowTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRowType(RowTypeDto rowTypeDto)
        {
            try
            {
                if (rowTypeDto.del_Id!=null)
                {
                    foreach (var item in rowTypeDto.del_Id)
                    {
                      var result = db.Z_RowType.AsNoTracking().FirstOrDefault(m=>m.Id== item);
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
        /// 修改原材料类型
        /// </summary>
        /// <param name="rowTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRowType(RowTypeDto rowTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(rowTypeDto.Name) && !string.IsNullOrWhiteSpace(rowTypeDto.Dec)&& !string.IsNullOrWhiteSpace(rowTypeDto.Id))
                {
                    var res = long.Parse(rowTypeDto.Id);
                    var result = await Task.Run(()=> db.Z_RowType.Where(m => m.Id == res));
                  //  db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                    foreach (var item in result)
                    {
                        item.Name = rowTypeDto.Name;
                        item.Dec = rowTypeDto.Dec;
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
        /// 获取原材料数据
        /// </summary>
        /// <param name="rowTypeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRowType(RowTypeOutDto rowTypeOutDto)
        {
            /*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
            try
            {
                if (rowTypeOutDto.PageIndex!=null && rowTypeOutDto.PageSize != null&& ! string.IsNullOrWhiteSpace(rowTypeOutDto.Name))
                {
                    var result = await Task.Run(() => (from p in db.Z_RowType.Where(p => p.Name.Contains(rowTypeOutDto.Name))
                                                       orderby p.CreateDate
                                                      select new RowTypeOutDto
                                                      {
                                                          Id = (p.Id).ToString(),
                                                          Name=p.Name,
                                                          Dec=p.Dec
                                                      })

                     .Skip((rowTypeOutDto.PageIndex * rowTypeOutDto.PageSize) - rowTypeOutDto.PageSize).Take(rowTypeOutDto.PageSize)) ;

                    return Json(new { code=200 ,Count = result.Count(), data = result });
                }

                if (rowTypeOutDto.PageIndex == -1 && rowTypeOutDto.PageSize == -1)
                {
                    var result = await Task.Run(() => (from p in db.Z_RowType.Where(p => true)
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                                                       );



                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (rowTypeOutDto.PageIndex != null && rowTypeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (from p in db.Z_RowType.Where(p => true)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })
                    .Skip((rowTypeOutDto.PageIndex * rowTypeOutDto.PageSize) - rowTypeOutDto.PageSize).Take(rowTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (rowTypeOutDto.PageIndex != null && rowTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(rowTypeOutDto.Id))
                {
                    var result = await Task.Run(() => (from p in db.Z_RowType.Where(p => p.Id == long.Parse(rowTypeOutDto.Id))
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                    {
                        Id = (p.Id).ToString(),
                        Name = p.Name,
                        Dec = p.Dec
                    }));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
              
                return  Json(new { code = 400,msg="添加失败" });
            }
            catch (Exception)
            {

                throw;
            }

        }



        }
}
