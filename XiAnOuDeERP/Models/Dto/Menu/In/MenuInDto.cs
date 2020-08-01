using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.Menu.In
{
    public class MenuInDto
    {

        /// <summary>
        /// 菜单id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 菜单名成
        /// </summary>
     //   [StringLength(10)]
        public string name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
       //  [StringLength(20)]
        public string icon { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
      //  [StringLength(50)]
        public string url { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public int Order { get; set; }
    }
}