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
    [RoutePrefix("api/SuppliesType")]
    public class SuppliesTypeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="suppliesTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRowType(SuppliesTypeDto suppliesTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(suppliesTypeDto.Name) && !string.IsNullOrWhiteSpace(suppliesTypeDto.Dec))
                {

                    Z_SuppliesType z_SuppliesType = new Z_SuppliesType
                    {
                        Id = IdentityManager.NewId(),
                        Name = suppliesTypeDto.Name,
                        Dec = suppliesTypeDto.Dec
                    };
                    db.Z_SuppliesType.Add(z_SuppliesType);
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
        /// 删除物料类型
        /// </summary>
        /// <param name="suppliesTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRowType(SuppliesTypeDto suppliesTypeDto)
        {
            try
            {
                if (suppliesTypeDto.del_Id!=null)
                {
                    foreach (var item in suppliesTypeDto.del_Id)
                    {
                    var result = db.Z_SuppliesType.AsNoTracking().First(m => m.Id == item);
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
        /// 修改物料类型
        /// </summary>
        /// <param name="suppliesTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRowType(SuppliesTypeDto suppliesTypeDto)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(suppliesTypeDto.Name) && !string.IsNullOrWhiteSpace(suppliesTypeDto.Dec) && !string.IsNullOrWhiteSpace(suppliesTypeDto.Id))
                {
                    var res = long.Parse(suppliesTypeDto.Id);
                    var result = await Task.Run(() => db.Z_SuppliesType.Where(m => m.Id == res));
                    //  db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                    foreach (var item in result)
                    {
                        item.Name = suppliesTypeDto.Name;
                        item.Dec = suppliesTypeDto.Dec;
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
        /// 获取物料类型数据
        /// </summary>
        /// <param name="suppliesTypeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRowType(SuppliesTypeOutDto suppliesTypeOutDto)
        {
            /*.Skip((PageIndex * PageSize) - PageSize).Take(PageSize)*/
            try
            {
                if (suppliesTypeOutDto.PageIndex != null && suppliesTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(suppliesTypeOutDto.Name))
                {
                    var result = await Task.Run(() => (from p in db.Z_SuppliesType.Where(p => p.Name.Contains(suppliesTypeOutDto.Name))
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                     .Skip((suppliesTypeOutDto.PageIndex * suppliesTypeOutDto.PageSize) - suppliesTypeOutDto.PageSize).Take(suppliesTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (suppliesTypeOutDto.PageIndex == -1 && suppliesTypeOutDto.PageSize == -1)
                {
                    var result = await Task.Run(() => (from p in db.Z_SuppliesType.Where(p => true)
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })

                                                       );



                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (suppliesTypeOutDto.PageIndex != null && suppliesTypeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (from p in db.Z_SuppliesType.Where(p => true)
                                                       orderby p.CreateDate
                                                       select new RowTypeOutDto
                                                       {
                                                           Id = (p.Id).ToString(),
                                                           Name = p.Name,
                                                           Dec = p.Dec
                                                       })
                    .Skip((suppliesTypeOutDto.PageIndex * suppliesTypeOutDto.PageSize) - suppliesTypeOutDto.PageSize).Take(suppliesTypeOutDto.PageSize));

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (suppliesTypeOutDto.PageIndex != null && suppliesTypeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(suppliesTypeOutDto.Id))
                {
                    var result = await Task.Run(() => (from p in db.Z_SuppliesType.Where(p => p.Id == long.Parse(suppliesTypeOutDto.Id))
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
