using SqlSugar;
using System;

namespace Feng.CMS.Entity
{
    /// <summary>
    /// 广告位置表
    /// </summary>
    [SugarTable("news_menu")]
    public class news_menu
    {
        /// <summary>
        /// 自增主键编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        /// <summary>
        /// 应用key
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 父编号,默认0
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string imgurl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
    }
}
