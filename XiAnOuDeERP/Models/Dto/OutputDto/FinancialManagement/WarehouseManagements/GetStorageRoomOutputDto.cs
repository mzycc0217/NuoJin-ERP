using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.OutputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 库存输出类
    /// </summary>
    public class GetStorageRoomOutputDto:OutputBase
    {
        /// <summary>
        /// 库存Id
        /// </summary>
        public string StorageRoomId { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public string RawMaterialId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// 库管人Id
        /// </summary>
        public string WarehouseKeeperId { get; set; }

        /// <summary>
        /// 原材料
        /// </summary>
        public RawMaterial RawMaterial { get; set; }

        /// <summary>
        /// 库管人
        /// </summary>
        public UserDetails WarehouseKeeper { get; set; }
    }
}