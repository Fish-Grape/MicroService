using SqlSugar;
using System;

namespace Feng.Product.Entity
{
    [SugarTable("product")]
    public class product
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
        /// 产品编码
        /// </summary>
        public string product_code { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 产品简述
        /// </summary>
        public string short_desc { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string categoryid { get; set; }
        /// <summary>
        /// 品牌编号
        /// </summary>
        public int brandid { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public bool is_publish { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal sales_price { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal market_price { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 毛重 公斤
        /// </summary>
        public float weight { get; set; }
        /// <summary>
        /// 长 mm
        /// </summary>
        public int xxx { get; set; }
        /// <summary>
        /// 宽 mm
        /// </summary>
        public int yyy { get; set; }
        /// <summary>
        /// 高 mm
        /// </summary>
        public int zzz { get; set; }
        /// <summary>
        /// 密度 g/cm3
        /// </summary>
        public double density { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }
    }
    
}
