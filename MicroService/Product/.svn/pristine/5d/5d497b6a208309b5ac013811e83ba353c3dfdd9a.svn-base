using SqlSugar;
using System;

namespace Feng.Product.Entity
{
    [SugarTable("category")]
    public class category
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父类
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }


    }
}
