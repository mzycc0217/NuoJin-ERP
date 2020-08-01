using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt.Outenport;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Db.Saled;
using XiAnOuDeERP.Models.Dto.My_FlowDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.PurchaseInformation.PurshOutDto;
using XiAnOuDeERP.Models.Dto.Saled.Sale_Input;
using XiAnOuDeERP.Models.Dto.Saled.Sale_OutPut;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.Sale_Product
{
    //  [ExperAuthentication]
    [AppAuthentication]
    [RoutePrefix("api/SaleProduct")]
    public class SaleProductController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();
        /// <summary>
        /// 销售申请
        /// </summary>
        /// <param name="productSaleInput"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IHttpActionResult> SetSaleProduct(ProductSaleInput productSaleInput)
        {
            Product_Sale product_Sale = new Product_Sale
            {
                Id = IdentityManager.NewId(),
                Behoof = productSaleInput.Behoof,
                Des = productSaleInput.Des,
                FishProductId = productSaleInput.FishProductId,
                ProductNumber = productSaleInput.ProductNumber,
                QuasiPurchaseNumber = productSaleInput.ProductNumber,
                Sale_Time = DateTime.Now,
                Userid = productSaleInput.Userid,
                Sale_Price = productSaleInput.Sale_Price,
                SupplierId = productSaleInput.SupplierId,
                Is_Or = 1


            };

            db.Product_Sales.Add(product_Sale);
            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "添加销售申请成功" });
            }



            return Json(new { code = 201, msg = "失败" });
        }



        /// <summary>
        /// 领导获取销售申请(却包在同意部门下的领导才可以考)（PageIndex，pageSize）
        /// </summary>
        /// <param name="productSaleOutPut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<ProductSaleOutPut>> GetSaleProduct(ProductSaleOutPut productSaleOutPut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var resul = Task.Run(() => db.UserDetails.AsNoTracking().FirstOrDefault(p => p.Id == userId && p.IsDelete == false));
            var dep = resul.Result.User.DepartmentId;

            //var res = await Task.Run(() => db.Product_Sales.AsNoTracking()
            //.Where(p => p.userDetails.User.DepartmentId == dep && p.del_ort == false && p.Is_Or == 1 || p.Is_Or == 6));


            var result = await Task.Run(() => db.Product_Sales
            .Where(p => p.userDetails.User.DepartmentId == dep && p.del_ort == false && p.Is_Or == 1)
            .Select(p => new ProductSaleOutPut
            {
                Id = p.Id.ToString(),
                FishProductId = p.FishProductId.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                ProductNumber = p.ProductNumber,
                Userid = p.userDetails.Id.ToString(),
                userDetails = p.userDetails,
                Des = p.Des,
                // Count = res.Count(),
                QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                Is_Or = p.Is_Or,
                Behoof = p.Behoof,
                Sale_Price = p.Sale_Price,
                SupplierId = p.SupplierId.ToString(),
                Supplier = p.Supplier,
                Sale_Time = p.Sale_Time

            })
            );
            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null && !string.IsNullOrWhiteSpace(productSaleOutPut.RelName) || !string.IsNullOrWhiteSpace(productSaleOutPut.Name))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name == productSaleOutPut.Name || p.userDetails.RealName == productSaleOutPut.RelName).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));

                var re = await result.ToListAsync();
                foreach (var item in re)
                {
                    item.Count = result.Count();
                }

                return re;

            }

            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null)
            {
                result = await Task.Run(() => result.OrderBy(p => p.Sale_Time)
            .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                var re = await result.ToListAsync();
                foreach (var item in re)
                {
                    item.Count = result.Count();
                }

                return re;
            }



            return await result.ToListAsync();



            //   return await result.ToListAsync().Result.Count();



        }


        public int Finsh_Start { get; set; }
        /// <summary>
        /// 领导签核申请单(Sale_Id)(Des)
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        //(1/6)
        [HttpPost]
        public async Task<IHttpActionResult> AddSaleProduct(LeaderShip_InputDto leaderShip_Input)
        {

            var resut = await Task.Run(() => db.Product_Sales.SingleOrDefaultAsync(p => p.Id == leaderShip_Input.Sale_Id));


            if (resut.Is_Or == 1)//出库
            {
                resut.Is_Or = 2;//库管员获取出库消息
                this.Finsh_Start = 1;
            }
            if (resut.Is_Or == 6)//销售
            {
                resut.Is_Or = 8;//可销售状态
                this.Finsh_Start = 2;
            }
            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des,
                Finsh_Start = this.Finsh_Start
            };
            db.LeaderShips.Add(leaderShip);

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "审核通过" });
            }
            return Json(new { code = 200, msg = "审核失败" });
        }




        /// <summary>
        /// 仓库获取出库申请单（PageIndex，pageSize）
        /// </summary>
        /// <param name="productSaleOutPut"></param>
        /// <returns></returns>
        //(2)

        [HttpPost]
        public async Task<List<ProductSaleOutPut>> GetsSaleProduct(ProductSaleOutPut productSaleOutPut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var res = await Task.Run(() => db.Product_Sales.AsNoTracking()
            .Where(p => p.Is_Or == 2 && p.del_ort == false));


            var result = await Task.Run(() => db.Product_Sales
            .Where(p => p.Is_Or == 2 && p.del_ort == false)
            .Select(p => new ProductSaleOutPut
            {
                Id = p.Id.ToString(),
                FishProductId = p.FishProductId.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                ProductNumber = p.ProductNumber,
                Userid = p.userDetails.Id.ToString(),
                userDetails = p.userDetails,
                Des = p.Des,
                Count = res.Count(),
                QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                Is_Or = p.Is_Or,
                Behoof = p.Behoof,
                Sale_Price = p.Sale_Price,
                SupplierId = p.SupplierId.ToString(),
                Supplier = p.Supplier,
                Sale_Time = p.Sale_Time

            })
            );
            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null && !string.IsNullOrWhiteSpace(productSaleOutPut.RelName) || !string.IsNullOrWhiteSpace(productSaleOutPut.Name))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name == productSaleOutPut.Name || p.userDetails.RealName == productSaleOutPut.RelName).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            result = await Task.Run(() => result.OrderBy(p => p.Sale_Time)
              .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));


            return await result.ToListAsync();


        }



        /// <summary>
        /// 库管员签核申请单(Sale_Id)(enportid?)(Des)
        /// </summary>
        /// <param name = "leaderShip_Input" ></param>
        /// <returns></returns>
        //(1 / 6)
        [HttpPost]
        public async Task<IHttpActionResult> SetSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var result = await Task.Run(() => db.Product_Sales.SingleOrDefaultAsync(p => p.Id == leaderShip_Input.Sale_Id));
            result.Is_Or = 3;
            db.Entry(result).State = System.Data.Entity.EntityState.Modified;
            db.Entry(result).Property("Is_Or").IsModified = true;

            //添加领料
            long Enportid;//仓库id
            if (leaderShip_Input.enportid == null || leaderShip_Input.enportid == 0)
            {
                var res = await Task.Run(() => db.FnishedProductRooms
                .FirstOrDefaultAsync(p => p.FnishedProductId == result.FishProductId && p.RawNumber >= result.QuasiPurchaseNumber));
                if (res == null)
                {
                    return Json(new { code = 200, msg = "仓库数量不足,请安排产成品入库" });
                }
                Enportid = (long)res.EntrepotId;
                res.RawNumber = res.RawNumber - (double)result.QuasiPurchaseNumber;
                db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                db.Entry(result).Property("RawNumber").IsModified = true;
            }
            else
            {
                Enportid = leaderShip_Input.enportid;
                var fnishedProductRoom = await db.FnishedProductRooms
               .FirstOrDefaultAsync(p => p.FnishedProductId == result.FishProductId && p.EntrepotId == Enportid);
                //  OfficeRoom officeRoom = new OfficeRoom { OfficeId = (long)resuls.OfficeId, EntrepotId = Enportid };
                db.Entry(fnishedProductRoom).State = System.Data.Entity.EntityState.Unchanged;
                fnishedProductRoom.RawNumber = fnishedProductRoom.RawNumber - (double)result.QuasiPurchaseNumber;
            }
            //签核表
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des
            };
            db.LeaderShips.Add(leaderShip);
            //领取详情记录表
            FnishedProduct_UserDetils fnishedProduct_UserDetils = new FnishedProduct_UserDetils
            {
                Id = IdentityManager.NewId(),
                FnishedProductId = result.z_FnishedProduct.Id,
                User_id = userId,
                is_or = 1,//出库状态
                entrepotid = Enportid,
                FnishedProductNumber = result.QuasiPurchaseNumber,
                GetTime = DateTime.Now,

            };
            db.FnishedProduct_UserDetils.Add(fnishedProduct_UserDetils);

            if (await db.SaveChangesAsync() > 0)
            {


                return Json(new { code = 200, msg = "出库成功" });
            }
            else
            {
                return Json(new { code = 400, msg = "出库失败" });
            }

        }


        /// <summary>
        /// 获取出库完成的申请单（3）//也就是销售部门获取
        /// </summary>
        /// <param name="productSaleOutPut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<ProductSaleOutPut>> GetsSaleProducts(ProductSaleOutPut productSaleOutPut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var res = await Task.Run(() => db.Product_Sales.AsNoTracking()
            .Where(p => p.Is_Or == 3 && p.del_ort == false));


            var result = await Task.Run(() => db.Product_Sales
            .Where(p => p.Is_Or == 3 && p.del_ort == false)
            .Select(p => new ProductSaleOutPut
            {
                Id = p.Id.ToString(),
                FishProductId = p.FishProductId.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                ProductNumber = p.ProductNumber,
                Userid = p.userDetails.Id.ToString(),
                userDetails = p.userDetails,
                Des = p.Des,
                Count = res.Count(),
                QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                Is_Or = p.Is_Or,
                Behoof = p.Behoof,
                Sale_Price = p.Sale_Price,
                SupplierId = p.SupplierId.ToString(),
                Supplier = p.Supplier,
                Sale_Time = p.Sale_Time

            })
            );
            if (productSaleOutPut.U_Id == 1)
            {
                result = await Task.Run(() => result.Where(p => p.userDetails.Id == userId).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null && !string.IsNullOrWhiteSpace(productSaleOutPut.RelName) || !string.IsNullOrWhiteSpace(productSaleOutPut.Name))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name == productSaleOutPut.Name || p.userDetails.RealName == productSaleOutPut.RelName).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            result = await Task.Run(() => result.OrderBy(p => p.Sale_Time)
              .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));


            return await result.ToListAsync();

        }

        /// <summary>
        /// 修改（完善销售单）（确认准销数量）
        /// </summary>
        /// <param name="productSaleInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> EdditSaleMonad(ProductSaleInput productSaleInput)
        {

            Product_Sale type_res = new Product_Sale { Id = productSaleInput.Id };
         // var type_res = await Task.Run(() => db.Product_Sales.SingleOrDefaultAsync(p => p.Id == productSaleInput.Id));
            type_res.Behoof = productSaleInput.Behoof;
            type_res.Des = productSaleInput.Des;
            type_res.ProductNumber = productSaleInput.ProductNumber;
            type_res.QuasiPurchaseNumber = productSaleInput.QuasiPurchaseNumber;
            type_res.Sale_Price = productSaleInput.Sale_Price;
            type_res.SupplierId = productSaleInput.SupplierId;
            type_res.Userid = productSaleInput.Userid;

            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(type_res).State = System.Data.Entity.EntityState.Unchanged;
            //Type type = typeof(ProductSaleInput);
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //object o = assembly.CreateInstance(type.FullName);
            //PropertyInfo[] propertyInfos = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //string str = string.Empty;
            //str += string.Format("类名称：{0}", type.Name);
            /* if (item.Name != "Id" && item.Name != "z_FnishedProduct" && item.Name != "Supplier" && item.Name != "userDetails")*/      /*item.GetValue(type_res) != null && !string.IsNullOrWhiteSpace(value.ToString()) && name != "Id"&& (long)item.GetValue(type_res) != 0&& (double)item.GetValue(type_res)!=null*/
            foreach (var item in type_res.GetType().GetProperties())
            {

                string name = item.Name;
                //  object value = item.GetValue(type_res);

                object value = item.GetValue(type_res);
                if (item.GetValue(type_res) != null && value.ToString() != "" && item.Name != "Id" &&
                    value.ToString() != "0"&&item.Name != "z_FnishedProduct" && item.Name != "Supplier" && item.Name != "userDetails")
                {

                    Console.WriteLine(item.Name);
                    db.Entry(type_res).Property(item.Name).IsModified = true;
                }
                else
                {
                    if (item.Name != "z_FnishedProduct"&& item.Name != "Supplier" && item.Name != "userDetails")
                    { 
                        Console.WriteLine(item.Name);
                        db.Entry(type_res).Property(item.Name).IsModified = false;
                    }
                  
                }

            }
            if (await db.SaveChangesAsync() > 0)
            {
                db.Configuration.ValidateOnSaveEnabled = true;
                return Json(new { code = 200, msg = "修改成功" });
            }

            return Json(new { code = 301, msg = "修改失败" });
        }


        /// <summary>
        /// 确认完成（销售员）//提交至领导
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SureSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des

            };
            db.LeaderShips.Add(leaderShip);

            var result = new Product_Sale { Id = leaderShip_Input.Sale_Id };

            db.Entry(result).State = EntityState.Unchanged;
            result.Is_Or = 6;

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "确认成功" });
            }
            return Json(new { code = 210, msg = "确认失败" });
        }



        /// <summary>
        /// 驳回(Sale_Id)(Des)
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetRejectSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des

            };
            db.LeaderShips.Add(leaderShip);

            var result = new Product_Sale { Id = leaderShip_Input.Sale_Id };

            db.Entry(result).State = EntityState.Unchanged;
            result.Is_Or = 50;

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "确认成功" });
            }
            return Json(new { code = 210, msg = "确认失败" });
        }

        /// <summary>
        /// 获取有这个产成品的的仓库(出库，入库的选择)(FishProductId)
        /// </summary>
        /// <param name="productSaleInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<FinshProducRooms>> BackSaleMonad(ProductSaleInput productSaleInput)
        {
            var result = await Task.Run(() => db.FnishedProductRooms.AsNoTracking()
            .Where(p => p.FnishedProductId == productSaleInput.FishProductId && p.del_or == false).Select(p => new FinshProducRooms
            {
                FnishedProductId = p.Z_FnishedProduct.Id,

                EntrepotId = p.entrepot.Id.ToString(),
                entrepot = p.entrepot
            }).ToListAsync());
            return result;


        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RejectSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des

            };
            db.LeaderShips.Add(leaderShip);

            var result = new Product_Sale { Id = leaderShip_Input.Sale_Id };

            db.Entry(result).State = EntityState.Unchanged;
            result.Is_Or = 20;//撤销

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "确认成功" });
            }
            return Json(new { code = 210, msg = "确认失败" });
        }

        /// <summary>
        /// 获取撤销的内容（自己的申请人的）
        /// </summary>
        /// <param name="productSaleOutPut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<ProductSaleOutPut>> GetRejectSaleProduct(ProductSaleOutPut productSaleOutPut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var res = await Task.Run(() => db.Product_Sales.AsNoTracking()
            .Where(p => p.Is_Or == 20 && p.userDetails.UserId == userId && p.del_ort == false));


            var result = await Task.Run(() => db.Product_Sales
            .Where(p => p.Is_Or == 20 && p.userDetails.UserId == userId && p.del_ort == false)
            .Select(p => new ProductSaleOutPut
            {
                FishProductId = p.FishProductId.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                ProductNumber = p.ProductNumber,
                Userid = p.userDetails.Id.ToString(),
                userDetails = p.userDetails,
                Des = p.Des,
                Count = res.Count(),
                QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                Is_Or = p.Is_Or,
                Behoof = p.Behoof,
                Sale_Price = p.Sale_Price,
                SupplierId = p.SupplierId.ToString(),
                Supplier = p.Supplier,
                Sale_Time = p.Sale_Time

            })
            );
            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null && !string.IsNullOrWhiteSpace(productSaleOutPut.RelName) || !string.IsNullOrWhiteSpace(productSaleOutPut.Name))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name == productSaleOutPut.Name || p.userDetails.RealName == productSaleOutPut.RelName).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            result = await Task.Run(() => result.OrderBy(p => p.Sale_Time)
              .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));


            return await result.ToListAsync();

        }


        /// <summary>
        /// 恢复撤销(Sale_Id)(Des)
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RecoverRejectSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des

            };
            db.LeaderShips.Add(leaderShip);

            var result = new Product_Sale { Id = leaderShip_Input.Sale_Id };

            db.Entry(result).State = EntityState.Unchanged;
            result.Is_Or = 1;

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "恢复成功" });
            }
            return Json(new { code = 210, msg = "恢复失败" });
        }



        /// <summary>
        /// 获取审核人
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetRejectSaleMonad(LeaderShip_InputDto leaderShip_Input)
        {
            //获取这个采购单下的审核人


            if (leaderShip_Input.Sale_Id != null)
            {
                var result = await Task.Run(() => db.LeaderShips.Where(p => p.Id == leaderShip_Input.Sale_Id)
         //.Select(p =>new Content_Users { 
         //UserDetails=p.UserDetails,
         //ContentDes=p.ContentDes
         //}).ToListAsync());
         .Include(p => p.user_Detils).Include(p => p.Product_Sale));

                return Json(new { code = 200, data = result });

            }

            //获取这个人审核的所有内容
            if (leaderShip_Input.user_Id != null)
            {
                var resul = await Task.Run(() => db.LeaderShips.Where(p => p.Id == leaderShip_Input.Sale_Id)
               .Include(p => p.user_Detils).Include(p => p.Product_Sale).ToListAsync());
                return Json(new { code = 200, data = resul });
            }

            return Json(new { code = 201, msg = "无此参数数据" });





        }

        /// <summary>
        /// 确认销售完成（30）
        /// </summary>
        /// <param name="leaderShip_Input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SureSaleProductFinshed(LeaderShip_InputDto leaderShip_Input)
        {

            var res = await Task.Run(() => db.Product_Sales.AsNoTracking().SingleOrDefaultAsync(p => p.Id == leaderShip_Input.Sale_Id));



            var userId = ((UserIdentity)User.Identity).UserId;
            LeaderShip leaderShip = new LeaderShip
            {
                Id = IdentityManager.NewId(),
                Sale_Id = leaderShip_Input.Sale_Id,
                User_DId = userId,
                Des = leaderShip_Input.Des
            };
            db.LeaderShips.Add(leaderShip);

            var result = new Product_Sale { Id = leaderShip_Input.Sale_Id };

            db.Entry(result).State = EntityState.Unchanged;
            result.Is_Or = 30;//确认完成

            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "确认成功" });
            }
            return Json(new { code = 210, msg = "确认失败" });
        }




        /// <summary>
        /// 获取完成的销售单（可以获取自己的完成的销售单 传1）
        /// </summary>
        /// <param name="productSaleOutPut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<ProductSaleOutPut>> GetSuccessSaleProduct(ProductSaleOutPut productSaleOutPut)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var res = await Task.Run(() => db.Product_Sales.AsNoTracking()
            .Where(p => p.Is_Or == 30 && p.del_ort == false));


            var result = await Task.Run(() => db.Product_Sales
            .Where(p => p.Is_Or == 30 && p.del_ort == false)
            .Select(p => new ProductSaleOutPut
            {
                Id = p.Id.ToString(),
                FishProductId = p.FishProductId.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                ProductNumber = p.ProductNumber,
                Userid = p.userDetails.Id.ToString(),
                userDetails = p.userDetails,
                Des = p.Des,
                Count = res.Count(),
                QuasiPurchaseNumber = p.QuasiPurchaseNumber,
                Is_Or = p.Is_Or,
                Behoof = p.Behoof,
                Sale_Price = p.Sale_Price,
                SupplierId = p.SupplierId.ToString(),
                Supplier = p.Supplier,
                Sale_Time = p.Sale_Time

            })
            );
            if (productSaleOutPut.U_Id == 1)
            {
                result = await Task.Run(() => result.Where(p => p.userDetails.Id == userId).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            if (productSaleOutPut.PageIndex != null && productSaleOutPut.PageSize != null && !string.IsNullOrWhiteSpace(productSaleOutPut.RelName) || !string.IsNullOrWhiteSpace(productSaleOutPut.Name))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name == productSaleOutPut.Name || p.userDetails.RealName == productSaleOutPut.RelName).OrderBy(p => p.Sale_Time)
                  .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));
                return await result.ToListAsync();
            }

            result = await Task.Run(() => result.OrderBy(p => p.Sale_Time)
              .Skip((productSaleOutPut.PageIndex * productSaleOutPut.PageSize) - productSaleOutPut.PageSize).Take(productSaleOutPut.PageSize));


            return await result.ToListAsync();

        }

        /// <summary>
        /// 获取历史销售申请单
        /// </summary>
        /// <param name="hostiry_Prace_SaleOut"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<List<Hostiry_Prace_SaleOut>> GetHositrysPrice(Hostiry_Prace_SaleOut hostiry_Prace_SaleOut)
        {
            //更新数据库
            var result = await Task.Run(() => db.Hostitry_Product_Prices
            .Where(p => p.Id != null && p.del_Or == false)
            .Select(p => new Hostiry_Prace_SaleOut
            {
                Id = p.Id.ToString(),
                FinshProductDes = p.FinshProductDes,
                FinshProductId = p.FinshProductId.ToString(),
                User_Id = p.Id.ToString(),
                z_FnishedProduct = p.z_FnishedProduct,
                UserDetails = p.UserDetails,
                Product_Custorm = p.Product_Custorm,
                CustomNameId = p.CustomNameId.ToString(),
                Price_Time = p.Price_Time

            }).OrderBy(p => p.Price_Time)
                .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
            //.Include(p=>p.Product_Custorm)
            //.Include(p=>p.UserDetails)
            //.Include(p=>p.z_FnishedProduct));


            if (!string.IsNullOrWhiteSpace(hostiry_Prace_SaleOut.CustomName))
            {
                result = await Task.Run(() => result.Where(p => p.Product_Custorm.CustomName.Contains(hostiry_Prace_SaleOut.CustomName))
                .OrderBy(p => p.Price_Time)
                .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(hostiry_Prace_SaleOut.CustomCompany))
            {
                result = await Task.Run(() => result.Where(p => p.Product_Custorm.CustomName.Contains(hostiry_Prace_SaleOut.CustomCompany))
               .OrderBy(p => p.Price_Time)
               .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(hostiry_Prace_SaleOut.FinshProductName))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name.Contains(hostiry_Prace_SaleOut.FinshProductName))

          .OrderBy(p => p.Price_Time)
          .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(hostiry_Prace_SaleOut.FinshProductEcode))
            {
                result = await Task.Run(() => result.Where(p => p.z_FnishedProduct.Name.Contains(hostiry_Prace_SaleOut.FinshProductEcode))

                      .OrderBy(p => p.Price_Time)
                      .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(hostiry_Prace_SaleOut.RelName))
            {
                result = await Task.Run(() => result.Where(p => p.UserDetails.RealName.Contains(hostiry_Prace_SaleOut.RelName))
                     .OrderBy(p => p.Price_Time)
                     .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }

            if (hostiry_Prace_SaleOut.start_Time != null && hostiry_Prace_SaleOut.end_Time != null)
            {

                result = await Task.Run(() => result.Where(p => p.Price_Time >= hostiry_Prace_SaleOut.start_Time && p.Price_Time <= hostiry_Prace_SaleOut.end_Time)
                   .OrderBy(p => p.Id)
                   .Skip((hostiry_Prace_SaleOut.PageIndex * hostiry_Prace_SaleOut.PageSize) - hostiry_Prace_SaleOut.PageSize).Take(hostiry_Prace_SaleOut.PageSize));
                return await result.ToListAsync();
            }
            return await result.ToListAsync();

        }

        //获取签单明细



    }
}
