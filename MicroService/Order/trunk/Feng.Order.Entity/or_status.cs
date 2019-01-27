using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Entity
{
    /// <summary>
    /// 订单表
    /// </summary>
    [SugarTable("or_status")]
    public class or_status
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        /// <summary>
        /// 状态id
        /// </summary>
        public int status_id { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public string status_val { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        public string status_type { get; set; }
    }
}
