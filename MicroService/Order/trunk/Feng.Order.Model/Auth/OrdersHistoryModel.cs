using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Model.History
{
    /// <summary>
    /// 订单表
    /// </summary>
    class OrdersHistoryModel
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 应用Key
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 订单code
        /// </summary>
        public string order_code { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 下单用户
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public string pay_status { get; set; }
        /// <summary>
        /// 订单审核人
        /// </summary>
        public string approve_by { get; set; }
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
