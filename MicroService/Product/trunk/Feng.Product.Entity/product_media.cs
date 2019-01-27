using SqlSugar;

namespace Feng.Product.Entity
{
    [SugarTable("product_media")]
    public class product_media
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
        public int id { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string platkey { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string productid { get; set; }

        /// <summary>
        /// 媒体类型(0图片,1视频)
        /// </summary>
        public int media_type { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string media_url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
    }
}
