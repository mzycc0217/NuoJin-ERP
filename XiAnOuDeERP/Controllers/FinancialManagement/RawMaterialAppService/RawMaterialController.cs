using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.RawMaterialDto;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.RawMaterialDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.FinancialManagement.Controllers.UserAppService
{
    /// <summary>
    /// 原材料服务
    /// </summary>
    [AppAuthentication]
    public class RawMaterialController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加基础数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(RawMaterialInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            if (await db.RawMaterials.AnyAsync(m => m.Name == input.Name && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Name + "已存在禁止重复添加" }))
                });
            }

            var data = new RawMaterial()
            {
                Id = IdentityManager.NewId(),
                Abbreviation = input.Abbreviation,
                EnglishName = input.EnglishName,
                AppearanceState = input.AppearanceState,
                BeCommonlyCalled1 = input.BeCommonlyCalled1,
                BeCommonlyCalled2 = input.BeCommonlyCalled2,
                CASNumber = input.CASNumber,
                EntryPersonId = userId,
                MolecularFormula = input.MolecularFormula,
                MolecularWeight = input.MolecularWeight,
                Name = input.Name,
                RawMaterialType = input.RawMaterialType,
                StructuralFormula = input.StructuralFormula,
                WarehousingTypeId = input.WarehousingTypeId,
                IsDelete = false,
                CompanyId = input.CompanyId
            };
            db.RawMaterials.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddCompany(CompanyInputDto input)
        {
            var company = await db.Companys.SingleOrDefaultAsync(m => m.Name == input.Name && !m.IsDelete);

            if (company != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "单位已存在" }))
                });
            }
            else
            {
                db.Companys.Add(new Company()
                {
                    Id = IdentityManager.NewId(),
                    Name = input.Name,
                    IsDelete = false
                });

                if (await db.SaveChangesAsync() <= 0)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                    });
                }
            }
        }

        /// <summary>
        /// 修改单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateCompany(CompanyInputDto input)
        {
            if (await db.Companys.AnyAsync(m => m.Name == input.Name && m.Id != input.CompanyId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "单位已存在" }))
                });
            }
            else
            {
                var company = await db.Companys.SingleOrDefaultAsync(m => m.Id == input.CompanyId && !m.IsDelete);

                if (company != null)
                {
                    company.Name = input.Name;
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该单位不存在" }))
                    });
                }

                if (await db.SaveChangesAsync() <= 0)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                    });
                }
            }
        }

        /// <summary>
        /// 批量删除单位
        /// </summary>
        /// <param name="CompanyIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteCompanyList(List<long> CompanyIds)
        {
            var list = new List<string>();

            foreach (var item in CompanyIds)
            {
                var data = await db.Companys.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;

        }

        /// <summary>
        /// 更新基础数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(RawMaterialInputDto input)
        {
            var data = await db.RawMaterials.SingleOrDefaultAsync(m => m.Id == input.RawMaterialId && !m.IsDelete);

            if (data != null)
            {
                data.Abbreviation = input.Abbreviation;
                data.EnglishName = input.EnglishName;
                data.AppearanceState = input.AppearanceState;
                data.BeCommonlyCalled1 = input.BeCommonlyCalled1;
                data.BeCommonlyCalled2 = input.BeCommonlyCalled2;
                data.CASNumber = input.CASNumber;
                data.CompanyId = input.CompanyId;
                data.MolecularFormula = input.MolecularFormula;
                data.MolecularWeight = input.MolecularWeight;
                data.Name = input.Name;
                data.RawMaterialType = input.RawMaterialType;
                data.StructuralFormula = input.StructuralFormula;
                data.WarehousingTypeId = input.WarehousingTypeId;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该基础资料不存在" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除基础数据
        /// </summary>
        /// <param name="RawMaterialIds">原材料Id集合</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteRawMaterialList(List<long> RawMaterialIds)
        {
            var list = new List<string>();

            foreach (var item in RawMaterialIds)
            {
                var data = await db.RawMaterials.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;

        }

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<CompanyOutputDto>> GetCompanyList(GetCompanyListInputDto input)
        {
            var list = new List<CompanyOutputDto>();

            var data = await db.Companys.Where(m => !m.IsDelete).ToListAsync();

            if (input.CompanyId != null)
            {
                data = data.Where(m => m.Id == input.CompanyId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.Name)).ToList();
            }

            foreach (var item in data)
            {
                list.Add(new CompanyOutputDto
                {
                    CompanyId = item.Id.ToString(),
                    Name = item.Name
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.CompanyId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

        /// <summary>
        /// 分页获取基础数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<RawMaterialOutputDto>> GetList(GetRawMaterialInputDto input)
        {
            var data = await db.RawMaterials
                .Include(m => m.EntryPerson)
                .Include(m => m.Company)
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.RawMaterialId != null)
            {
                data = data.Where(m => m.Id == input.RawMaterialId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.Name)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.CompanyName))
            {
                data = data.Where(m => m.Company.Name != null && m.Company.Name.Contains(input.CompanyName)).ToList();
            }

            if (input.WarehousingTypeId != null)
            {
                data = data.Where(m => m.WarehousingTypeId == input.WarehousingTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RawMaterialType))
            {
                data = data.Where(m => m.RawMaterialType == input.RawMaterialType).ToList();
            }

            var list = new List<RawMaterialOutputDto>();

            foreach (var item in data)
            {
                list.Add(new RawMaterialOutputDto()
                {
                    RawMaterialId = item.Id.ToString(),
                    Abbreviation = item.Abbreviation,
                    EnglishName = item.EnglishName,
                    AppearanceState = item.AppearanceState,
                    BeCommonlyCalled1 = item.BeCommonlyCalled1,
                    BeCommonlyCalled2 = item.BeCommonlyCalled2,
                    CASNumber = item.CASNumber,
                    MolecularFormula = item.MolecularFormula,
                    MolecularWeight = item.MolecularWeight,
                    RawMaterialType = item.RawMaterialType,
                    StructuralFormula = item.StructuralFormula,
                    Name = item.Name,
                    EntryPerson = item.EntryPerson,
                    EntryPersonId = item.EntryPersonId.ToString(),
                    WarehousingTypeId = item.WarehousingTypeId.ToString(),
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    CompanyId = item.CompanyId.ToString(),
                    Company = item.Company,
                    WarehousingType = item.WarehousingType
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.RawMaterialId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

        /// <summary>
        /// 添加入库类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddWarehousingType(WarehousingTypeInputDto input)
        {
            if (await db.WarehousingTypes.AnyAsync(m => m.Name == input.Name && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Name + "已存在禁止重复添加" }))
                });
            }

            var data = new WarehousingType()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                Desc = input.Desc,
                IsDelete = false
            };

            db.WarehousingTypes.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }

        }

        /// <summary>
        /// 更新入库类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateWarehousingType(WarehousingTypeInputDto input)
        {
            if (await db.WarehousingTypes.AnyAsync(m => m.Name == input.Name && m.Id != input.WarehousingId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Name + "已存在禁止重复添加" }))
                });
            }

            var data = await db.WarehousingTypes.SingleOrDefaultAsync(m=>m.Id == input.WarehousingId);

            if (data != null)
            {
                data.Name = input.Name;
                data.Desc = input.Desc;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.WarehousingId+"不存在" }))
                });
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }

        }

        /// <summary>
        /// 批量删除入库类型
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteWarehousingTypeList(List<long> Ids)
        {
            var list = new List<string>();

            foreach (var item in Ids)
            {
                var data = await db.WarehousingTypes.SingleOrDefaultAsync(m => m.Id == item);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item+"不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取入库类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<WarehousingTypeOutputDto>> WarehousingType(GetWarehousingTypeInputDto input)
        {
            var data = await db.WarehousingTypes
                .Where(m=>!m.IsDelete)
                .ToListAsync();

            if (input.WarehousingId != null)
            {
                data = data.Where(m => m.Id == input.WarehousingId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.Name)).ToList();
            }

            var list = new List<WarehousingTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new WarehousingTypeOutputDto
                {
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    Name = item.Name,
                    WarehousingId = item.Id.ToString(),
                    UpdateDate = item.UpdateDate
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.WarehousingId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

    }
}
