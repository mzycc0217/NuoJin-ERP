using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Z_Menus
{
    [Table("Z_Menu")]
    public class Z_Menu : EntityBase
    {

        /// <summary>
        /// 菜单名成
        /// </summary>
        public  string name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public  string icon { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public  string url { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public  long pid { get; set; }

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public  int Order { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Del_or { get; set; }
    }
}