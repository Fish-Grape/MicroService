using SqlSugar;
using System;

namespace Feng.Product.Entity
{
    [SugarTable("shopping_cart")]
    public class shopping_cart
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 应用Key
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 下单用户
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool is_product_exists { get; set; }
        /// <summary>
        /// 商店编号
        /// </summary>
        public string shop_id { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string productid { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 购买方式
        /// </summary>
        public int buy_method { get; set; }
        /// <summary>
        /// 购买时长
        /// </summary>
        public int buy_period { get; set; }
        /// <summary>
        /// 规格id
        /// </summary>
        public string skuid { get; set; }
        /// <summary>
        /// 单品价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal total_price { get; set; }
        /// <summary>
        /// 单品名称
        /// </summary>
        public string good_name { get; set; }
        /// <summary>
        /// 审核类型
        /// </summary>
        public string valid_type { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime create_time { get; set; }
    }
}
