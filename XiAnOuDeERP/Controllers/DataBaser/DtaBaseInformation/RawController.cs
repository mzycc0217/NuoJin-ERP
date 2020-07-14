using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.MethodWay;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase;
using XiAnOuDeERP.Models.Dto.Z_DataBaseDto.Z_DataBase.OutoPut;
using XiAnOuDeERP.Models.Util;
using System.Data.Entity;
using static XiAnOuDeERP.MethodWay.CreatCode;

namespace XiAnOuDeERP.Controllers.DataBaser.DtaBaseInformation
{
    [AppAuthentication]
    [RoutePrefix("api/Raw")]
    public class RawController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 添加原材料
        /// </summary>
        /// <param name="z_RawDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRow(Z_RawDto z_RawDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string Moren = " N / A";
                    Z_Raw z_Raw = new Z_Raw
                    {
                        Id = IdentityManager.NewId(),
                        Name = z_RawDto.Name,
                        Encoding= z_RawDto.Encoding!=null? z_RawDto.Encoding: Moren,
                        EntryPersonId = z_RawDto.EntryPersonId,
                        Desc = z_RawDto.Desc,
                        CompanyId = z_RawDto.Companyid,
                        Z_RowTypeid = z_RawDto.Z_RowTypeid,
                        EnglishName = z_RawDto.EnglishName!=null? z_RawDto.EnglishName: Moren,
                        Abbreviation = z_RawDto.Abbreviation != null ? z_RawDto.Abbreviation : Moren,
                        BeCommonlyCalled1 = z_RawDto.BeCommonlyCalled1 != null ? z_RawDto.BeCommonlyCalled1 : Moren,
                        BeCommonlyCalled2 = z_RawDto.BeCommonlyCalled2 != null ? z_RawDto.BeCommonlyCalled2 : Moren,
                        CASNumber = z_RawDto.CASNumber != null ? z_RawDto.CASNumber : Moren,
                        MolecularWeight = z_RawDto.MolecularWeight != null ? z_RawDto.MolecularWeight : Moren,
                        MolecularFormula = z_RawDto.MolecularFormula,
                        StructuralFormula = z_RawDto.StructuralFormula,
                        Density = z_RawDto.Density,
                        Number = z_RawDto.Number,
                        Statement = z_RawDto.Statement != null ? z_RawDto.Statement : Moren,
                        Caution = z_RawDto.Caution != null ? z_RawDto.Caution : Moren,
                        AppearanceState = z_RawDto.AppearanceState != null ? z_RawDto.AppearanceState : Moren,
                        WarehousingTypeId = z_RawDto.WarehousingTypeId,
                    };
                    db.Z_Raw.Add(z_Raw);
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
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除原材料
        /// </summary>
        /// <param name="z_RawDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRow(Z_RawDto z_RawDto)
        {
            try
            {
                if (z_RawDto.del_Id != null)
                {
                    foreach (var item in z_RawDto.del_Id)
                    {
                        //优化的地方
                        var result =await Task.Run(()=> (db.Z_Raw.AsNoTracking().FirstOrDefault(m => m.Id == item)));
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
        /// 修改原材料
        /// </summary>
        /// <param name="z_RawDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EditRow(Z_RawDto z_RawDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var type = new Models.Db.Aggregate.FinancialManagement.WarehouseManagements.Z_Raw() { Id = z_RawDto.Id };
                    db.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    // Z_RawDto z_RawDto1 = new Z_RawDto();
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Name))
                    {
                        type.Name = z_RawDto.Name;
                    }
                    if (z_RawDto.Number!=null)
                    {
                        type.Number = z_RawDto.Number;
                    }
                    if (z_RawDto.Companyid != null)
                    {
                        type.CompanyId = z_RawDto.Companyid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Encoding))
                    {
                        type.Encoding = z_RawDto.Encoding;
                    }
                    if (z_RawDto.EntryPersonId != null)
                    {
                        type.EntryPersonId = z_RawDto.EntryPersonId;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Desc))
                    {
                        type.Desc = z_RawDto.Desc;
                    }
                    if (z_RawDto.Z_RowTypeid!=null)
                    {
                        type.Z_RowTypeid = z_RawDto.Z_RowTypeid;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.EnglishName))
                    {
                        type.EnglishName = z_RawDto.EnglishName;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Abbreviation))
                    {
                        type.Abbreviation = z_RawDto.Abbreviation;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.BeCommonlyCalled1))
                    {
                        type.BeCommonlyCalled1 = z_RawDto.BeCommonlyCalled1;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.BeCommonlyCalled2))
                    {
                        type.BeCommonlyCalled2 = z_RawDto.BeCommonlyCalled2;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.CASNumber))
                    {
                        type.CASNumber = z_RawDto.CASNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.MolecularWeight))
                    {
                        type.MolecularWeight = z_RawDto.MolecularWeight;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.MolecularFormula))
                    {
                        type.MolecularFormula = z_RawDto.MolecularFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.StructuralFormula))
                    {
                        type.StructuralFormula = z_RawDto.StructuralFormula;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Density))
                    {
                        type.Density = z_RawDto.Density;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Statement))
                    {
                        type.Statement = z_RawDto.Statement;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.Caution))
                    {
                        type.Caution = z_RawDto.Caution;
                    }
                    if (!string.IsNullOrWhiteSpace(z_RawDto.AppearanceState))
                    {
                        type.AppearanceState = z_RawDto.AppearanceState;
                    }
                    if (z_RawDto.WarehousingTypeId!=null)
                    {
                        type.WarehousingTypeId = z_RawDto.WarehousingTypeId;
                    }
                    if (await db.SaveChangesAsync()>0)
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
                    return Json(new { code = 201, msg = "请勿修改空数据" });
                }
               

            }
            catch (Exception)
            {

                throw;
            }


        }
      
        /// <summary>
        /// 获取原材料
        /// </summary>
        /// <param name="z_RawOutPut"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRow(Z_RawOutPut z_RawOutPut)
        {

            try
            {
                if (z_RawOutPut.PageIndex != null && z_RawOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_RawOutPut.Name))
                {

                    var result =await Task.Run(() => (db.Z_Raw
                         .Where(x => x.Name.Contains(z_RawOutPut.Name))
                       .Select(x => new Z_RawOutPut
                       {
                           Id = (x.Id).ToString(),
                           Name = x.Name,
                           Encoding = x.Encoding,
                           Desc = x.Desc,
                           Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                           Company = x.Company,
                           Companyid = x.CompanyId.ToString(),
                           Z_RowType =x.Z_RowType,
                           RowtypeName = x.Name,
                           EnglishName = x.EnglishName,
                           Abbreviation = x.Abbreviation,
                           BeCommonlyCalled1 = x.BeCommonlyCalled1,
                           BeCommonlyCalled2 = x.BeCommonlyCalled2,
                           CASNumber = x.CASNumber,
                           Number = x.Number,
                           MolecularWeight = x.MolecularWeight,
                           MolecularFormula = x.MolecularFormula,
                           StructuralFormula = x.StructuralFormula,
                           Statement = x.Statement,
                           Caution = x.Caution,
                           AppearanceState = x.AppearanceState,
                           WarehousingTypeId = (x.WarehousingTypeId).ToString(),
                           WarehousingType = x.WarehousingType,
                           EntryPerson = x.EntryPerson,
                           Density=x.Density,
                           CreateDate = x.CreateDate
                       }).OrderBy(x => x.CreateDate)
                            .Skip((z_RawOutPut.PageIndex * z_RawOutPut.PageSize) - z_RawOutPut.PageSize).Take(z_RawOutPut.PageSize).ToListAsync()));
                   

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }

                if (z_RawOutPut.PageIndex == -1 && z_RawOutPut.PageSize == -1 && !string.IsNullOrWhiteSpace(z_RawOutPut.Id))
                {
                    var id = long.Parse( z_RawOutPut.Id);


                    var result = await Task.Run(() => (db.Z_Raw
                            .Where(x => x.Id == id).Select(x => new Z_RawOutPut
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                                Z_RowType = x.Z_RowType,
                                Density = x.Density,
                                Company = x.Company,
                                Companyid = x.CompanyId.ToString(),
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
                                CreateDate = x.CreateDate
                            }).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (z_RawOutPut.PageIndex == -1 && z_RawOutPut.PageSize == -1)
                {
                    var result = await Task.Run(() => (db.Z_Raw
                            .Where(x => x.Id > 0).Select(x => new Z_RawOutPut
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                                Z_RowType = x.Z_RowType,
                                Density = x.Density,
                                Company = x.Company,
                                Companyid = x.CompanyId.ToString(),
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
                                CreateDate = x.CreateDate
                            }).ToListAsync()));


                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (z_RawOutPut.PageIndex != null && z_RawOutPut.PageSize != null)
                {
                    var result = await Task.Run(() => (db.Z_Raw
                            .Where(x => x.Id > 0).Select(x => new Z_RawOutPut
                            {
                                Id = (x.Id).ToString(),
                                Name = x.Name,
                                Encoding = x.Encoding,
                                Desc = x.Desc,
                                Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                                Z_RowType = x.Z_RowType,
                                Density = x.Density,
                                Company = x.Company,
                                Companyid = x.CompanyId.ToString(),
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
                                CreateDate = x.CreateDate
                            }).OrderBy(x=>x.CreateDate)
                            .Skip((z_RawOutPut.PageIndex * z_RawOutPut.PageSize) - z_RawOutPut.PageSize).Take(z_RawOutPut.PageSize).ToListAsync()));
                  

                    return Json(new { code = 200, Count = result.Count(), data = result });
                }
                if (z_RawOutPut.PageIndex != null && z_RawOutPut.PageSize != null && !string.IsNullOrWhiteSpace(z_RawOutPut.Id))
                {
                    var result = Task.Run(() => (db.Z_Raw
                          .Where(x => x.Id == long.Parse(z_RawOutPut.Id))
                        .Select(x => new Z_RawOutPut
                        {
                            Id = (x.Id).ToString(),
                            Name = x.Name,
                            Encoding = x.Encoding,
                            Desc = x.Desc,
                            Companyid = x.CompanyId.ToString(),
                            Z_RowTypeid = (x.Z_RowTypeid).ToString(),
                            Z_RowType = x.Z_RowType,
                            Density = x.Density,
                            Company = x.Company,
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
                            CreateDate = x.CreateDate
                        }).OrderBy(x => x.CreateDate)
                            .Skip((z_RawOutPut.PageIndex * z_RawOutPut.PageSize) - z_RawOutPut.PageSize).Take(z_RawOutPut.PageSize).ToListAsync()));

                 

                    return Json(new { code = 200, Count = result.Result.Count(), data = result });
                }

                return Json(new { code = 400, msg = "添加失败" });
            }
            catch (Exception)
            {

                throw;
            }


        }




        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetCode()
        {
           
          
            try
            {
                Random random = new Random();
                var s = random.Next();
              
                    Creatcodes code1 = new Creatcodes(CreatEncond);
                    var st = await Task.Run(() => code1());
                
                    return Json(new { code = 200, data = st });
                
                //if ((s % 2) != 0)
                //{
                //    Creatcodes code2 = new Creatcodes(CreatEnconds);
                //    var st = await Task.Run(() => code2());
                //    return Json(new { code = 200, data = st });
                //}

               
            }
            catch (Exception)
            {

                throw;
            }
           


        }
       


    }
}
