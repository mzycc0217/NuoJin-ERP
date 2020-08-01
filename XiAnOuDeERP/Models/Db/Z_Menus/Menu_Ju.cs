using System.ComponentModel.DataAnnotations.Schema;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Z_Menus
{
    /// <summary>
    /// 菜单权限对应表
    /// </summary>
    [Table("Menu_Ju")]
    public class Menu_Ju : EntityBase
    {
        /// <summary>
        /// 个人
        /// </summary>
        public long userType_Id{ get; set; }

        public virtual UserType UserType{ get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public long Menu_Id { get; set; }

        public virtual Z_Menu z_Menu { get; set; }

    }
}