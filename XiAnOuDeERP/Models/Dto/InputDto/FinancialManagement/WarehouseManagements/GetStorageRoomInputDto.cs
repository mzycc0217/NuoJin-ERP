using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 获取库存输入类
    /// </summary>
    public class GetStorageRoomInputDto:InputBase
    {
        /// <summary>
        /// 库存Id
        /// </summary>
        public long? StorageRoomId { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public string RawMaterialName { get; set; }

        /// <summary>
        /// 库存类型
        /// </summary>
        public long? WarehousingTypeId { get; set; }

        /// <summary>
        /// 库管人Id
        /// </summary>
        public long? WarehouseKeeperId { get; set; }
    }
}