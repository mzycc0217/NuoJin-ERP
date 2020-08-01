using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Db.Z_Menus;
using XiAnOuDeERP.Models.Dto.Menu.In;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.Login
{
    [AppAuthentication]
    public class LoginController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuInDtos"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> AddMenu(MenuInDto menuInDtos)
        {
            Z_Menu z_Menu =await Task.Run(()=> new Z_Menu
            {
                Id = IdentityManager.NewId(),
                name = menuInDtos.name,
                url=menuInDtos.url,
                icon = menuInDtos.icon,
                pid = menuInDtos.pid
            });
            db.Z_Menus.Add(z_Menu);


            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new {code = 200, msg = "添加成功"});
            }

            return Json(new { code = 300, msg = "添加失败" });

        }


        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuInDtos"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> RemoveMenu(MenuInDto menuInDtos)
        {
            Z_Menu z_Menu = new Z_Menu {Id = menuInDtos.Id};
            z_Menu.name = menuInDtos.name;
            z_Menu.Order = menuInDtos.Order;
            z_Menu.pid = menuInDtos.pid;
            z_Menu.icon = menuInDtos.icon;
            z_Menu.url = menuInDtos.url;
            





            if (await db.SaveChangesAsync() > 0)
            {
                return Json(new { code = 200, msg = "添加成功" });
            }

            return Json(new { code = 300, msg = "添加失败" });

        }

    }
}
