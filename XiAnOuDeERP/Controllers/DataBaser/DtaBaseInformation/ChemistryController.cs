using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.OutoPut;
using XiAnOuDeERP.Models.Util;
using System.Data.Entity;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.IntoPut;

namespace XiAnOuDeERP.Controllers.DataBaser.DtaBaseInformation
{
    [AppAuthentication]
    [RoutePrefix("api/Chemistry")]
    public class ChemistryController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
       /// <summary>
       /// 添加化学材料
       /// </summary>
       /// <param name="z_ChemistryDto"></param>
       /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddChemistry(Z_ChemistryDto z_ChemistryDto)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    Z_Chemistry z_Chemistry = new Z_Chemistry
                    {
                        Id = IdentityManager.NewId(),
                        Name = z_ChemistryDto.Name,
                        Encoding = z_ChemistryDto.Encoding,
                        EntryPersonId = z_ChemistryDto.EntryPersonId,
                        Desc = z_ChemistryDto.Desc,
                        Z_ChemistryTypeid = z_ChemistryDto.Z_ChemistryTypeid,
                        EnglishName = z_ChemistryDto.EnglishName,
                        Abbreviation = z_ChemistryDto.Abbreviation,
                        BeCommonlyCalled1 = z_ChemistryDto.BeCommonlyCalled1,
                        BeCommonlyCalled2 = z_ChemistryDto.BeCommonlyCalled2,
                        CASNumber = z_ChemistryDto.CASNumber,
                        MolecularWeight = z_ChemistryDto.MolecularWeight,
                        MolecularFormula = z_ChemistryDto.MolecularFormula,
                        StructuralFormula = z_ChemistryDto.StructuralFormula,
                        Density = z_ChemistryDto.Density,
                        CompanyId = z_ChemistryDto.Companyid,
                        Statement = z_ChemistryDto.Statement,
                        Caution = z_ChemistryDto.Caution,
                        AppearanceState = z_ChemistryDto.AppearanceState,
                        WarehousingTypeId = z_ChemistryDto.WarehousingTypeId,
                    };
                    db.Z_Chemistry.Add(z_Chemistry);
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
        /// 删除化学耗材
        /// </summary>
        /// <param name="z_ChemistryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveChemistry(Z_ChemistryDto z_ChemistryDto)
        {
            try
            {
                if (z_ChemistryDto.del_Id != null)
                {
                    foreach (var item in z_ChemistryDto.del_Id)
                    {
                        var result = new Z_Chemistry { Id = item };
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
        /// 化学用品
        /// </summary>
        /// <param name="z_ChemistryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditChemistrye(Z_ChemistryDto z_ChemistryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var type = new Models.Db.Aggregate.FinancialManagement.WarehouseManagements.Z_Chemistry() { Id = z_ChemistryDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    // Z_RawDto z_RawDto1 = new Z_RawDto();
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Name))
                    {
                        type.Name = z_ChemistryDto.Name;
                    }
                    if (z_ChemistryDto.Companyid != null)
                    {
                        type.CompanyId = z_ChemistryDto.Companyid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Encoding))
                    {
                        type.Encoding = z_ChemistryDto.Encoding;
                    }
                    if (z_ChemistryDto.EntryPersonId != null)
                    {
                        type.EntryPersonId = z_ChemistryDto.EntryPersonId;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Desc))
                    {
                        type.Desc = z_ChemistryDto.Desc;
                    }
                    if (z_ChemistryDto.Z_ChemistryTypeid != null)
                    {
                        type.Z_ChemistryTypeid = z_ChemistryDto.Z_ChemistryTypeid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.EnglishName))
                    {
                        type.EnglishName = z_ChemistryDto.EnglishName;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Abbreviation))
                    {
                        type.Abbreviation = z_ChemistryDto.Abbreviation;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.BeCommonlyCalled1))
                    {
                        type.BeCommonlyCalled1 = z_ChemistryDto.BeCommonlyCalled1;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.BeCommonlyCalled2))
                    {
                        type.BeCommonlyCalled2 = z_ChemistryDto.BeCommonlyCalled2;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.CASNumber))
                    {
                        type.CASNumber = z_ChemistryDto.CASNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.MolecularWeight))
                    {
                        type.MolecularWeight = z_ChemistryDto.MolecularWeight;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.MolecularFormula))
                    {
                        type.MolecularFormula = z_ChemistryDto.MolecularFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.StructuralFormula))
                    {
                        type.StructuralFormula = z_ChemistryDto.StructuralFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Density))
                    {
                        type.Density = z_ChemistryDto.Density;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Statement))
                    {
                        type.Statement = z_ChemistryDto.Statement;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.Caution))
                    {
                        type.Caution = z_ChemistryDto.Caution;
                    }
                    if (!string.IsNullOrWhiteSpace(z_ChemistryDto.AppearanceState))
                    {
                        type.AppearanceState = z_ChemistryDto.AppearanceState;
                    }
                    if (z_ChemistryDto.WarehousingTypeId != null)
                    {
                        type.WarehousingTypeId = z_ChemistryDto.WarehousingTypeId;
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
        /// 获取化学用品
        /// </summary>
        /// <param name="z_OfficeOutPut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetChemistry(Z_ChemistryOutDto z_OfficeOutPut)
        {
            try
            {
                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_OfficeOutPut.Name))
                {

                    var result = await Task.Run(() => (db.Z_Chemistry
                          .Where(x => x.Name.Contains(z_OfficeOutPut.Name))
                        .Select(x => new Z_ChemistryOutDto
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_ChemistryTypeid = (x.Z_ChemistryTypeid).ToString(),
                            Company = x.Company,
                            Z_ChemistryType = x.Z_ChemistryType,
                            Companyid = (x.CompanyId).ToString(),
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
                             .Skip((z_OfficeOutPut.PageIndex * z_OfficeOutPut.PageSize) - z_OfficeOutPut.PageSize).Take(z_OfficeOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }




                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null)
                {
                    var result = await Task.Run(() => (db.Z_Chemistry
                            .Where(x => x.Id > 0).Select(x => new Z_ChemistryOutDto
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_ChemistryTypeid = (x.Z_ChemistryTypeid).ToString(),
                                Company = x.Company,
                                Z_ChemistryType = x.Z_ChemistryType,
                                Companyid = (x.CompanyId).ToString(),
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
                            .Skip((z_OfficeOutPut.PageIndex * z_OfficeOutPut.PageSize) - z_OfficeOutPut.PageSize).Take(z_OfficeOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (z_OfficeOutPut.PageIndex != null && z_OfficeOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_OfficeOutPut.Id))
                {
                    var result = Task.Run(() => (db.Z_Chemistry
                          .Where(x => x.Id == long.Parse(z_OfficeOutPut.Id))
                        .Select(x => new Z_ChemistryOutDto
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_ChemistryTypeid = (x.Z_ChemistryTypeid).ToString(),
                            Company = x.Company,
                            Z_ChemistryType = x.Z_ChemistryType,
                            Companyid = (x.CompanyId).ToString(),
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
