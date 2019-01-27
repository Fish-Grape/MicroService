using SqlSugar;
using System;

namespace Feng.CMS.Entity
{
    /// <summary>
    /// 广告位置表
    /// </summary>
    [SugarTable("news")]
    public class news
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
        /// 归属栏目编号
        /// </summary>
        public int menuid { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool ispub { get; set; }
        /// <summary>
        /// 推荐类型,热门,置顶
        /// </summary>
        public int recommend_type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string imgurl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 简述
        /// </summary>
        public string shor_description { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? addtime { get; set; }
        /// <summary>
        /// 访问数量
        /// </summary>
        public int vister_count { get; set; }
    }
}
