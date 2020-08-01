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
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserJurisdictionDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserJurisdictionDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.PersonnelMatters.UserJurisdictionAppService
{
    /// <summary>
    /// 用户权限服务
    /// </summary>
    [AppAuthentication]
    public class UserJurisdictionController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddModule(AddModuleInputDto input)
        {
            if (await db.Modules.AnyAsync(m => m.Name == input.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Name + "已存在，禁止重复添加" }))
                });
            }

            var data = await db.Modules.ToListAsync();

            var max = 0;

            if (data != null && data.Count > 0)
            {
                max = data.Max(m => m.DisplayOrder) + 1;
            }

            db.Modules.Add(new Module()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                DisplayOrder = max,
                Key = input.Key
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
        /// 添加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddMenu(AddMenuInputDto input)
        {
            if (await db.Menus.AnyAsync(m => m.Title == input.Title))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Title + "已存在，禁止重复添加" }))
                });
            }

            var data = await db.Menus.ToListAsync();

            var max = 0;

            if (data != null && data.Count > 0)
            {
                max = data.Max(m => m.DisplayOrder) + 1;
            }

            var key = await db.Modules.SingleOrDefaultAsync(m => m.Id == input.ModuleId);

            if (key == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "模块不存在" }))
                });
            }

            db.Menus.Add(new Menu()
            {
                Id = IdentityManager.NewId(),
                Title = input.Title,
                DisplayOrder = max,
                Key = key.Key,
                ModuleId = input.ModuleId,
                Url = input.Url
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
        /// 添加元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddElement(AddElementInputDto input)
        {
            if (await db.Elements.AnyAsync(m => m.Name == input.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.Name + "已存在，禁止重复添加" }))
                });
            }

            var data = await db.Elements.Where(m=>m.MenuId == input.MenuId).ToListAsync();

            var max = 0;

            if (data != null && data.Count > 0)
            {
                max = data.Max(m => m.DisplayOrder) + 1;
            }

            var key = await db.Menus.SingleOrDefaultAsync(m => m.Id == input.MenuId);

            if (key == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "模块不存在" }))
                });
            }

            db.Elements.Add(new Element()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                DisplayOrder = max,
                Key = key.Key,
                MenuId = input.MenuId,
                Value = input.Value
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
        /// 更新模块
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateModule(ModuleInputDto input)
        {
            if (await db.Modules.AnyAsync(m => m.Name == input.Name && m.Id != input.ModuleId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该模块名称已存在" }))
                });
            }

            if (await db.Modules.AnyAsync(m => m.Key == input.Key && m.Id != input.ModuleId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该模块Key已存在" }))
                });
            }

            var module = await db.Modules.SingleOrDefaultAsync(m => m.Id == input.ModuleId);

            if (module != null)
            {
                if (input.Key != module.Key)
                {
                    module.Key = input.Key;

                    var menu = await db.Menus.Where(m => m.ModuleId == module.Id).ToListAsync();

                    foreach (var item in menu)
                    {
                        item.Key = module.Key;

                        var element = await db.Elements.Where(m => m.MenuId == item.Id).ToListAsync();

                        foreach (var item1 in element)
                        {
                            item1.Key = module.Key;
                        }
                    }
                }

                module.Name = input.Name;
                module.DisplayOrder = input.DisplayOrder;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ModuleId + "不存在" }))
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
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateMenu(MenuInputDto input)
        {
            if (await db.Menus.AnyAsync(m => m.Title == input.Title && m.Id != input.MenuId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该菜单名称已存在" }))
                });
            }

            var menu = await db.Menus.SingleOrDefaultAsync(m => m.Id == input.MenuId);

            var module = await db.Modules.SingleOrDefaultAsync(m => m.Id == input.ModuleId);

            if (menu != null && module != null)
            {
                menu.Key = module.Key;
                menu.DisplayOrder = input.DisplayOrder;
                menu.ModuleId = input.ModuleId;
                menu.Title = input.Title;
                menu.Url = input.Url;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.MenuId + "不存在或"+input.ModuleId+"不存在" }))
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
        /// 更新元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateElement(ElementInputDto input)
        {
            if (await db.Elements.AnyAsync(m => m.Name == input.Name && m.Id != input.ElementId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该元素名称已存在" }))
                });
            }

            var element = await db.Elements.SingleOrDefaultAsync(m => m.Id == input.ElementId);

            var menu = await db.Menus.SingleOrDefaultAsync(m => m.Id == input.MenuId);

            if (element != null && menu != null)
            {
                element.Key = menu.Key;
                element.DisplayOrder = input.DisplayOrder;
                element.MenuId = input.MenuId;
                element.Name = input.Name;
                element.Value = input.Value;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ElementId + "不存在或" + input.MenuId + "不存在" }))
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
        /// 批量删除模块
        /// </summary>
        /// <param name="ModuleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteModule(List<long> ModuleIds)
        {
            var list = new List<string>();

            foreach (var item in ModuleIds)//删除模块
            {
                var module = await db.Modules.SingleOrDefaultAsync(m => m.Id == item);

                if (module != null)
                {
                    var userModules = await db.UserModules.Where(m => m.ModuleId == item).ToListAsync();

                    foreach (var item1 in userModules)//删除用户模块权限
                    {
                        db.UserModules.Remove(item1);
                    }

                    var menus = await db.Menus.Where(m => m.ModuleId == item).ToListAsync();

                    db.Modules.Remove(module);

                    foreach (var menu in menus)//删除菜单
                    {
                        var userMenus = await db.UserMenus.Where(m => m.MenuId == menu.Id).ToListAsync();

                        foreach (var userMenu in userMenus)//删除用户菜单权限
                        {
                            db.UserMenus.Remove(userMenu);
                        }

                        var elements = await db.Elements.Where(m => m.MenuId == menu.Id).ToListAsync();

                        db.Menus.Remove(menu);

                        foreach (var element in elements)//删除元素
                        {
                            var userElements = await db.UserElements.Where(m => m.ElementId == element.Id).ToListAsync();

                            foreach (var userElement in userElements)//删除用户元素权限
                            {
                                db.UserElements.Remove(userElement);
                            }

                            db.Elements.Remove(element);
                        }
                    }
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
        /// 批量删除菜单
        /// </summary>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteMenu(List<long> MenuIds)
        {
            var list = new List<string>();

            foreach (var item in MenuIds)//删除菜单
            {
                var menu = await db.Menus.SingleOrDefaultAsync(m => m.Id == item);

                var userMenus = await db.UserMenus.Where(m => m.MenuId == item).ToListAsync();

                if (menu != null)
                {
                    foreach (var userMenu in userMenus)//删除用户菜单权限
                    {
                        db.UserMenus.Remove(userMenu);
                    }

                    var elements = await db.Elements.Where(m => m.MenuId == menu.Id).ToListAsync();

                    db.Menus.Remove(menu);

                    foreach (var element in elements)//删除元素
                    {
                        var userElements = await db.UserElements.Where(m => m.ElementId == element.Id).ToListAsync();

                        foreach (var userElement in userElements)//删除用户元素权限
                        {
                            db.UserElements.Remove(userElement);
                        }

                        db.Elements.Remove(element);
                    }

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
        /// 批量删除元素
        /// </summary>
        /// <param name="ElementIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteElement(List<long> ElementIds)
        {
            var list = new List<string>();

            foreach (var item in ElementIds)
            {
                var element = await db.Elements.SingleOrDefaultAsync(m => m.Id == item);

                var userElement = await db.UserElements.Where(m => m.ElementId == item).ToListAsync();

                if (element != null)
                {
                    foreach (var item1 in userElement)
                    {
                        db.UserElements.Remove(item1);
                    }
                    db.Elements.Remove(element);
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
        /// 授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task ToGrantAuthorization(ToGrantAuthorizationInputDto input)
        {
            var moduleList = await db.UserModules.Where(m => m.UserTypeId == input.UserTypeId).ToListAsync();

            foreach (var item in moduleList)
            {
                db.UserModules.Remove(item);
            }

            if (input.ModuleIds != null && input.ModuleIds.Count > 0)
            {
                foreach (var item in input.ModuleIds)
                {
                    if (await db.Modules.AnyAsync(m => m.Id == item))
                    {
                        db.UserModules.Add(new UserModule()
                        {
                            Id = IdentityManager.NewId(),
                            ModuleId = item,
                            UserTypeId = input.UserTypeId
                        });
                    }
                }
            }
            var menuList = await db.UserMenus.Where(m => m.UserTypeId == input.UserTypeId).ToListAsync();

            foreach (var item in menuList)
            {
                db.UserMenus.Remove(item);
            }

            if (input.MenuIds != null && input.MenuIds.Count > 0)
            {
                foreach (var item in input.MenuIds)
                {
                    if (await db.Menus.AnyAsync(m => m.Id == item))
                    {
                        db.UserMenus.Add(new UserMenu()
                        {
                            Id = IdentityManager.NewId(),
                            MenuId = item,
                            UserTypeId = input.UserTypeId
                        });
                    }
                }
            }


            var elementList = await db.UserElements.Where(m => m.UserTypeId == input.UserTypeId).ToListAsync();

            foreach (var item in elementList)
            {
                db.UserElements.Remove(item);
            }

            if (input.ElementIds != null && input.ElementIds.Count > 0)
            {
                foreach (var item in input.ElementIds)
                {
                    if (await db.Elements.AnyAsync(m => m.Id == item))
                    {
                        db.UserElements.Add(new UserElement()
                        {
                            Id = IdentityManager.NewId(),
                            ElementId = item,
                            UserTypeId = input.UserTypeId
                        });
                    }
                }
            }

            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 获取菜单list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuOutputDto>> GetMenuList(GetMenuListInputDto input)
        {
            var list = new List<MenuOutputDto>();

            var data = await db.Menus.Where(m => m.ModuleId == input.ModuleId).ToListAsync();

            foreach (var item in data)
            {
                list.Add(new MenuOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    MenuId = item.Id.ToString(),
                    ModuleId = item.ModuleId.ToString(),
                    Title = item.Title,
                    Url = item.Url
                });
            }

            return list;
        }

        /// <summary>
        /// 获取元素List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ElementOutputDto>> GetElementList(GetMenuListInputDto input)
        {
            var list = new List<ElementOutputDto>();

            var data = await db.Elements.Where(m => m.MenuId == input.MenuId).ToListAsync();

            foreach (var item in data)
            {
                list.Add(new ElementOutputDto
                {
                    MenuId = item.MenuId.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    ElementId = item.Id.ToString(),
                    Key = item.Key,
                    Name = item.Name,
                    Value = item.Value
                });
            }

            return list;
        }

        /// <summary>
        /// 获取菜单List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserJurisdictionListOutputDto>> GetList()
        {
            var list = new List<GetUserJurisdictionListOutputDto>();

            var moduleList = await db.Modules.ToListAsync();

            var menuList = await db.Menus.ToListAsync();

            var elementList = await db.Elements.ToListAsync();

            var module = new List<ModuleOutputDto>();

            var menu = new List<MenuOutputDto>();

            var element = new List<ElementOutputDto>();

            foreach (var item in moduleList)
            {
                module.Add(new ModuleOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    ModuleId = item.Id.ToString(),
                    Name = item.Name
                });
            }

            foreach (var item in menuList)
            {
                menu.Add(new MenuOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    MenuId = item.Id.ToString(),
                    ModuleId = item.ModuleId.ToString(),
                    Title = item.Title,
                    Url = item.Url
                });
            }

            foreach (var item in elementList)
            {
                element.Add(new ElementOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    ElementId = item.Id.ToString(),
                    Key = item.Key,
                    MenuId = item.MenuId.ToString(),
                    Name = item.Name,
                    Value = item.Value
                });
            }

            list.Add(new GetUserJurisdictionListOutputDto
            {
                ElementList = element,
                MenuList = menu,
                ModuleList = module
            });

            return list;
            #region 旧

            //var moduleList = await db.Modules.ToListAsync();

            //var list = new List<GetUserJurisdictionListOutputDto>();

            //var list1 = new List<ModuleOutputDto>();

            //var list2 = new List<MenuOutputDto>();

            //var list3 = new List<ElementOutputDto>();

            //foreach (var item in moduleList)
            //{
            //    list2 = new List<MenuOutputDto>();

            //    var menuList = await db.Menus.Where(m => m.ModuleId == item.Id).ToListAsync();

            //    foreach (var item1 in menuList)
            //    {
            //        list3 = new List<ElementOutputDto>();

            //        var elementList = await db.Elements.Where(m => m.MenuId == item1.Id).ToListAsync();

            //        foreach (var item2 in elementList)
            //        {
            //            list3.Add(new ElementOutputDto
            //            {
            //                DisplayOrder = item2.DisplayOrder,
            //                ElementId = item2.Id.ToString(),
            //                Key = item2.Key,
            //                MenuId = item2.MenuId.ToString(),
            //                Name = item2.Name,
            //                Value = item2.Value
            //            });
            //        }

            //        list2.Add(new MenuOutputDto
            //        {
            //            MenuId = item1.Id.ToString(),
            //            DisplayOrder = item1.DisplayOrder,
            //            Element = list3,
            //            Key = item1.Key,
            //            ModuleId = item1.ModuleId.ToString(),
            //            Title = item1.Title,
            //            Url = item1.Url
            //        });

            //    }

            //    list1.Add(new ModuleOutputDto
            //    {
            //        ModuleId = item.Id.ToString(),
            //        DisplayOrder = item.DisplayOrder,
            //        Key = item.Key,
            //        Menu = list2,
            //        Name = item.Name
            //    });

            //}

            //list.Add(new GetUserJurisdictionListOutputDto
            //{
            //    Module = list1
            //});

            //return list;

            #endregion

        }

        /// <summary>
        /// list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetModuleListOutputDto>> GetModuleList()
        {
            var list = new List<GetModuleListOutputDto>();

            var moduleList = await db.Modules.ToListAsync();

            var menuList = await db.Menus.ToListAsync();

            var elementList = await db.Elements.ToListAsync();

            foreach (var item in moduleList)
            {
                list.Add(new GetModuleListOutputDto
                {
                    Name = item.Name,
                    Key = item.Key,
                    DisplayOrder = item.DisplayOrder,
                    ModuleId = item.Id.ToString()
                });
            }

            foreach (var item in menuList)
            {
                list.Add(new GetModuleListOutputDto
                {
                    MenuId = item.Id.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    ModuleId = item.ModuleId.ToString(),
                    Title = item.Title,
                    Url = item.Url
                });
            }

            foreach (var item in elementList)
            {
                list.Add(new GetModuleListOutputDto
                {
                    ElementId = item.Id.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    MenuId = item.MenuId.ToString(),
                    Name = item.Name,
                    Value = item.Value
                });
            }

            return list;
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserJurisdictionListOutputDto>> GetUserJurisdictionList(GetUserJurisdictionListInputDto input)
        {
            var list = new List<GetUserJurisdictionListOutputDto>();

            var moduleList = await db.UserModules.Include(m => m.Module).Where(m => m.UserTypeId == input.UserTypeId).Select(m => m.Module).ToListAsync();

            var menuList = await db.UserMenus.Include(m => m.Menu).Where(m => m.UserTypeId == input.UserTypeId).Select(m => m.Menu).ToListAsync();

            var elementList = await db.UserElements.Include(m => m.Element).Where(m => m.UserTypeId == input.UserTypeId).Select(m => m.Element).ToListAsync();

            var module = new List<ModuleOutputDto>();

            var menu = new List<MenuOutputDto>();

            var element = new List<ElementOutputDto>();

            foreach (var item in moduleList)
            {
                module.Add(new ModuleOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    ModuleId = item.Id.ToString(),
                    Name = item.Name
                });
            }

            foreach (var item in menuList)
            {
                menu.Add(new MenuOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    MenuId = item.Id.ToString(),
                    ModuleId = item.ModuleId.ToString(),
                    Title = item.Title,
                    Url = item.Url
                });
            }

            foreach (var item in elementList)
            {
                element.Add(new ElementOutputDto
                {
                    DisplayOrder = item.DisplayOrder,
                    ElementId = item.Id.ToString(),
                    Key = item.Key,
                    MenuId = item.MenuId.ToString(),
                    Name = item.Name,
                    Value = item.Value
                });
            }

            list.Add(new GetUserJurisdictionListOutputDto
            {
                UserTypeId = input.UserTypeId,
                ElementList = element,
                MenuList = menu,
                ModuleList = module
            });

            //list.Add(new GetUserJurisdictionListOutputDto()
            //{
            //    UserTypeId = input.UserTypeId,
            //    ElementList = elementList,
            //    MenuList = menuList,
            //    ModuleList = moduleList
            //});

            #region 旧
            //var list1 = new List<ModuleOutputDto>();

            //var list2 = new List<MenuOutputDto>();

            //var list3 = new List<ElementOutputDto>();

            //foreach (var item in moduleList)
            //{
            //    list2.Clear();
            //    list3.Clear();

            //    var menuList = await db.UserMenus.Include(m => m.Menu).Where(m => m.UserTypeId == input.UserTypeId && m.Menu.ModuleId == item.Id).Select(m => m.Menu).ToListAsync();

            //    foreach (var item1 in menuList)
            //    {

            //        var elementList = await db.UserElements.Include(m => m.Element).Where(m => m.UserTypeId == input.UserTypeId).Select(m => m.Element).ToListAsync();

            //        foreach (var item2 in elementList)
            //        {
            //            list3.Add(new ElementOutputDto
            //            {
            //                DisplayOrder = item2.DisplayOrder,
            //                ElementId = item2.Id.ToString(),
            //                Key = item2.Key,
            //                MenuId = item2.MenuId.ToString(),
            //                Name = item2.Name,
            //                Value = item2.Value
            //            });
            //        }

            //        list2.Add(new MenuOutputDto
            //        {
            //            MenuId = item1.Id.ToString(),
            //            DisplayOrder = item1.DisplayOrder,
            //            Element = list3,
            //            Key = item1.Key,
            //            ModuleId = item1.ModuleId.ToString(),
            //            Title = item1.Title,
            //            Url = item1.Url
            //        });

            //    }

            //    list1.Add(new ModuleOutputDto
            //    {
            //        ModuleId = item.Id.ToString(),
            //        DisplayOrder = item.DisplayOrder,
            //        Key = item.Key,
            //        Menu = list2,
            //        Name = item.Name
            //    });

            //}

            //list.Add(new GetUserJurisdictionListOutputDto
            //{
            //    Module = list1,
            //    UserTypeId = input.UserTypeId
            //});
            #endregion



            return list;
        }

        /// <summary>
        /// 获取用户模块权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserModuleListOutputDto>> GetUserModuleByUserTypeId(GetUserJurisdictionListInputDto input)
        {
            var list = new List<GetUserModuleListOutputDto>();

            #region 旧
            var list1 = new List<ModuleOutputDto>();

            var list2 = new List<MenuOutputDto>();

            var list3 = new List<ElementOutputDto>();

            var moduleList = await db.UserModules.Include(m => m.Module).Where(m => m.UserTypeId == input.UserTypeId).Select(m => m.Module).OrderBy(m=>m.DisplayOrder).ToListAsync();

            foreach (var item in moduleList)
            {
                list2 = new List<MenuOutputDto>();
                

                var menuList = await db.UserMenus.Include(m => m.Menu).Where(m => m.UserTypeId == input.UserTypeId && m.Menu.ModuleId == item.Id).Select(m => m.Menu).OrderBy(m => m.DisplayOrder).ToListAsync();

                foreach (var item1 in menuList)
                {
                    list3 = new List<ElementOutputDto>();

                    var elementList = await db.UserElements.Include(m => m.Element).Where(m => m.UserTypeId == input.UserTypeId && m.Element.MenuId == item1.Id).Select(m => m.Element).OrderBy(m => m.DisplayOrder).ToListAsync();

                    foreach (var item2 in elementList)
                    {
                        list3.Add(new ElementOutputDto
                        {
                            DisplayOrder = item2.DisplayOrder,
                            ElementId = item2.Id.ToString(),
                            Key = item2.Key,
                            MenuId = item2.MenuId.ToString(),
                            Name = item2.Name,
                            Value = item2.Value
                        });
                    }

                    list2.Add(new MenuOutputDto
                    {
                        MenuId = item1.Id.ToString(),
                        DisplayOrder = item1.DisplayOrder,
                        Element = list3,
                        Key = item1.Key,
                        ModuleId = item1.ModuleId.ToString(),
                        Title = item1.Title,
                        Url = item1.Url
                    });

                }

                list1.Add(new ModuleOutputDto
                {
                    ModuleId = item.Id.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    Menu = list2,
                    Name = item.Name
                });

            }

            list.Add(new GetUserModuleListOutputDto
            {
                Module = list1,
                UserTypeId = input.UserTypeId
            });
            #endregion

            return list;
        }

        /// <summary>
        /// 获取用户模块权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserModuleListOutputDto>> GetUserModule()
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var usertypeId = token.UserTypeId;

            var list = new List<GetUserModuleListOutputDto>();

            var moduleList = await db.UserModules.Include(m => m.Module).Where(m => usertypeId.Contains(m.UserTypeId)).GroupBy(m=>m.Module.Name).Select(m=>m.FirstOrDefault()).Select(m => m.Module).OrderBy(m => m.DisplayOrder).ToListAsync();

            var list1 = new List<ModuleOutputDto>();

            var list2 = new List<MenuOutputDto>();

            var list3 = new List<ElementOutputDto>();

            foreach (var item in moduleList)
            {
                list2 = new List<MenuOutputDto>();


                var menuList = await db.UserMenus.Include(m => m.Menu).Where(m => usertypeId.Contains(m.UserTypeId) && m.Menu.ModuleId == item.Id).GroupBy(m=>m.Menu.Title).Select(m=>m.FirstOrDefault()).Select(m => m.Menu).OrderBy(m => m.DisplayOrder).ToListAsync();

                foreach (var item1 in menuList)
                {
                    list3 = new List<ElementOutputDto>();

                    var elementList = await db.UserElements.Include(m => m.Element).Where(m => usertypeId.Contains(m.UserTypeId) && m.Element.MenuId == item1.Id).GroupBy(m=>m.Element.Name).Select(m => m.FirstOrDefault()).Select(m=>m.Element).OrderBy(m=>m.DisplayOrder).ToListAsync();

                    foreach (var item2 in elementList)
                    {
                        list3.Add(new ElementOutputDto
                        {
                            DisplayOrder = item2.DisplayOrder,
                            ElementId = item2.Id.ToString(),
                            Key = item2.Key,
                            MenuId = item2.MenuId.ToString(),
                            Name = item2.Name,
                            Value = item2.Value
                        });
                    }

                    list2.Add(new MenuOutputDto
                    {
                        MenuId = item1.Id.ToString(),
                        DisplayOrder = item1.DisplayOrder,
                        Element = list3,
                        Key = item1.Key,
                        ModuleId = item1.ModuleId.ToString(),
                        Title = item1.Title,
                        Url = item1.Url
                    });

                }

                list1.Add(new ModuleOutputDto
                {
                    ModuleId = item.Id.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    Menu = list2,
                    Name = item.Name
                });

            }

            list.Add(new GetUserModuleListOutputDto
            {
                Module = list1
            });

            return list;
        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetUserModuleListOutputDto>> GetUserModuleList()
        {
            var token = ((UserIdentity)User.Identity).Token;

            var list = new List<GetUserModuleListOutputDto>();

            #region 旧
            var list1 = new List<ModuleOutputDto>();

            var list2 = new List<MenuOutputDto>();

            var list3 = new List<ElementOutputDto>();

            var moduleList = await db.Modules.OrderBy(m => m.DisplayOrder).ToListAsync();

            foreach (var item in moduleList)
            {
                list2 = new List<MenuOutputDto>();

                var menuList = await db.Menus.Where(m=>m.ModuleId == item.Id).OrderBy(m => m.DisplayOrder).ToListAsync();

                foreach (var item1 in menuList)
                {
                    //if (token.UserTypeKey != "admin")
                    //{
                    //    if (item1.Title == "权限分配")
                    //    {
                    //        continue;
                    //    }
                    //}

                    list3 = new List<ElementOutputDto>();

                    var elementList = await db.Elements.Where(m=>m.MenuId == item1.Id).OrderBy(m => m.DisplayOrder).ToListAsync();

                    foreach (var item2 in elementList)
                    {
                        list3.Add(new ElementOutputDto
                        {
                            DisplayOrder = item2.DisplayOrder,
                            ElementId = item2.Id.ToString(),
                            Key = item2.Key,
                            MenuId = item2.MenuId.ToString(),
                            Name = item2.Name,
                            Value = item2.Value
                        });
                    }

                    list2.Add(new MenuOutputDto
                    {
                        MenuId = item1.Id.ToString(),
                        DisplayOrder = item1.DisplayOrder,
                        Element = list3,
                        Key = item1.Key,
                        ModuleId = item1.ModuleId.ToString(),
                        Title = item1.Title,
                        Url = item1.Url
                    });

                }

                list1.Add(new ModuleOutputDto
                {
                    ModuleId = item.Id.ToString(),
                    DisplayOrder = item.DisplayOrder,
                    Key = item.Key,
                    Menu = list2,
                    Name = item.Name
                });

            }

            list.Add(new GetUserModuleListOutputDto
            {
                Module = list1
            });
            #endregion

            return list;
        }

        /// <summary>
        ///Mzy_获取菜单
        /// </summary>
        /// <returns></returns>


        //[HttpPost]
        //public async Task<IHttpActionResult> GetMenus()
        //{

        //    var userId = ((UserIdentity)User.Identity).UserId;

        //    var result = db.User_User_Types.Where(p => p.u_Id == userId).ToList();
        //    return Json(result);

            
                        




            

        //}
    }
}
