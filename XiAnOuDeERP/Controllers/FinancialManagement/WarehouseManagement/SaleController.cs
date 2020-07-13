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
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.FinancialManagement.WarehouseManagement
{
    /// <summary>
    /// 销售服务
    /// </summary>
    [AppAuthentication]
    public class SaleController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加销售信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddSale(SaleInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var outofStock = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId && !m.IsDelete && m.ApprovalType == EApprovalType.Reviewed);

            if (outofStock == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "出库信息不存在或已销售" }))
                });
            }
            
            if (!await db.UserDetails.AnyAsync(m=>m.Id == input.UserId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该销售员信息不存在" }))
                });
            }

            if (await db.Sales.AnyAsync(m=>m.OutOfStockId == input.OutOfStockId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该出库信息已绑定销售订单" }))
                });
            }

            if (!await db.SaleTypes.AnyAsync(m => m.Id == input.SaleTypeId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该销售类型不存在" }))
                });
            }

            var amount = input.Price * (decimal)outofStock.Number;

            var sale = new Sale()
            {
                Id = IdentityManager.NewId(),
                ActualSale = outofStock.Number,
                OutOfStockId = input.OutOfStockId,
                Url = input.Url,
                Price = input.Price,
                UserId = input.UserId,
                IsDelete = false,
                Amount = amount,
                SaleTypeId = input.SaleTypeId
            };

            db.Sales.Add(sale);

            outofStock.ApprovalType = EApprovalType.Completed;

            db.AssetIncomes.Add(new AssetIncome
            {
                Id = IdentityManager.NewId(),
                Desc = input.AssetIncomeDesc,
                SaleId = sale.Id,
                UserId = userId,
                Amount = sale.Amount
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
        /// 添加销售类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddSaleType(SaleTypeInputDto input)
        {
            if (await db.SaleTypes.AnyAsync(m=>m.Name == input.SaleTypeName && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.SaleTypeName+"已存在" }))
                });
            }

            var data = new SaleType()
            {
                Id = IdentityManager.NewId(),
                IsDelete = false,
                Name = input.SaleTypeName
            };

            db.SaleTypes.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 修改销售类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateAssetsType(SaleTypeInputDto input)
        {
            if (await db.SaleTypes.AnyAsync(m => m.Name == input.SaleTypeName))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.SaleTypeName + "已存在" }))
                });
            }

            var saleType = await db.SaleTypes.SingleOrDefaultAsync(m => m.Id == input.SaleTypeId);

            if (saleType == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.SaleTypeId + "不存在" }))
                });
            }

            saleType.Name = input.SaleTypeName;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }

        }

        /// <summary>
        /// 批量删除销售类型
        /// </summary>
        /// <param name="SaleTypeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteSaleTypeList(List<long> SaleTypeIds)
        {
            var list = new List<string>();

            foreach (var item in SaleTypeIds)
            {
                var assets = await db.SaleTypes.SingleOrDefaultAsync(m => m.Id == item);

                if (assets == null)
                {
                    list.Add(item + "不存在");
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
        /// 获取销售类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SaleTypeOutputDto>> GetSaleTypeList(GetSaleTypeInputDto input)
        {
            var data = await db.SaleTypes
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.SaleTypeId != null)
            {
                data = data.Where(m => m.Id == input.SaleTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.SaeleTypeName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.SaeleTypeName)).ToList();
            }

            var list = new List<SaleTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new SaleTypeOutputDto
                {
                    SaleTypeId = item.Id.ToString(),
                    SaleTypeName = item.Name,
                    CreateDate = item.CreateDate,
                   UpdateDate = item.UpdateDate 
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.SaleTypeId)
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
        /// 修改销售信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateSale(UpdateSaleInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var outOfStock = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId);

            if (outOfStock == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "出库信息不存在" }))
                });
            }

            if (!await db.UserDetails.AnyAsync(m => m.Id == input.UserId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该销售员信息不存在" }))
                });
            }

            var sale = await db.Sales.SingleOrDefaultAsync(m => m.Id == input.SaleId && !m.IsDelete);

            if (sale != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该销售单不存在" }))
                });
            }

            var amount = input.Price * (decimal)outOfStock.Number;

            sale.ActualSale = outOfStock.Number;
            sale.OutOfStockId = input.OutOfStockId;
            sale.Price = input.Price;
            sale.UserId = input.UserId;
            sale.Amount = amount;
            sale.Url = input.Url;
            sale.SaleTypeId = input.SaleTypeId;

            var assetIncomes = await db.AssetIncomes.SingleOrDefaultAsync(m => m.SaleId == sale.Id);

            if (assetIncomes != null)
            {
                assetIncomes.Amount = amount;
                assetIncomes.Desc = input.AssetIncomeDesc;
                assetIncomes.UserId = userId;
            }

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除销售信息
        /// </summary>
        /// <param name="SaleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteSaleList(List<long> SaleIds)
        {
            var list = new List<string>();

            foreach (var item in SaleIds)
            {
                var sale = await db.Sales.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (sale != null)
                {
                    sale.IsDelete = true;
                }
                else
                {
                    list.Add("【"+item+"】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取销售信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SaleOutputDto>> GetSaleList(GetSaleInputDto input)
        {
            var data = await db.Sales
                    .Include(m => m.OutOfStock)
                    .Include(m=>m.OutOfStock.RawMaterial)
                    .Include(m => m.User)
                    .Where(m=>!m.IsDelete)
                    .ToListAsync();

            if (input.SaleId != null)
            {
                data = data.Where(m => m.Id == input.SaleId).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (input.RawMaterialId != null)
            {
                data = data.Where(m => m.OutOfStock.RawMaterialId == input.RawMaterialId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RawMaterialName))
            {
                data = data.Where(m => m.OutOfStock.RawMaterial.Name != null && m.OutOfStock.RawMaterial.Name.Contains(input.RawMaterialName)).ToList();
            }

            var list = new List<SaleOutputDto>();

            foreach (var item in data)
            {
                list.Add(new SaleOutputDto
                {
                    SaleId = item.Id.ToString(),
                    ActualSale = item.ActualSale,
                    CreateDate = item.CreateDate,
                    IsDelete = item.IsDelete,
                    OutOfStock = item.OutOfStock,
                    OutOfStockId = item.OutOfStockId.ToString(),
                    Price = item.Price,
                    User = item.User,
                    UserId = item.UserId.ToString(),
                    UpdateDate = item.UpdateDate,
                    Amount = item.Amount,
                    Url = item.Url,
                    SaleTypeId = item.SaleTypeId.ToString(),
                    SaleType = new SaleTypeOutputDto() { 
                    SaleTypeId  = item.SaleType.Id.ToString(),
                    SaleTypeName = item.SaleType.Name
                    }
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.SaleId)
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
