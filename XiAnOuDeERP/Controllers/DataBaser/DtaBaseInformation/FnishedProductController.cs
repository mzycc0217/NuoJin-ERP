using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.MethodWay;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.IntoPut;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.OutoPut;
using XiAnOuDeERP.Models.Util;
using EnumExtensions = XiAnOuDeERP.Models.Util.EnumExtensions;

namespace XiAnOuDeERP.Controllers.DataBaser.DtaBaseInformation
{
    [AppAuthentication]
    [RoutePrefix("api/FnishedProduct")]
    public class FnishedProductController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加成品半成品
        /// </summary>
        /// <param name="z_FnishedProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddFnishedProduct(Z_FnishedProductDto z_FnishedProductDto)
        {

            try
            {

                var userId = ((UserIdentity)User.Identity).UserId;
                Z_FnishedProduct z_FnishedProduct = new Z_FnishedProduct
                    {
                        Id = IdentityManager.NewId(),
                        Name = z_FnishedProductDto.Name,
                        Encoding = z_FnishedProductDto.Encoding,
                        EntryPersonId = z_FnishedProductDto.EntryPersonId,
                        CompanyId = z_FnishedProductDto.Companyid,
                        Desc = z_FnishedProductDto.Desc,
                        Z_FinshedProductTypeid = z_FnishedProductDto.Z_FinshedProductTypeid,
                        Finshed_Sign= z_FnishedProductDto.Finshed_Sign,
                        EnglishName = z_FnishedProductDto.EnglishName,
                        Abbreviation = z_FnishedProductDto.Abbreviation,
                        BeCommonlyCalled1 = z_FnishedProductDto.BeCommonlyCalled1,
                        BeCommonlyCalled2 = z_FnishedProductDto.BeCommonlyCalled2,
                        CASNumber = z_FnishedProductDto.CASNumber,
                        MolecularWeight = z_FnishedProductDto.MolecularWeight,
                        MolecularFormula = z_FnishedProductDto.MolecularFormula,
                        StructuralFormula = z_FnishedProductDto.StructuralFormula,
                        Density = z_FnishedProductDto.Density,
                        Statement = z_FnishedProductDto.Statement,
                        Number= z_FnishedProductDto.Number,
                        Caution = z_FnishedProductDto.Caution,
                        AppearanceState = z_FnishedProductDto.AppearanceState,
                        WarehousingTypeId = z_FnishedProductDto.WarehousingTypeId,
                    };
                var result = await Task.Run(() => db.Entrepots.AsNoTracking().FirstOrDefaultAsync(p => p.Id > 0));
                FnishedProductRoom fnishedProductRoom = new FnishedProductRoom
                    {
                        Id = IdentityManager.NewId(),
                        FnishedProductId = z_FnishedProduct.Id,
                     User_id= userId,
                        RawNumber = 0,
                       EntrepotId = result.Id

                };
                    db.FnishedProductRooms.Add(fnishedProductRoom);
                    db.Z_FnishedProduct.Add(z_FnishedProduct);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        return Json(new { code = 200, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { code = 400, msg = "添加失败" });
                    }

              


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除成品半成品
        /// </summary>
        /// <param name="z_FnishedProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveFnishedProduct(Z_FnishedProductDto z_FnishedProductDto)
        {
            try
            {
                if (z_FnishedProductDto.del_Id != null)
                {
                    foreach (var item in z_FnishedProductDto.del_Id)
                    {
                        var result = new Z_FnishedProduct { Id = item };
                        db.Entry(result).State = System.Data.Entity.EntityState.Unchanged;
                        result.del_or = 1;
                       // var resul = new FnishedProductRoom { FnishedProductId = item };
                        var res = await db.FnishedProductRooms.SingleOrDefaultAsync(s => s.FnishedProductId == item);
                        if (res != null)
                        {
                            res.RawNumber = 10;
                            res.RawOutNumber = 0;
                            res.Warning_RawNumber = 0;
                        }
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
        ///成品半成品
        /// </summary>
        /// <param name="z_FnishedProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditFnishedProduct(Z_FnishedProductDto z_FnishedProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var type = new Models.Db.Aggregate.FinancialManagement.WarehouseManagements.Z_FnishedProduct() { Id = z_FnishedProductDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    // Z_RawDto z_RawDto1 = new Z_RawDto();
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Name))
                    {
                        type.Name = z_FnishedProductDto.Name;
                    }
                    if (z_FnishedProductDto.Number != null)
                    {
                        type.Number = z_FnishedProductDto.Number;
                    }
                    if (z_FnishedProductDto.Companyid!=null)
                    {
                        type.CompanyId = z_FnishedProductDto.Companyid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Encoding))
                    {
                        type.Encoding = z_FnishedProductDto.Encoding;
                    }
                    if (z_FnishedProductDto.EntryPersonId != null)
                    {
                        type.EntryPersonId = z_FnishedProductDto.EntryPersonId;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Desc))
                    {
                        type.Desc = z_FnishedProductDto.Desc;
                    }
                    if (z_FnishedProductDto.Z_FinshedProductTypeid != null)
                    {
                        type.Z_FinshedProductTypeid = z_FnishedProductDto.Z_FinshedProductTypeid;
                    }
                    if (z_FnishedProductDto.Finshed_Sign != null)
                    {
                        type.Finshed_Sign = z_FnishedProductDto.Finshed_Sign;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.EnglishName))
                    {
                        type.EnglishName = z_FnishedProductDto.EnglishName;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Abbreviation))
                    {
                        type.Abbreviation = z_FnishedProductDto.Abbreviation;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.BeCommonlyCalled1))
                    {
                        type.BeCommonlyCalled1 = z_FnishedProductDto.BeCommonlyCalled1;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.BeCommonlyCalled2))
                    {
                        type.BeCommonlyCalled2 = z_FnishedProductDto.BeCommonlyCalled2;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.CASNumber))
                    {
                        type.CASNumber = z_FnishedProductDto.CASNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.MolecularWeight))
                    {
                        type.MolecularWeight = z_FnishedProductDto.MolecularWeight;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.MolecularFormula))
                    {
                        type.MolecularFormula = z_FnishedProductDto.MolecularFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.StructuralFormula))
                    {
                        type.StructuralFormula = z_FnishedProductDto.StructuralFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Density))
                    {
                        type.Density = z_FnishedProductDto.Density;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Statement))
                    {
                        type.Statement = z_FnishedProductDto.Statement;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.Caution))
                    {
                        type.Caution = z_FnishedProductDto.Caution;
                    }
                    if (!string.IsNullOrWhiteSpace(z_FnishedProductDto.AppearanceState))
                    {
                        type.AppearanceState = z_FnishedProductDto.AppearanceState;
                    }
                    if (z_FnishedProductDto.WarehousingTypeId != null)
                    {
                        type.WarehousingTypeId = z_FnishedProductDto.WarehousingTypeId;
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
        /// 获取成品半成品
        /// </summary>
        /// <param name="_FnishedProductOutPut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetFnishedProduct(Z_FnishedProductOutPut _FnishedProductOutPut)
        {
            try
            {
                if (_FnishedProductOutPut.PageIndex != null && _FnishedProductOutPut.PageSize != null && !string.IsNullOrWhiteSpace(_FnishedProductOutPut.Name))
                {
                   // EnumExtensions enumerationConversion = new EnumExtensions();
                    var result = await Task.Run(() => (db.Z_FnishedProduct
                          .Where(x => x.del_or == 0 && x.Id>0||x.Name.Contains(_FnishedProductOutPut.Name))
                        .Select(x => new Z_FnishedProductOutPut
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_FinshedProductTypeid = (x.Z_FinshedProductTypeid).ToString(),
                            Companyid=(x.CompanyId).ToString(),
                            Company = x.Company,
                            Z_FinshedProductType = x.Z_FinshedProductType,
                            Finshed_Sign=x.Finshed_Sign,
                            EnglishName = x.EnglishName,
                            Abbreviation = x.Abbreviation,
                            BeCommonlyCalled1 = x.BeCommonlyCalled1,
                            BeCommonlyCalled2 = x.BeCommonlyCalled2,
                            EnupProduct = ((EnupProduct)x.Finshed_Sign).ToString(),
                            CASNumber = x.CASNumber,
                            MolecularWeight = x.MolecularWeight,
                            MolecularFormula = x.MolecularFormula,
                            StructuralFormula = x.StructuralFormula,
                            Statement = x.Statement,
                            Caution = x.Caution,
                            Number=x.Number,
                            AppearanceState = x.AppearanceState,
                            WarehousingTypeId = (x.WarehousingTypeId).ToString(),
                            WarehousingType = x.WarehousingType,
                            EntryPerson = x.EntryPerson,
                            Density = x.Density,
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                             .Skip((_FnishedProductOutPut.PageIndex * _FnishedProductOutPut.PageSize) - _FnishedProductOutPut.PageSize).Take(_FnishedProductOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }




                if (_FnishedProductOutPut.PageIndex != null && _FnishedProductOutPut.PageSize != null)
                {
                    var result = await Task.Run(() => (db.Z_FnishedProduct
                            .Where(x => x.del_or == 0 && x.Id > 0).Select(x => new Z_FnishedProductOutPut
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_FinshedProductTypeid = (x.Z_FinshedProductTypeid).ToString(),
                                Companyid = (x.CompanyId).ToString(),
                                Company = x.Company,
                                Z_FinshedProductType = x.Z_FinshedProductType,
                                Finshed_Sign = x.Finshed_Sign,
                                EnglishName = x.EnglishName,
                                Abbreviation = x.Abbreviation,
                                BeCommonlyCalled1 = x.BeCommonlyCalled1,
                                BeCommonlyCalled2 = x.BeCommonlyCalled2,
                                EnupProduct = ((EnupProduct)x.Finshed_Sign).ToString(),
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
                            .Skip((_FnishedProductOutPut.PageIndex * _FnishedProductOutPut.PageSize) - _FnishedProductOutPut.PageSize).Take(_FnishedProductOutPut.PageSize).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (_FnishedProductOutPut.PageIndex != null && _FnishedProductOutPut.PageSize != null && !string.IsNullOrWhiteSpace(_FnishedProductOutPut.Id))
                {
                    var result = Task.Run(() => (db.Z_FnishedProduct
                          .Where(x => x.del_or == 0  && x.Id > 0 || x.Id == long.Parse(_FnishedProductOutPut.Id))
                        .Select(x => new Z_FnishedProductOutPut
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Z_FinshedProductTypeid = (x.Z_FinshedProductTypeid).ToString(),
                            Companyid = (x.CompanyId).ToString(),
                            Company = x.Company,
                            Z_FinshedProductType = x.Z_FinshedProductType,
                            Finshed_Sign = x.Finshed_Sign,
                            EnglishName = x.EnglishName,
                            Abbreviation = x.Abbreviation,
                            BeCommonlyCalled1 = x.BeCommonlyCalled1,
                            BeCommonlyCalled2 = x.BeCommonlyCalled2,
                            EnupProduct = ((EnupProduct)x.Finshed_Sign).ToString(),
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
                            .Skip((_FnishedProductOutPut.PageIndex * _FnishedProductOutPut.PageSize) - _FnishedProductOutPut.PageSize).Take(_FnishedProductOutPut.PageSize).ToListAsync()));



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
