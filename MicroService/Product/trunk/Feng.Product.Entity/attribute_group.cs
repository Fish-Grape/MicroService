using SqlSugar;
using System;

namespace Feng.Product.Entity
{
    [SugarTable("attribute_group")]
    public class attribute_group
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
        /// 分组名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 所属分类编号
        /// </summary>
        public string categoryid { get; set; }


    }
}
