using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Entity
{
    /// <summary>
    /// 订单产品表
    /// </summary>
    [SugarTable("o_product")]
    public class o_product
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
        /// 产品分类
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string product_code { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 产品成交价
        /// </summary>
        public decimal final_price { get; set; }
        /// <summary>
        /// 产品原价
        /// </summary>
        public decimal original_price { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 产品图片地址
        /// </summary>
        public string product_image { get; set; }
        /// <summary>
        /// 产品简述
        /// </summary>
        public string short_desc { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 产品属性
        /// </summary>
        public string attr_json { get; set; }
        /// <summary>
        /// sku字符串
        /// </summary>
        public string sku_json { get; set; }
        /// <summary>
        /// skuID
        /// </summary>
        public string skuid { get; set; }
    }
}
