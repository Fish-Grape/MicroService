using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.Entity
{
    [SugarTable("shopping_cart_detail")]
    public class shopping_cart_detail
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 购物车id
        /// </summary>
        public string shopping_cart_id { get; set; }
        /// <summary>
        /// 附加属性id
        /// </summary>
        public string extend_attr_id { get; set; }
        /// <summary>
        /// 附加属性名称
        /// </summary>
        public string extend_attr_name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime ? create_time { get; set; }
    }
}
