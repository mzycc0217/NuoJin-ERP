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
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.FinancialManagement.Controllers.UserAppService
{
    /// <summary>
    /// 库存服务
    /// </summary>
    [AppAuthentication]
    public class StockController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 采购入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddWarePurchase(WarehousingInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var data = await db.Purchases.Include(m => m.RawMaterial).SingleOrDefaultAsync(m => m.Id == input.PurchaseId && !m.IsDelete && m.ApprovalType == EApprovalType.Paid);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "采购订单不存在或未付款" }))
                });
            }

            var warehousing = new Warehousing()
            {
                Id = IdentityManager.NewId(),
                Number = data.QuasiPurchaseNumber == null ? 0 : (double)data.QuasiPurchaseNumber,
                ApplicantId = userId,
                ApprovalType = EApprovalType.Completed,
                IsDelete = false,
                PurchaseId = data.Id,
                RawMaterialId = null,
                ApprovalKey = "",
                ApprovalIndex = 0
            };

            db.Warehousings.Add(warehousing);

            data.ApprovalType = EApprovalType.Completed;

            var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

            if (data1 != null)
            {
                data1.Number += data.QuasiPurchaseNumber == null ? 0 : (double)data.QuasiPurchaseNumber;
            }
            else
            {
                db.StorageRooms.Add(new StorageRoom()
                {
                    Id = IdentityManager.NewId(),
                    Number = data.QuasiPurchaseNumber == null ? 0 : (double)data.QuasiPurchaseNumber,
                    RawMaterialId = data.RawMaterialId
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
        /// 成品入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddWareFinishedProduct(WareFinishedProductInputDto input)
        {
            if (!await db.RawMaterials.AnyAsync(m => m.Id == input.RawMaterialId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.RawMaterialId + "该基础数据不存在" }))
                });
            }

            var data = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == input.RawMaterialId);

            if (data != null)
            {
                data.Number += input.Number;
            }
            else
            {
                db.StorageRooms.Add(new StorageRoom()
                {
                    Id = IdentityManager.NewId(),
                    Number = input.Number,
                    RawMaterialId = input.RawMaterialId
                });

                if (await db.SaveChangesAsync() <= 0)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                    });
                }
            }
        }
    
            /// <summary>
            /// 出库
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public async Task OutOfStock(OutOfStockInputDto input)
            {
                var userId = ((UserIdentity)User.Identity).UserId;

                if (!await db.OutOfStockTypes.AnyAsync(m => m.Id == input.OutOfStockTypeId && !m.IsDelete))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.OutOfStockTypeId + "出库类型不存在" }))
                    });
                }

                var data = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == input.RawMaterialId && !m.IsDelete);

                if (data != null)
                {
                    var outOfStock = new OutOfStock()
                    {
                        Id = IdentityManager.NewId(),
                        Number = (double)input.Number,
                        ApplicantId = userId,
                        ApprovalType = EApprovalType.Reviewed,
                        ProjectId = input.ProjectId,
                        RawMaterialId = (long)input.RawMaterialId,
                        IsDelete = false,
                        ApprovalKey = "",
                        ApprovalIndex = 0,
                        OutOfStockTypeId = input.OutOfStockTypeId
                    };

                    db.OutOfStocks.Add(outOfStock);

                    var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

                    if (data1 != null)
                    {
                        data1.Number -= outOfStock.Number;
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.RawMaterialId + "基础数据无库存" }))
                        });
                    }
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该库存不存在" }))
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
        
    
        #region 暂时不需要的接口

        ///// <summary>
        ///// 入库申请
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task AddWarehousing(WarehousingInputDto input)
        //{
        //    var userId = ((UserIdentity)User.Identity).UserId;

        //    var data = await db.Purchases.SingleOrDefaultAsync(m => m.Id == input.PurchaseId && !m.IsDelete && m.ApprovalType == EApprovalType.Paid);

        //    if (data == null)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "采购订单不存在或未付款" }))
        //        });
        //    }

        //    var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "Warehousing");

        //    if (related == null)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "入库申请未绑定审批流，添加失败" }))
        //        });
        //    }

        //    var approval = await db.Approvals.Where(m => m.Key == related.ApprovalKey).ToListAsync();

        //    if (approval == null && approval.Count <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = related.ApprovalKey + "该审批流不存在，添加失败" }))
        //        });
        //    }

        //    var warehousing = new Warehousing();

        //    if (input.RawMaterialId != null && input.PurchaseId == null)
        //    {
        //        warehousing = new Warehousing()
        //        {
        //            Id = IdentityManager.NewId(),
        //            Number = (double)data.QuasiPurchaseNumber,
        //            ApplicantId = userId,
        //            ApprovalType = EApprovalType.UnderReview,
        //            IsDelete = false,
        //            RawMaterialId = input.RawMaterialId,
        //            ApprovalKey = related.ApprovalKey,
        //            ApprovalIndex = 0
        //        };

        //        db.Warehousings.Add(warehousing);

        //        var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == warehousing.ApprovalKey);

        //        if (userTypeKey != null)
        //        {
        //            db.WarehousingApprovals.Add(new WarehousingApproval
        //            {
        //                Id = IdentityManager.NewId(),
        //                WarehousingId = warehousing.Id,
        //                IsApproval = false,
        //                UserTypeKey = userTypeKey.UserTypeKey,
        //                ApprovalIndex = 1
        //            });
        //        }
        //        else
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流异常" }))
        //            });
        //        }
        //    }
        //    else if (input.PurchaseId != null && input.RawMaterialId == null)
        //    {
        //        warehousing = new Warehousing()
        //        {
        //            Id = IdentityManager.NewId(),
        //            Number = (double)input.Number,
        //            ApplicantId = userId,
        //            ApprovalType = EApprovalType.UnderReview,
        //            IsDelete = false,
        //            PurchaseId = input.PurchaseId,
        //            ApprovalKey = related.ApprovalKey,
        //            ApprovalIndex = 0
        //        };

        //        db.Warehousings.Add(warehousing);

        //        var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == warehousing.ApprovalKey);

        //        if (userTypeKey != null)
        //        {
        //            db.WarehousingApprovals.Add(new WarehousingApproval
        //            {
        //                Id = IdentityManager.NewId(),
        //                WarehousingId = warehousing.Id,
        //                IsApproval = false,
        //                UserTypeKey = userTypeKey.UserTypeKey,
        //                ApprovalIndex = 1
        //            });
        //        }
        //        else
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流异常" }))
        //            });
        //        }
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "参数异常" }))
        //        });
        //    }

        //    if (await db.SaveChangesAsync() <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
        //        });
        //    }
        //}

        ///// <summary>
        ///// 出库申请
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task AddOutOfStock(OutOfStockInputDto input)
        //{
        //    var userId = ((UserIdentity)User.Identity).UserId;

        //    var data = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == input.RawMaterialId && !m.IsDelete);

        //    var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "OutOfStock");

        //    if (related == null)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "出库申请未绑定审批流，添加失败" }))
        //        });
        //    }

        //    var approval = await db.Approvals.Where(m => m.Key == related.ApprovalKey).ToListAsync();

        //    if (approval == null && approval.Count <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = related.ApprovalKey + "该审批流不存在，添加失败" }))
        //        });
        //    }

        //    if (data != null)
        //    {
        //        var outOfStock = new OutOfStock()
        //        {
        //            Id = IdentityManager.NewId(),
        //            Number = (double)input.Number,
        //            ApplicantId = userId,
        //            ApprovalType = EApprovalType.UnderReview,
        //            ProjectId = input.ProjectId,
        //            RawMaterialId = (long)input.RawMaterialId,
        //            IsDelete = false,
        //            ApprovalKey = related.ApprovalKey,
        //            ApprovalIndex = 0
        //        };

        //        db.OutOfStocks.Add(outOfStock);

        //        var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == outOfStock.ApprovalKey);

        //        if (userTypeKey != null)
        //        {
        //            db.OutOfStockApprovals.Add(new OutOfStockApproval
        //            {
        //                Id = IdentityManager.NewId(),
        //                OutOfStockId = outOfStock.Id,
        //                IsApproval = false,
        //                UserTypeKey = userTypeKey.UserTypeKey,
        //                ApprovalIndex = 1
        //            });
        //        }
        //        else
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = outOfStock.ApprovalKey + "审批流异常" }))
        //            });
        //        }
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该库存不存在" }))
        //        });
        //    }

        //    if (await db.SaveChangesAsync() <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
        //        });
        //    }
        //}

        ///// <summary>
        ///// 更新入库申请审核状态
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task UpdateWarehousingApprovalType(WarehousingInputDto input)
        //{
        //    var token = ((UserIdentity)User.Identity).Token;

        //    var data = await db.Warehousings.Include(m => m.Purchase).SingleOrDefaultAsync(m => m.Id == input.WarehousingId);

        //    if (data.ApprovalType == EApprovalType.Rejected)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该出库单已被驳回" }))
        //        });
        //    }

        //    var approval = await db.Approvals.Where(m => m.Key == data.ApprovalKey).ToListAsync();

        //    if (approval == null && approval.Count <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "该审批流不存在，请重新提交申请" }))
        //        });
        //    }

        //    var index = data.ApprovalIndex + 1;

        //    var maxIndex = approval.Max(m => m.Deis);

        //    var userApproval = approval.SingleOrDefault(m => m.Deis == index);

        //    if (userApproval != null && input.ApprovalType == null)
        //    {
        //        if (userApproval.UserTypeKey != token.UserTypeKey)
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "无权操作" }))
        //            });
        //        }

        //        data.ApprovalIndex += 1;

        //        data.ApprovalType = EApprovalType.InExecution;

        //        var warehousingApprovals = await db.WarehousingApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.WarehousingId == data.Id);

        //        if (warehousingApprovals == null)
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
        //            });
        //        }

        //        warehousingApprovals.IsApproval = true;
        //        warehousingApprovals.UserId = token.UserId;

        //        var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

        //        if (userTypeKey != null)
        //        {
        //            db.WarehousingApprovals.Add(new WarehousingApproval
        //            {
        //                Id = IdentityManager.NewId(),
        //                ApprovalIndex = index + 1,
        //                IsApproval = false,
        //                WarehousingId = data.Id,
        //                UserTypeKey = userTypeKey.UserTypeKey
        //            });
        //        }

        //        if (maxIndex == data.ApprovalIndex)
        //        {
        //            data.ApprovalType = EApprovalType.Reviewed;

        //            if (data.PurchaseId != null && data.RawMaterialId == null)
        //            {
        //                var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.Purchase.RawMaterialId);

        //                if (data1 != null)
        //                {
        //                    data1.Number += data.Number;
        //                }
        //                else
        //                {
        //                    db.StorageRooms.Add(new StorageRoom()
        //                    {
        //                        Id = IdentityManager.NewId(),
        //                        Number = data.Number,
        //                        RawMaterialId = data.Purchase.RawMaterialId
        //                    });
        //                }
        //            }
        //            else if (data.RawMaterialId != null && data.PurchaseId == null)
        //            {
        //                var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //                if (data1 != null)
        //                {
        //                    data1.Number += data.Number;
        //                }
        //                else
        //                {
        //                    db.StorageRooms.Add(new StorageRoom()
        //                    {
        //                        Id = IdentityManager.NewId(),
        //                        Number = data.Number,
        //                        RawMaterialId = (long)data.RawMaterialId
        //                    });
        //                }
        //            }
        //            else
        //            {
        //                throw new HttpResponseException(new HttpResponseMessage()
        //                {
        //                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "入库申请单绑定基础数据异常" }))
        //                });
        //            }
        //        }
        //    }
        //    else if (maxIndex == data.ApprovalIndex && input.ApprovalType == null)
        //    {
        //        data.ApprovalType = EApprovalType.Reviewed;

        //        if (data.PurchaseId != null && data.RawMaterialId == null)
        //        {
        //            var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.Purchase.RawMaterialId);

        //            if (data1 != null)
        //            {
        //                data1.Number += data.Number;
        //            }
        //            else
        //            {
        //                db.StorageRooms.Add(new StorageRoom()
        //                {
        //                    Id = IdentityManager.NewId(),
        //                    Number = data.Number,
        //                    RawMaterialId = data.Purchase.RawMaterialId
        //                });
        //            }
        //        }
        //        else if (data.RawMaterialId != null && data.PurchaseId == null)
        //        {
        //            var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //            if (data1 != null)
        //            {
        //                data1.Number += data.Number;
        //            }
        //            else
        //            {
        //                db.StorageRooms.Add(new StorageRoom()
        //                {
        //                    Id = IdentityManager.NewId(),
        //                    Number = data.Number,
        //                    RawMaterialId = (long)data.RawMaterialId
        //                });
        //            }
        //        }
        //        else
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "入库申请单绑定基础数据异常" }))
        //            });
        //        }
        //    }
        //    else
        //    {
        //        if (input.ApprovalType != null)
        //        {
        //            data.ApprovalType = (EApprovalType)input.ApprovalType;
        //        }

        //        if (data.ApprovalType == EApprovalType.Reviewed)
        //        {
        //            if (data.PurchaseId != null && data.RawMaterialId == null)
        //            {
        //                var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.Purchase.RawMaterialId);

        //                if (data1 != null)
        //                {
        //                    data1.Number += data.Number;
        //                }
        //                else
        //                {
        //                    db.StorageRooms.Add(new StorageRoom()
        //                    {
        //                        Id = IdentityManager.NewId(),
        //                        Number = data.Number,
        //                        RawMaterialId = data.Purchase.RawMaterialId
        //                    });
        //                }
        //            }
        //            else if (data.RawMaterialId != null && data.PurchaseId == null)
        //            {
        //                var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //                if (data1 != null)
        //                {
        //                    data1.Number += data.Number;
        //                }
        //                else
        //                {
        //                    db.StorageRooms.Add(new StorageRoom()
        //                    {
        //                        Id = IdentityManager.NewId(),
        //                        Number = data.Number,
        //                        RawMaterialId = (long)data.RawMaterialId
        //                    });
        //                }
        //            }
        //            else
        //            {
        //                throw new HttpResponseException(new HttpResponseMessage()
        //                {
        //                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "入库申请单绑定基础数据异常" }))
        //                });
        //            }
        //        }
        //    }

        //    if (await db.SaveChangesAsync() <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
        //        });
        //    }
        //}

        ///// <summary>
        ///// 添加成品入库申请
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task WareFinishedProduct(WareFinishedProductInputDto input)
        //{
        //    var data = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == input.RawMaterialId);

        //    if (data != null)
        //    {
        //        data.Number += input.Number;
        //    }
        //    else
        //    {
        //        db.StorageRooms.Add(new StorageRoom()
        //        {
        //            Id = IdentityManager.NewId(),
        //            Number = input.Number,
        //            RawMaterialId = input.RawMaterialId
        //        });
        //    }

        //    if (await db.SaveChangesAsync() <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
        //        });
        //    }
        //}

        ///// <summary>
        ///// 更新出库申请审核状态
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task UpdateOutOfStockApprovalType(OutOfStockInputDto input)
        //{
        //    var token = ((UserIdentity)User.Identity).Token;

        //    var data = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId);

        //    if (data.ApprovalType == EApprovalType.Rejected)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该出库单已被驳回" }))
        //        });
        //    }

        //    var approval = await db.Approvals.Where(m => m.Key == data.ApprovalKey).ToListAsync();

        //    if (approval == null && approval.Count <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "该审批流不存在，请重新提交申请" }))
        //        });
        //    }

        //    var index = data.ApprovalIndex + 1;

        //    var maxIndex = approval.Max(m => m.Deis);

        //    var userApproval = approval.SingleOrDefault(m => m.Deis == index);

        //    if (userApproval != null && input.ApprovalType == null)
        //    {
        //        if (userApproval.UserTypeKey != token.UserTypeKey)
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "无权操作" }))
        //            });
        //        }
        //        else
        //        {
        //            data.ApprovalIndex += 1;

        //            data.ApprovalType = EApprovalType.InExecution;

        //            var outOfStockApproval = await db.OutOfStockApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.OutOfStockId == data.Id);

        //            if (outOfStockApproval == null)
        //            {
        //                throw new HttpResponseException(new HttpResponseMessage()
        //                {
        //                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
        //                });
        //            }

        //            outOfStockApproval.IsApproval = true;
        //            outOfStockApproval.UserId = token.UserId;

        //            var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

        //            if (userTypeKey != null)
        //            {
        //                db.OutOfStockApprovals.Add(new OutOfStockApproval
        //                {
        //                    Id = IdentityManager.NewId(),
        //                    ApprovalIndex = index + 1,
        //                    IsApproval = false,
        //                    OutOfStockId = data.Id,
        //                    UserTypeKey = userTypeKey.UserTypeKey
        //                });
        //            }

        //            if (maxIndex == data.ApprovalIndex)
        //            {
        //                data.ApprovalType = EApprovalType.Reviewed;

        //                var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //                if (data1 != null)
        //                {
        //                    data1.Number -= data.Number;
        //                }
        //                else
        //                {
        //                    throw new HttpResponseException(new HttpResponseMessage()
        //                    {
        //                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.RawMaterialId + "基础数据无库存" }))
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    else if (maxIndex == data.ApprovalIndex && input.ApprovalType == null)
        //    {
        //        data.ApprovalType = EApprovalType.Reviewed;

        //        var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //        if (data1 != null)
        //        {
        //            data1.Number -= data.Number;
        //        }
        //        else
        //        {
        //            throw new HttpResponseException(new HttpResponseMessage()
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.RawMaterialId + "基础数据无库存" }))
        //            });
        //        }
        //    }
        //    else
        //    {
        //        if (input.ApprovalType != null)
        //        {
        //            data.ApprovalType = (EApprovalType)input.ApprovalType;
        //        }

        //        if (data.ApprovalType == EApprovalType.Reviewed)
        //        {
        //            var data1 = await db.StorageRooms.SingleOrDefaultAsync(m => m.RawMaterialId == data.RawMaterialId);

        //            if (data1 != null)
        //            {
        //                data1.Number -= data.Number;
        //            }
        //            else
        //            {
        //                throw new HttpResponseException(new HttpResponseMessage()
        //                {
        //                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.RawMaterialId + "基础数据无库存" }))
        //                });
        //            }
        //        }
        //    }

        //    if (await db.SaveChangesAsync() <= 0)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage()
        //        {
        //            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
        //        });
        //    }

        //}

        #endregion

        /// <summary>
        /// 添加出库类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddOutOfStockType(OutOfStockTypeInputDto input)
        {
            if (await db.OutOfStockTypes.AnyAsync(m => m.Name == input.OutOfStockName && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.OutOfStockName + "已存在" }))
                });
            }

            var data = new OutOfStockType()
            {
                Id = IdentityManager.NewId(),
                IsDelete = false,
                Name = input.OutOfStockName
            };

            db.OutOfStockTypes.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 修改出库类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateAssetsType(OutOfStockTypeInputDto input)
        {
            if (await db.OutOfStockTypes.AnyAsync(m => m.Name == input.OutOfStockName))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.OutOfStockName + "已存在" }))
                });
            }

            var outOfStockType = await db.OutOfStockTypes.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId);

            if (outOfStockType == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.OutOfStockId + "不存在" }))
                });
            }

            outOfStockType.Name = input.OutOfStockName;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }

        }

        /// <summary>
        /// 批量删除出库类型
        /// </summary>
        /// <param name="OutOfStockTypeIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteSaleTypeList(List<long> OutOfStockTypeIds)
        {
            var list = new List<string>();

            foreach (var item in OutOfStockTypeIds)
            {
                var assets = await db.OutOfStockTypes.SingleOrDefaultAsync(m => m.Id == item);

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
        /// 获取出库类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<OutOfStockTypeOutputDto>> GetSaleTypeList(GetOutOfStockTypeInputDto input)
        {
            var data = await db.OutOfStockTypes
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.OutOfStockTypeId != null)
            {
                data = data.Where(m => m.Id == input.OutOfStockTypeId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.OutOfStockTypeName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.OutOfStockTypeName)).ToList();
            }

            var list = new List<OutOfStockTypeOutputDto>();

            foreach (var item in data)
            {
                list.Add(new OutOfStockTypeOutputDto
                {
                    OutOfStockId = item.Id.ToString(),
                    OutOfStockName = item.Name,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.OutOfStockId)
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
        /// 更新入库申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateWarehousing(WarehousingInputDto input)
        {
            var data = await db.Warehousings.Include(m => m.Purchase).SingleOrDefaultAsync(m => m.Id == input.WarehousingId);

            if (data != null && data.ApprovalType != EApprovalType.Completed)
            {
                data.Number = input.Number;

                data.PurchaseId = input.PurchaseId;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "入库申请不存在或已完成禁止修改" }))
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
        /// 更新出库申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateOutOfStock(OutOfStockInputDto input)
        {
            var data = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == input.OutOfStockId);

            if (!await db.OutOfStockTypes.AnyAsync(m => m.Id == input.OutOfStockTypeId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.OutOfStockTypeId + "出库类型不存在" }))
                });
            }

            if (data != null && data.ApprovalType != EApprovalType.Completed)
            {
                data.Number = input.Number;
                if (input.ProjectId != null)
                {
                    if (!await db.Projects.AnyAsync(m => m.Id == input.ProjectId && !m.IsDelete))
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ProjectId + "项目不存在" }))
                        });
                    }
                    data.ProjectId = (long)input.ProjectId;
                }
                if (input.RawMaterialId != null)
                {
                    if (!await db.RawMaterials.AnyAsync(m => m.Id == input.RawMaterialId && !m.IsDelete))
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.RawMaterialId + "基础数据不存在" }))
                        });
                    }
                    data.RawMaterialId = (long)input.RawMaterialId;
                }
                data.OutOfStockTypeId = input.OutOfStockTypeId;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "出库数据不存在或已完成禁止修改" }))
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
        /// 批量删除出库申请
        /// </summary>
        /// <param name="OuOfStockIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteOutOfStock(List<long> OuOfStockIds)
        {
            var list = new List<string>();
            foreach (var item in OuOfStockIds)
            {
                var data = await db.OutOfStocks.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete && m.ApprovalType != EApprovalType.Reviewed);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在或已审核！");
                }

            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 批量删除入库申请
        /// </summary>
        /// <param name="WarehousingIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteWarehousing(List<long> WarehousingIds)
        {
            var list = new List<string>();
            foreach (var item in WarehousingIds)
            {
                var data = await db.Warehousings.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete && m.ApprovalType != EApprovalType.Reviewed);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在或已审核！");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 库存绑定库管人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task StorageRoomAddWarehouseKeeper(StorageRoomAddWarehouseKeeperInputDto input)
        {
            var storageRoom = await db.StorageRooms.SingleOrDefaultAsync(m => m.Id == input.StorageRoomId && !m.IsDelete);

            if (storageRoom != null)
            {
                if (await db.UserDetails.AnyAsync(m=>m.Id == input.WarehouseKeeperId && !m.IsDelete))
                {
                    storageRoom.WarehouseKeeperId = input.WarehouseKeeperId;
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.WarehouseKeeperId + "不存在" }))
                    });
                }
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.StorageRoomId + "不存在" }))
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
        /// 获取出库申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<OutOfStockOutputDto>> GetOutOfStock(GetOutOfStockInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;
            var userTypeKeys = token.UserTypeKey;

            var data = await db.OutOfStocks
                .Include(m => m.Applicant)
                .Include(m => m.Project)
                .Include(m => m.RawMaterial)
                .Include(m => m.OutOfStockType)
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.IsApproval)
            {
                var outOfStock = await db.OutOfStockApprovals
                    .Include(m => m.OutOfStock)
                    .Include(m => m.OutOfStock.Applicant)
                    .Include(m => m.OutOfStock.Project)
                    .Include(m => m.OutOfStock.RawMaterial)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.OutOfStock.IsDelete && m.OutOfStock.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.OutOfStock.IsDelete && m.OutOfStock.ApprovalType != EApprovalType.Rejected)
                    .Select(m => m.OutOfStock)
                    .ToListAsync();

                data = outOfStock;
            }

            if (input.UserId != null)
            {
                var outOfStock = await db.OutOfStockApprovals
                    .Include(m => m.OutOfStock)
                    .Include(m => m.OutOfStock.Applicant)
                    .Include(m => m.OutOfStock.Project)
                    .Include(m => m.OutOfStock.RawMaterial)
                    .Where(m => m.UserId == input.UserId && !m.OutOfStock.IsDelete && m.OutOfStock.ApprovalType == EApprovalType.Reviewed && m.IsApproval)
                    .Select(m => m.OutOfStock)
                    .ToListAsync();

                data = outOfStock;
            }

            if (input.OutOfStockId != null)
            {
                data = data.Where(m => m.Id == input.OutOfStockId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RawMaterialName))
            {
                data = data.Where(m => m.RawMaterial.Name != null && m.RawMaterial.Name.Contains(input.RawMaterialName)).ToList();
            }

            if (input.WarehousingTypeId != null)
            {
                data = data.Where(m => m.RawMaterial.WarehousingTypeId == input.WarehousingTypeId).ToList();
            }

            if (input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            var list = new List<OutOfStockOutputDto>();

            foreach (var item in data)
            {
                list.Add(new OutOfStockOutputDto()
                {
                    OutOfStockId = item.Id.ToString(),
                    Applicant = item.Applicant,
                    ApplicantId = item.ApplicantId.ToString(),
                    ApprovalType = item.ApprovalType,
                    Number = item.Number,
                    Project = item.Project,
                    ProjectId = item.ProjectId.ToString(),
                    RawMaterial = item.RawMaterial,
                    RawMaterialId = item.RawMaterialId.ToString(),
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    OutOfStockTypeId = item.OutOfStockTypeId.ToString(),
                    OutOfStockType = item.OutOfStockType
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.OutOfStockId)
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
        /// 获取入库申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<WarehousingOutputDto>> GetWarehousing(GetWarehousingInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.Warehousings
                    .Include(m => m.Applicant)
                    .Include(m => m.Purchase.RawMaterial)
                    .Where(m => !m.IsDelete)
                    .ToListAsync();

            if (input.IsApproval)
            {
                var warehousing = await db.WarehousingApprovals
                    .Include(m => m.Warehousing)
                    .Include(m => m.Warehousing.Applicant)
                    .Include(m => m.Warehousing.Purchase.RawMaterial)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.Warehousing.IsDelete && m.Warehousing.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.Warehousing.IsDelete && m.Warehousing.ApprovalType != EApprovalType.Rejected)
                    .Select(m => m.Warehousing)
                    .ToListAsync();

                data = warehousing;
            }

            if (input.UserId != null)
            {
                var warehousing = await db.WarehousingApprovals
                    .Include(m => m.Warehousing)
                    .Include(m => m.Warehousing.Applicant)
                    .Include(m => m.Warehousing.Purchase.RawMaterial)
                    .Where(m => m.UserId == input.UserId && !m.Warehousing.IsDelete && m.Warehousing.ApprovalType == EApprovalType.Reviewed && m.IsApproval)
                    .Select(m => m.Warehousing)
                    .ToListAsync();

                data = warehousing;
            }

            if (input.ApprovalType != null)
            {
                data = data.Where(m => m.ApprovalType == input.ApprovalType).ToList();
            }

            if (input.WarehousingId != null)
            {
                data = data.Where(m => m.Id == input.WarehousingId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.RawMaterialName))
            {
                data = data.Where(m => m.Purchase.RawMaterial.Name != null && m.Purchase.RawMaterial.Name.Contains(input.RawMaterialName)).ToList();
            }

            if (input.WarehousingTypeId != null)
            {
                data = data.Where(m => m.Purchase.RawMaterial.WarehousingTypeId == input.WarehousingTypeId).ToList();
            }

            var list = new List<WarehousingOutputDto>();

            foreach (var item in data)
            {
                list.Add(new WarehousingOutputDto()
                {
                    ApplicantId = item.ApplicantId.ToString(),
                    Applicant = item.Applicant,
                    ApprovalType = item.ApprovalType,
                    Number = item.Number,
                    Purchase = item.Purchase,
                    PurchaseId = item.PurchaseId.ToString(),
                    WarehousingId = item.Id.ToString(),
                    UpdateDate = item.UpdateDate,
                    CreateDate = item.CreateDate
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

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetStorageRoomOutputDto>> GetStorageRoomList(GetStorageRoomInputDto input)
        {
            var data = await db.StorageRooms
                .Include(m => m.RawMaterial)
                .Include(m => m.RawMaterial.Company)
                .Include(m => m.RawMaterial.EntryPerson)
                .Include(m => m.RawMaterial.WarehousingType)
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(input.RawMaterialName))
            {
                data = data.Where(m => m.RawMaterial.Name != null && m.RawMaterial.Name.Contains(input.RawMaterialName)).ToList();
            }

            if (input.StorageRoomId != null)
            {
                data = data.Where(m => m.Id == input.StorageRoomId).ToList();
            }

            if (input.WarehousingTypeId != null)
            {
                data = data.Where(m => m.RawMaterial.WarehousingTypeId == input.WarehousingTypeId).ToList();
            }

            if (input.WarehouseKeeperId != null)
            {
                data = data.Where(m => m.WarehouseKeeperId == input.WarehouseKeeperId).ToList();
            }

            var list = new List<GetStorageRoomOutputDto>();

            foreach (var item in data)
            {
                list.Add(new GetStorageRoomOutputDto()
                {
                    StorageRoomId = item.Id.ToString(),
                    RawMaterialId = item.RawMaterialId.ToString(),
                    RawMaterial = item.RawMaterial,
                    Number = item.Number,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    WarehouseKeeperId = item.WarehouseKeeperId.ToString(),
                    WarehouseKeeper = item.WarehouseKeeper
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.StorageRoomId)
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
