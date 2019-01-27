using SqlSugar;
using System;

namespace Feng.CMS.Entity
{
    /// <summary>
    /// 导航菜单表
    /// </summary>
    [SugarTable("cms_menu")]
    public class cms_menu
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
        /// 导航名
        /// </summary>
        public string menu_name { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string navigate_url { get; set; }
        /// <summary>
        /// 目标  0-当前页面打开  1-新页面
        /// </summary>
        public int target { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 父级code
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string classfy { get; set; }
    }
}

