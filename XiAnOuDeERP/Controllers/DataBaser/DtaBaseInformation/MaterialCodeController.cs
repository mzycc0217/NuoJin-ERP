using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace XiAnOuDeERP.Controllers.DataBaser.DtaBaseInformation
{
    [AppAuthentication]
    [RoutePrefix("api/MaterialCode")]
    public class MaterialCodeController : ApiController
    {

        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加原材料编码
        /// </summary>
        /// <param name="z_MaterialCodeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddMaterialCode(Z_MaterialCodeDto z_MaterialCodeDto)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    string Moren = " N / A";
                    Z_MaterialCode z_MaterialCode = new Z_MaterialCode
                    {
                        Id = IdentityManager.NewId(),
                        Name = z_MaterialCodeDto.Name,
                        Encoding = z_MaterialCodeDto.Encoding != null ? z_MaterialCodeDto.Encoding : Moren,
                        EntryPersonId = z_MaterialCodeDto.EntryPersonId,
                        Desc = z_MaterialCodeDto.Desc,
                        CompanyId = z_MaterialCodeDto.Companyid,
                        Z_RowTypeid = z_MaterialCodeDto.Z_RowTypeid,
                        EnglishName = z_MaterialCodeDto.EnglishName != null ? z_MaterialCodeDto.EnglishName : Moren,
                        Abbreviation = z_MaterialCodeDto.Abbreviation != null ? z_MaterialCodeDto.Abbreviation : Moren,
                        BeCommonlyCalled1 = z_MaterialCodeDto.BeCommonlyCalled1 != null ? z_MaterialCodeDto.BeCommonlyCalled1 : Moren,
                        BeCommonlyCalled2 = z_MaterialCodeDto.BeCommonlyCalled2 != null ? z_MaterialCodeDto.BeCommonlyCalled2 : Moren,
                        CASNumber = z_MaterialCodeDto.CASNumber != null ? z_MaterialCodeDto.CASNumber : Moren,
                        MolecularWeight = z_MaterialCodeDto.MolecularWeight != null ? z_MaterialCodeDto.MolecularWeight : Moren,
                        MolecularFormula = z_MaterialCodeDto.MolecularFormula,
                        StructuralFormula = z_MaterialCodeDto.StructuralFormula,
                        Density = z_MaterialCodeDto.Density,
                        Statement = z_MaterialCodeDto.Statement != null ? z_MaterialCodeDto.Statement : Moren,
                        Caution = z_MaterialCodeDto.Caution != null ? z_MaterialCodeDto.Caution : Moren,
                        AppearanceState = z_MaterialCodeDto.AppearanceState != null ? z_MaterialCodeDto.AppearanceState : Moren,
                        WarehousingTypeId = z_MaterialCodeDto.WarehousingTypeId,
                    };
                    db.Z_MaterialCode.Add(z_MaterialCode);
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
        /// 删除原材料编码耗材
        /// </summary>
        /// <param name="z_MaterialCodeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveMaterialCode(Z_MaterialCodeDto z_MaterialCodeDto)
        {
            try
            {
                if (z_MaterialCodeDto.del_Id != null)
                {
                    foreach (var item in z_MaterialCodeDto.del_Id)
                    {
                        var result = new Z_MaterialCode { Id = item };
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
        /// 原材料编码用品
        /// </summary>
        /// <param name="z_MaterialCodeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditMaterialCode(Z_MaterialCodeDto z_MaterialCodeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var type = new Models.Db.Aggregate.FinancialManagement.WarehouseManagements.Z_MaterialCode() { Id = z_MaterialCodeDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    // Z_RawDto z_RawDto1 = new Z_RawDto();
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Name))
                    {
                        type.Name = z_MaterialCodeDto.Name;
                    }
                    if (z_MaterialCodeDto.Companyid != null)
                    {
                        type.CompanyId = z_MaterialCodeDto.Companyid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Encoding))
                    {
                        type.Encoding = z_MaterialCodeDto.Encoding;
                    }
                    if (z_MaterialCodeDto.EntryPersonId != null)
                    {
                        type.EntryPersonId = z_MaterialCodeDto.EntryPersonId;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Desc))
                    {
                        type.Desc = z_MaterialCodeDto.Desc;
                    }
                    if (z_MaterialCodeDto.Z_RowTypeid != null)
                    {
                        type.Z_RowTypeid = z_MaterialCodeDto.Z_RowTypeid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.EnglishName))
                    {
                        type.EnglishName = z_MaterialCodeDto.EnglishName;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Abbreviation))
                    {
                        type.Abbreviation = z_MaterialCodeDto.Abbreviation;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.BeCommonlyCalled1))
                    {
                        type.BeCommonlyCalled1 = z_MaterialCodeDto.BeCommonlyCalled1;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.BeCommonlyCalled2))
                    {
                        type.BeCommonlyCalled2 = z_MaterialCodeDto.BeCommonlyCalled2;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.CASNumber))
                    {
                        type.CASNumber = z_MaterialCodeDto.CASNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.MolecularWeight))
                    {
                        type.MolecularWeight = z_MaterialCodeDto.MolecularWeight;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.MolecularFormula))
                    {
                        type.MolecularFormula = z_MaterialCodeDto.MolecularFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.StructuralFormula))
                    {
                        type.StructuralFormula = z_MaterialCodeDto.StructuralFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Density))
                    {
                        type.Density = z_MaterialCodeDto.Density;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Statement))
                    {
                        type.Statement = z_MaterialCodeDto.Statement;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.Caution))
                    {
                        type.Caution = z_MaterialCodeDto.Caution;
                    }
                    if (!string.IsNullOrWhiteSpace(z_MaterialCodeDto.AppearanceState))
                    {
                        type.AppearanceState = z_MaterialCodeDto.AppearanceState;
                    }
                    if (z_MaterialCodeDto.WarehousingTypeId != null)
                    {
                        type.WarehousingTypeId = z_MaterialCodeDto.WarehousingTypeId;
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
        /// 获取原材料编码
        /// </summary>
        /// <param name="_MaterialCodeOutDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetMaterialCode(Z_MaterialCodeOutDto _MaterialCodeOutDto)
        {
            try
            {
                if (_MaterialCodeOutDto.PageIndex != null && _MaterialCodeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(_MaterialCodeOutDto.Name))
                {

                    var result = await Task.Run(() => (db.Z_MaterialCode
                          .Where(x => x.Name.Contains(_MaterialCodeOutDto.Name))
                        .Select(x => new Z_MaterialCodeOutDto
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                             Company = x.Company,
                            Companyid=(x.Company.Id).ToString(),
                            Z_RowType = x.Z_RowType,
                          //  Companyid = (x.CompanyId).ToString(),
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
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                             .Skip((_MaterialCodeOutDto.PageIndex * _MaterialCodeOutDto.PageSize) - _MaterialCodeOutDto.PageSize).Take(_MaterialCodeOutDto.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }




                if (_MaterialCodeOutDto.PageIndex != null && _MaterialCodeOutDto.PageSize != null)
                {
                    var result = await Task.Run(() => (db.Z_MaterialCode
                            .Where(x => x.Id > 0).Select(x => new Z_MaterialCodeOutDto
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                                Companyid = (x.Company.Id).ToString(),
                                Z_RowType = x.Z_RowType,
                                Company = x.Company,
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
                                CreateDate = x.CreateDate
                            }).OrderBy(x => x.CreateDate)
                            .Skip((_MaterialCodeOutDto.PageIndex * _MaterialCodeOutDto.PageSize) - _MaterialCodeOutDto.PageSize).Take(_MaterialCodeOutDto.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (_MaterialCodeOutDto.PageIndex != null && _MaterialCodeOutDto.PageSize != null && !string.IsNullOrWhiteSpace(_MaterialCodeOutDto.Id))
                {
                    var result = Task.Run(() => (db.Z_MaterialCode
                          .Where(x => x.Id == long.Parse(_MaterialCodeOutDto.Id))
                        .Select(x => new Z_MaterialCodeOutDto
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                            Companyid = (x.CompanyId).ToString(),
                            Z_RowType = x.Z_RowType,
                            Company = x.Company,
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
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                            .Skip((_MaterialCodeOutDto.PageIndex * _MaterialCodeOutDto.PageSize) - _MaterialCodeOutDto.PageSize).Take(_MaterialCodeOutDto.PageSize).ToListAsync()));



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
