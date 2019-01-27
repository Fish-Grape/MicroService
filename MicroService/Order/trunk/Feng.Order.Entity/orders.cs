using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Entity
{
    /// <summary>
    /// 订单表
    /// </summary>
    [SugarTable("orders")]
    public class orders
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        [SugarColumn(IsPrimaryKey =true)]
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
        /// 真实用户名称
        /// </summary>
        public string user_real_name { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int is_disable { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int order_status_id { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int pay_status_id { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 购买方式
        /// </summary>
        public int buy_method { get; set; }
        /// <summary>
        /// 购买时长
        /// </summary>
        public int buy_period { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public string pay_status { get; set; }
        /// <summary>
        /// 订单审核人id
        /// </summary>
        public int approve_by_id { get; set; }
        /// <summary>
        /// 订单审核人
        /// </summary>
        public string approve_by { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int pay_method_id { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string pay_method { get; set; }
        /// <summary>
        /// 成交价
        /// </summary>
        public decimal final_price { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal original_price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_time { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? update_time { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? pay_time { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? approve_time { get; set; }
        /// <summary>
        /// 交易完成时间
        /// </summary>
        public DateTime? end_time { get; set; }
        /// <summary>
        /// 交易关闭时间
        /// </summary>
        public DateTime? close_time { get; set; }
    }
}
