using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Entity
{
    /// <summary>
    /// 订单附加属性表
    /// </summary>
    [SugarTable("o_userinfo")]
    public class o_userinfo
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,ColumnName ="id")]
        public string o_userinfo_id { get; set; }
        /// <summary>
        /// 应用Key
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string receive_name { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 收货省名称
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 收货市名称
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 收货区名称
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public int zip { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public int region_id { get; set; }
    }
}
