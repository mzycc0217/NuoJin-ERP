using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WarehouseManagements
{
    /// <summary>
    /// 库存绑定库管人输入类
    /// </summary>
    public class StorageRoomAddWarehouseKeeperInputDto
    {
        /// <summary>
        /// 库存Id
        /// </summary>
        public long StorageRoomId { get; set; }

        /// <summary>
        /// 库管人Id
        /// </summary>
        public long WarehouseKeeperId { get; set; }
    }
}