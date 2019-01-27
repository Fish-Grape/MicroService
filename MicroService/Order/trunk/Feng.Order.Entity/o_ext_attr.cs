using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Entity
{
    /// <summary>
    /// 订单附加属性表
    /// </summary>
    [SugarTable("o_ext_attr")]
    public class o_ext_attr
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 应用Key
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// skuID
        /// </summary>
        public string skuid { get; set; }
        /// <summary>
        /// 附加属性值id
        /// </summary>
        public string extend_attr_id { get; set; }
        /// <summary>
        /// 附加属性值名称
        /// </summary>
        public string extend_attr_name { get; set; }
    }
}
