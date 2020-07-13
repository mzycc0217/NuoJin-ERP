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
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.PersonnelMatters.AssetssAppService
{
    /// <summary>
    /// 资产服务
    /// </summary>
    [AppAuthentication]
    public class AssetssController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加资产信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddAssets(AssetsInputDto input)
        {
            var department = await db.Departments.SingleOrDefaultAsync(m => m.Id == input.DepartmentId && !m.IsDelete);

            if (department == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该部门不存在" }))
                });
            }

            db.Assetss.Add(new Assets
            {
                Id = IdentityManager.NewId(),
                DepartmentId = input.DepartmentId,
                AssetsTypeId = input.AssetsTypeId,
                Desc = input.Desc,
                Number = input.Number,
                IsDelete = false,
                CompanyId = input.CompanyId
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 修改资产信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateAssets(AssetsInputDto input)
        {
            var assets = await db.Assetss.SingleOrDefaultAsync(m => m.Id == input.AssetsId && !m.IsDelete);

            if (assets == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该资产信息不存在" }))
                });
            }

            var department = await db.Departments.SingleOrDefaultAsync(m => m.Id == input.DepartmentId && !m.IsDelete);

            if (department == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该部门不存在" }))
                });
            }

            assets.AssetsTypeId = input.AssetsTypeId;
            assets.DepartmentId = input.DepartmentId;
            assets.Desc = input.Desc;
            assets.Number = input.Number;
            assets.CompanyId = input.CompanyId;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除资产信息
        /// </summary>
        /// <param name="AssetsIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteAssets(List<long> AssetsIds)
        {
            var list = new List<string>();

            foreach (var item in AssetsIds)
            {
                var assets = await db.Assetss.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (assets != null)
                {
                    assets.IsDelete = true;
                }
                else
                {
                    list.Add("【" + item + "】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 添加资产支出信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddAssetExpenditure(AddAssetExpenditureInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            if (input.PurchaseId != null)
            {
                var purchase = await db.Purchases.SingleOrDefaultAsync(m => m.Id == input.PurchaseId && !m.IsDelete && m.ApprovalType != EApprovalType.Paid);

                if (purchase == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "所选采购订单不存在或已付款" }))
                    });
                }

                purchase.ApprovalType = EApprovalType.Paid;
            }
            
            db.AssetExpenditures.Add(new AssetExpenditure
            {
                Id = IdentityManager.NewId(),
                Desc = input.Desc,
                PurchaseId = input.PurchaseId,
                UserId = userId,
                Amount = input.Amount
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }

        }

        /// <summary>
        /// 添加资产收入信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddAssetIncome(AddAssetIncomeInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            if (input.SaleId != null)
            {
                if (await db.AssetIncomes.AnyAsync(m => m.SaleId == input.SaleId))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "所选销售订单已收款" }))
                    });
                }
            }
            
            db.AssetIncomes.Add(new AssetIncome
            {
                Id = IdentityManager.NewId(),
                Desc = input.Desc,
                SaleId = input.SaleId,
                UserId = userId,
                Amount = input.Amount
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }

        }

        /// <summary>
        /// 添加资产类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddAssetsType(AssetsTypeInputDto input)
        {
            if (await db.AssetsTypes.AnyAsync(m => m.Name == input.AssetsTypeName))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.AssetsTypeName + "已存在" }))
                });
            }

            var assets = new AssetsType()
            {
                Id = IdentityManager.NewId(),
                Name = input.AssetsTypeName,
                IsDelete = false
            };

            db.AssetsTypes.Add(assets);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }

        }

        /// <summary>
        /// 修改资产类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateAssetsType(AssetsTypeInputDto input)
        {
            if (await db.AssetsTypes.AnyAsync(m => m.Name == input.AssetsTypeName))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.AssetsTypeName + "已存在" }))
                });
            }

            var assets = await db.AssetsTypes.SingleOrDefaultAsync(m => m.Id == input.AssetsTypeId);

            if (assets == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.AssetsTypeId + "不存在" }))
                });
            }

            assets.Name = input.AssetsTypeName;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }

        }

        /// <summary>
        /// 批量删除资产类型
        /// </summary>
        /// <param name="AssetsTypeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteAssetsTypeList(List<long> AssetsTypeIds)
        {
            var list = new List<string>();

            foreach (var item in AssetsTypeIds)
            {
                var assets = await db.AssetsTypes.SingleOrDefaultAsync(m => m.Id == item);

                if (assets == null)
                {
                    list.Add(item+"不存在");
                }
                else
                {
                    assets.IsDelete = true;
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取资产类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AssetsTypeOutputDto>> GetAssetsTypeList(GetAssetsTypeListInputDto input)
        {
            var data = await db.AssetsTypes
                .Where(m=>!m.IsDelete)
                .ToListAsync();

            if (input.AssetsTypeId != null)
            {
                data = data.Where(m => m.Id == input.AssetsTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.AssetsTypeName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.AssetsTypeName)).ToList();
            }

            var list = new List<AssetsTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new AssetsTypeOutputDto
                {
                    AssetsTypeId = item.Id.ToString(),
                    AssetsTypeName = item.Name
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.AssetsTypeId)
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
        /// 获取资产信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AssetsOutputDto>> GetAssetsList(GetAssetsListInputDto input)
        {
            var data = await db.Assetss
                    .Include(m => m.Department)
                    .Include(m=>m.AssetsType)
                    .Include(m=>m.Company)
                    .Where(m => !m.IsDelete)
                    .ToListAsync();

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.DepartmentId == input.DepartmentId).ToList();
            }

            if (input.AssetsTypeId != null)
            {
                data = data.Where(m => m.AssetsTypeId == input.AssetsTypeId).ToList();
            }

            var list = new List<AssetsOutputDto>();

            foreach (var item in data)
            {
                list.Add(new AssetsOutputDto
                {
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    AssetsId = item.Id.ToString(),
                    AssetsType = item.AssetsType,
                    Department = item.Department,
                    DepartmentId = item.DepartmentId.ToString(),
                    Desc = item.Desc,
                    Number = item.Number,
                    AssetsTypeId = item.AssetsTypeId.ToString(),
                    CompanyId = item.CompanyId.ToString(),
                    Company = item.Company
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.AssetsId)
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
        /// 获取资产支出列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AssetExpenditureOutputDto>> GetAssetExpenditureList(GetAssetExpenditureListInputDto input)
        {
            var data = await db.AssetExpenditures
                    .Include(m => m.Purchase)
                    .Include(m => m.User)
                    .ToListAsync();

            if (input.AssetExpenditureId != null)
            {
                data = data.Where(m => m.Id == input.AssetExpenditureId).ToList();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.Purchase.Applicant.User.DepartmentId == input.DepartmentId).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (input.PurchaseId != null)
            {
                data = data.Where(m => m.PurchaseId == input.PurchaseId).ToList();
            }

            var list = new List<AssetExpenditureOutputDto>();

            foreach (var item in data)
            {
                list.Add(new AssetExpenditureOutputDto
                {
                    AssetExpenditureId = item.Id.ToString(),
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    Purchase = item.Purchase,
                    PurchaseId = item.PurchaseId.ToString(),
                    UpdateDate = item.UpdateDate,
                    User = item.User,
                    UserId = item.UserId.ToString(),
                    Amount = item.Amount
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.AssetExpenditureId)
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
        /// 获取资产收入列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AssetIncomeOutputDto>> GetAssetIncomeList(GetAssetIncomeListInputDto input)
        {
            var data = await db.AssetIncomes
                    .Include(m => m.Sale)
                    .Include(m => m.User)
                    .ToListAsync();

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (input.DepartmentId != null)
            {
                data = data.Where(m => m.User.User.DepartmentId == input.DepartmentId).ToList();
            }

            if (input.AssetIncomeId != null)
            {
                data = data.Where(m => m.Id == input.AssetIncomeId).ToList();
            }

            if (input.SaleId != null)
            {
                data = data.Where(m => m.SaleId == input.SaleId).ToList();
            }

            var list = new List<AssetIncomeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new AssetIncomeOutputDto
                {
                    AssetIncomeId = item.Id.ToString(),
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    Sale = item.Sale,
                    SaleId = item.SaleId.ToString(),
                    UpdateDate = item.UpdateDate,
                    User = item.User,
                    UserId = item.UserId.ToString(),
                    Amount = item.Amount
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.AssetIncomeId)
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
