using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.Entity
{
    [SugarTable("extend_attr")]
    public class extend_attr
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 附加类型：1:分类,2:属性,3:属性值
        /// </summary>
        public int ex_type { get; set; }
        /// <summary>
        /// 关联id
        /// </summary>
        public string ex_id { get; set; }
        /// <summary>
        /// 附加属性值
        /// </summary>
        public string ex_name { get; set; }
        /// <summary>
        /// 附加价格
        /// </summary>
        public decimal add_price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime ? create_time { get; set; }
    }
}
