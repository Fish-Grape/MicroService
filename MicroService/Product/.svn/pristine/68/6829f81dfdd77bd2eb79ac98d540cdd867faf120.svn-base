using SqlSugar;

namespace Feng.Product.Entity
{
    [SugarTable("attribute_values")]
    public class attribute_values
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
        /// 值
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 值图片
        /// </summary>
        public string image_url { get; set; }

        /// <summary>
        /// 所属属性编号
        /// </summary>
        public string attributeid { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
    }
}
