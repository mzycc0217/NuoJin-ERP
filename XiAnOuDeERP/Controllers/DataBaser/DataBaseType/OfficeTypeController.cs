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
    [RoutePrefix("api/OfficeType")]
    public class OfficeTypeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 办公用品耗材类型
        /// </summary>
        /// <param name="officeTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRowType(OfficeTypeDto officeTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(officeTypeDto.Name) && !string.IsNullOrWhiteSpace(officeTypeDto.Dec))
                {

                    Z_OfficeType z_OfficeType = new Z_OfficeType
                    {
                        Id = IdentityManager.NewId(),
                        Name = officeTypeDto.Name,
                        Dec = officeTypeDto.Dec
                    };
                    db.Z_OfficeType.Add(z_OfficeType);
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
        ///办公用品耗材类型
        /// </summary>
        /// <param name="officeTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRowType(OfficeTypeDto officeTypeDto)
        {
            try
            {
                if (officeTypeDto.del_Id!=null)
                {
                    foreach (var item in officeTypeDto.del_Id)
                    {
                        var result = new Z_OfficeType { Id = item };
                       // var result = db.Z_OfficeType.AsNoTracking().First(m => m.Id == item);
                        db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                        result.IsDelete = true;
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
        /// 办公用品耗材类型
        /// </summary>
        /// <param name="officeTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRowType(OfficeTypeDto officeTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(officeTypeDto.Name) && !string.IsNullOrWhiteSpace(officeTypeDto.Dec) && !string.IsNullOrWhiteSpace(officeTypeDto.Id))
                {
                    var res = long.Parse(officeTypeDto.Id);
                    var result = await Task.Run(() => db.Z_OfficeType.Where(m => m.Id == res));
                    //  db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                    foreach (var item in result)
                    {
                        item.Name = officeTypeDto.Name;
                        item.Dec = officeTypeDto.Dec;
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
        /// 办公用品耗材数据
        /// </summary>
        /// <param name="officeTypeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRowType(OfficeTypeOutDto officeTypeOutDto)
        {
            /*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
            try
            {
                if (officeTypeOutDto.PageIndex != null && officeTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(officeTypeOutDto.Name))
                {
                    var result = await Task.Run(() => (from p in db.Z_OfficeType.Where(p => p.Name.Contains(officeTypeOutDto.Name)&& p.IsDelete == false)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                     .Skip((officeTypeOutDto.PageIndex * officeTypeOutDto.PageSize) - officeTypeOutDto.PageSize).Take(officeTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (officeTypeOutDto.PageIndex == -1 && officeTypeOutDto.PageSize == -1)
                {
                    var result = await Task.Run(() => (from p in db.Z_OfficeType.Where(p => true&& p.IsDelete == false)
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                                                       );



                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (officeTypeOutDto.PageIndex != null && officeTypeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (from p in db.Z_OfficeType.Where(p => true&& p.IsDelete == false)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })
                    .Skip((officeTypeOutDto.PageIndex * officeTypeOutDto.PageSize) - officeTypeOutDto.PageSize).Take(officeTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (officeTypeOutDto.PageIndex != null && officeTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(officeTypeOutDto.Id))
                {
                    var result = await Task.Run(() => (from p in db.Z_OfficeType.Where(p => p.Id == long.Parse(officeTypeOutDto.Id)&& p.IsDelete == false)
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
