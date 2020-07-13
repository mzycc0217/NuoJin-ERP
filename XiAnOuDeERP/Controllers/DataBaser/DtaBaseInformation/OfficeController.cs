using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.IntoPut;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.OutoPut;
using XiAnOuDeERP.Models.Util;
using System.Data.Entity;

namespace XiAnOuDeERP.Controllers.DataBaser.DtaBaseInformation
{
    [AppAuthentication]
    [RoutePrefix("api/Office")]
    public class OfficeController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加办公用品
        /// </summary>
        /// <param name="z_OfficeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddOffice(Z_OfficeDto z_OfficeDto)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    Z_Office z_Office = new Z_Office
                    {
                        Id = IdentityManager.NewId(),
                        Name = z_OfficeDto.Name,
                        Encoding = z_OfficeDto.Encoding,
                        EntryPersonId = z_OfficeDto.EntryPersonId,
                        Desc = z_OfficeDto.Desc,
                        CompanyId = z_OfficeDto.Companyid,
                        Z_OfficeTypeid = z_OfficeDto.Z_OfficeTypeid,
                        EnglishName = z_OfficeDto.EnglishName,
                        Abbreviation = z_OfficeDto.Abbreviation,
                        BeCommonlyCalled1 = z_OfficeDto.BeCommonlyCalled1,
                        BeCommonlyCalled2 = z_OfficeDto.BeCommonlyCalled2,
                        CASNumber = z_OfficeDto.CASNumber,
                        MolecularWeight = z_OfficeDto.MolecularWeight,
                        MolecularFormula = z_OfficeDto.MolecularFormula,
                        StructuralFormula = z_OfficeDto.StructuralFormula,
                        Density = z_OfficeDto.Density,
                        Statement = z_OfficeDto.Statement,
                        Number= z_OfficeDto.Number,
                        Caution = z_OfficeDto.Caution,
                        AppearanceState = z_OfficeDto.AppearanceState,
                        WarehousingTypeId = z_OfficeDto.WarehousingTypeId,
                    };
                    db.Z_Office.Add(z_Office);
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
        /// 删除办公耗材
        /// </summary>
        /// <param name="z_OfficeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveOffice(Z_OfficeDto z_OfficeDto)
        {
            try
            {
                if (z_OfficeDto.del_Id != null)
                {
                    foreach (var item in z_OfficeDto.del_Id)
                    {
                        var result = new Z_Office { Id= item };
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
        /// 办公用品
        /// </summary>
        /// <param name="z_OfficeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditOffice(Z_OfficeDto z_OfficeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var type = new Models.Db.Aggregate.FinancialManagement.WarehouseManagements.Z_Office() { Id = z_OfficeDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    // Z_RawDto z_RawDto1 = new Z_RawDto();
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Name))
                    {
                        type.Name = z_OfficeDto.Name;
                    }
                    if (z_OfficeDto.Companyid != null)
                    {
                        type.CompanyId = z_OfficeDto.Companyid;
                    }
                    if (z_OfficeDto.Number != null)
                    {
                        type.Number = z_OfficeDto.Number;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Encoding))
                    {
                        type.Encoding = z_OfficeDto.Encoding;
                    }
                    if (z_OfficeDto.EntryPersonId != null)
                    {
                        type.EntryPersonId = z_OfficeDto.EntryPersonId;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Desc))
                    {
                        type.Desc = z_OfficeDto.Desc;
                    }
                    if (z_OfficeDto.Z_OfficeTypeid != null)
                    {
                        type.Z_OfficeTypeid = z_OfficeDto.Z_OfficeTypeid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.EnglishName))
                    {
                        type.EnglishName = z_OfficeDto.EnglishName;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Abbreviation))
                    {
                        type.Abbreviation = z_OfficeDto.Abbreviation;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.BeCommonlyCalled1))
                    {
                        type.BeCommonlyCalled1 = z_OfficeDto.BeCommonlyCalled1;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.BeCommonlyCalled2))
                    {
                        type.BeCommonlyCalled2 = z_OfficeDto.BeCommonlyCalled2;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.CASNumber))
                    {
                        type.CASNumber = z_OfficeDto.CASNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.MolecularWeight))
                    {
                        type.MolecularWeight = z_OfficeDto.MolecularWeight;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.MolecularFormula))
                    {
                        type.MolecularFormula = z_OfficeDto.MolecularFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.StructuralFormula))
                    {
                        type.StructuralFormula = z_OfficeDto.StructuralFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Density))
                    {
                        type.Density = z_OfficeDto.Density;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Statement))
                    {
                        type.Statement = z_OfficeDto.Statement;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.Caution))
                    {
                        type.Caution = z_OfficeDto.Caution;
                    }
                    if (!string.IsNullOrWhiteSpace(z_OfficeDto.AppearanceState))
                    {
                        type.AppearanceState = z_OfficeDto.AppearanceState;
                    }
                    if (z_OfficeDto.WarehousingTypeId != null)
                    {
                        type.WarehousingTypeId = z_OfficeDto.WarehousingTypeId;
                    }
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "修改成功" });
                    }
                    else
                    {
                        return Json(new { code = 200, msg = "修改失败" });
                    }

                }
                else
                {
                    return Json(new { code = 201, msg = "数据格式错误" });
                }


            }
            catch (Exception)
            {

                throw;
            }



        }

        /// <summary>
        /// 获取办公用品
        /// </summary>
        /// <param name="z_OfficeOutPut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetOffice(Z_OfficeOutPut z_OfficeOutPut)
        {

            try
            {
                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_OfficeOutPut.Name))
                {

                    var result = await Task.Run(() => (db.Z_Office
                          .Where(x => x.Name.Contains(z_OfficeOutPut.Name))
                        .Select(x => new Z_OfficeOutPut
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_OfficeTypeid = (x.Z_OfficeTypeid).ToString(),
                            Company = x.Company,
                            Companyid = x.CompanyId.ToString(),
                            Z_OfficeType = x.Z_OfficeType,
                            RowtypeName = x.Name,
                            EnglishName = x.EnglishName,
                            Abbreviation = x.Abbreviation,
                            BeCommonlyCalled1 = x.BeCommonlyCalled1,
                            BeCommonlyCalled2 = x.BeCommonlyCalled2,
                            CASNumber = x.CASNumber,
                            MolecularWeight = x.MolecularWeight,
                            MolecularFormula = x.MolecularFormula,
                            StructuralFormula = x.StructuralFormula,
                            Statement = x.Statement,
                            Caution = x.Caution,
                            AppearanceState = x.AppearanceState,
                            WarehousingTypeId = (x.WarehousingTypeId).ToString(),
                            WarehousingType = x.WarehousingType,
                            EntryPerson = x.EntryPerson,
                            Density = x.Density,
                            Number=x.Number,
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                             .Skip((z_OfficeOutPut.PageIndex * z_OfficeOutPut.PageSize) - z_OfficeOutPut.PageSize).Take(z_OfficeOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }




                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null)
                {
                    var result = await Task.Run(() => (db.Z_Office
                            .Where(x => x.Id > 0).Select(x => new Z_OfficeOutPut
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_OfficeTypeid = (x.Z_OfficeTypeid).ToString(),
                                Company = x.Company,
                                Companyid = x.CompanyId.ToString(),
                                Z_OfficeType = x.Z_OfficeType,
                                RowtypeName = x.Name,
                                EnglishName = x.EnglishName,
                                Abbreviation = x.Abbreviation,
                                BeCommonlyCalled1 = x.BeCommonlyCalled1,
                                BeCommonlyCalled2 = x.BeCommonlyCalled2,
                                CASNumber = x.CASNumber,
                                MolecularWeight = x.MolecularWeight,
                                MolecularFormula = x.MolecularFormula,
                                StructuralFormula = x.StructuralFormula,
                                Statement = x.Statement,
                                Caution = x.Caution,
                                Number = x.Number,
                                AppearanceState = x.AppearanceState,
                                WarehousingTypeId = (x.WarehousingTypeId).ToString(),
                                WarehousingType = x.WarehousingType,
                                EntryPerson = x.EntryPerson,
                                Density = x.Density,
                                CreateDate = x.CreateDate
                            }).OrderBy(x => x.CreateDate)
                            .Skip((z_OfficeOutPut.PageIndex * z_OfficeOutPut.PageSize) - z_OfficeOutPut.PageSize).Take(z_OfficeOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_OfficeOutPut.Id))
                {
                    var result = Task.Run(() => (db.Z_Office
                          .Where(x => x.Id == long.Parse(z_OfficeOutPut.Id))
                        .Select(x => new Z_OfficeOutPut
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_OfficeTypeid = (x.Z_OfficeTypeid).ToString(),
                             Company = x.Company,
                            Companyid = x.CompanyId.ToString(),
                            Z_OfficeType = x.Z_OfficeType,
                            RowtypeName = x.Name,
                            EnglishName = x.EnglishName,
                            Abbreviation = x.Abbreviation,
                            BeCommonlyCalled1 = x.BeCommonlyCalled1,
                            BeCommonlyCalled2 = x.BeCommonlyCalled2,
                            CASNumber = x.CASNumber,
                            MolecularWeight = x.MolecularWeight,
                            MolecularFormula = x.MolecularFormula,
                            StructuralFormula = x.StructuralFormula,
                            Statement = x.Statement,
                            Caution = x.Caution,
                            Number = x.Number,
                            AppearanceState = x.AppearanceState,
                            WarehousingTypeId = (x.WarehousingTypeId).ToString(),
                            WarehousingType = x.WarehousingType,
                            EntryPerson = x.EntryPerson,
                            Density = x.Density,
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                            .Skip((z_OfficeOutPut.PageIndex * z_OfficeOutPut.PageSize) - z_OfficeOutPut.PageSize).Take(z_OfficeOutPut.PageSize).ToListAsync()));



                    return Json(new { code = 200, Count = result.Result.Count(), data = result });
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
