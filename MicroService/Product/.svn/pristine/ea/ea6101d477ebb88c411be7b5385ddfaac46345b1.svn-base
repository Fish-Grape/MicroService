using SqlSugar;

namespace Feng.Product.Entity
{
    [SugarTable("attribute")]
    public class attribute
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
        /// 所属分类编号
        /// </summary>
        public string categoryid { get; set; }

        /// <summary>
        /// 所属分组编号
        /// </summary>
        public string groupid { get; set; }
        /// <summary>
        /// 展示类型(0文字,1图片)
        /// </summary>
        public int show_type { get; set; }
        /// <summary>
        /// 是否筛选属性
        /// </summary>
        public bool is_filter { get; set; }
        /// <summary>
        /// 否销售属性
        /// </summary>
        public bool is_sales { get; set; }
        /// <summary>
        /// 是否搜索字段
        /// </summary>
        public bool is_search { get; set; }
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool is_multi { get; set; }
        /// <summary>
        /// 是否必须
        /// </summary>
        public bool is_must { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
    }
}
