using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst
{
    /// <summary>
    /// 仓库表
    /// </summary>
    [Table("Entrepot")]
    public class Entrepot : EntityBase
    {

        /// <summary>
        /// 仓库负责人
        /// </summary>
        public long User_id { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(20)]
        public string EntrepotName { get; set; } 
        
        /// <summary>
        /// 仓库描述
        /// </summary>
        [StringLength(225)]
        public string EntrepotDes { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public int del_Enpto { get; set; }
        /// <summary>
        /// 仓库地点
        /// </summary>
        [StringLength(225)]
        public string EntrepotAddress { get; set; }
        public virtual UserDetails userDetails { get; set; }
    }
   


}