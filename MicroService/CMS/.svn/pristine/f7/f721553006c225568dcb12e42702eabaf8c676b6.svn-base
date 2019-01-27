using SqlSugar;
using System;

namespace Feng.CMS.Entity
{
    /// <summary>
    /// 广告内容表
    /// </summary>
    [SugarTable("advert")]
    public class advert
    {
        /// <summary>
        /// 自增主键编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        /// <summary>
        /// 应用Key
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 关联位置
        /// </summary>
        public string positionid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 类型,0图片,1视频,2文字
        /// </summary>
        public int ad_type { get; set; }
        /// <summary>
        /// 类型内容
        /// </summary>
        public string ad_content { get; set; }
        /// <summary>
        /// 访问URL
        /// </summary>
        public string visit_url { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begintime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endtime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string extfield1 { get; set; }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string extfield2 { get; set; }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string extfield3 { get; set; }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string extfield4 { get; set; }
    }
}

