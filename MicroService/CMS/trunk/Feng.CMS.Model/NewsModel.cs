using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.CMS.Model
{
    public class NewsModel:AddNewsModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 访问数量
        /// </summary>
        public int visterCount { get; set; }
    }

    public class AddNewsModel {
        /// <summary>
        /// 归属栏目编号
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool isPublish { get; set; }
        /// <summary>
        /// 推荐类型,热门,置顶
        /// </summary>
        public int recommendType { get; set; }
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
        public string imageUrl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
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
        public string shorDescription { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string description { get; set; }

    }

    public class ReturnNewsModel {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
    }

    public class QueryNewsListModel {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string menu { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool isPublish { get; set; }
        /// <summary>
        /// 访问数量
        /// </summary>
        public int visterCount { get; set; }
    }
    
    public class NewsCategoryModel: AddNewsCategoryModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        
    }

    public class AddNewsCategoryModel {
        /// <summary>
        /// 父编号,默认0
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string visitUrl { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
    }

    public class ReturnNewsCategoryModel {
        public int id { get; set; }
    }

    public class NewsCateModel
    {

        /// <summary>
        /// title
        /// </summary>
        public string label { get; set; }
        
        /// <summary>
        /// id
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string visitUrl { get; set; }

        public List<NewsCateModel> children { get; set; }
    }


    
}
