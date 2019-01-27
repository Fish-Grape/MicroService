using SqlSugar;
using System;

namespace Feng.CMS.Entity
{
    /// <summary>
    /// 公告表
    /// </summary>
    [SugarTable("cms_notice")]
    public class cms_notice
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
        /// 公告标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int isdisable { get; set; }
    }
}

