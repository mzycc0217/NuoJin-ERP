using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.DeviceManagement
{
    public class DeviceInputDto
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public long DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 使用情况
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 出库Id
        /// </summary>
        public long OutOfStockId { get; set; }

        ///// <summary>
        ///// 技术说明
        ///// </summary>
        //public string TechnicalDescription { get; set; }

        ///// <summary>
        ///// 使用年限
        ///// </summary>
        //public double ServiceLife { get; set; }

        /// <summary>
        /// 是否报废
        /// </summary>
        public bool IsScrap { get; set; }
    }
}