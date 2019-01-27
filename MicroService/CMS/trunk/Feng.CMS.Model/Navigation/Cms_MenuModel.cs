using System;
using System.Collections.Generic;

namespace Feng.CMS.Model.Navigation
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public class Cms_MenuModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 导航名
        /// </summary>
        public string label { get; set; }
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
        /// 分类
        /// </summary>
        public string classfy { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public List<Cms_MenuModel> children { get; set; }
    }
    public class ReturnCms_MenuModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
    }

    public class AddCms_MenuModel
    {
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
        /// 分类
        /// </summary>
        public string classfy { get; set; }
    }

    /// <summary>
    /// 修改
    /// </summary>
    public class ModifyCms_MenuModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
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
        /// 分类
        /// </summary>
        public string classfy { get; set; }
    }
}
