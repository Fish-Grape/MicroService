using System;
using System.Collections.Generic;

namespace Feng.Product.Model
{
    #region 前台模型

    public class ReturnListCategoryWebModel
    {
        /// <summary>
        /// 类目编号
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string categoryIcon { get; set; }

        public List<ReturnListCategoryModel> children;
    }

    public class CategoryHomeModel
    {
        /// <summary>
        /// 类目编号
        /// </summary>
        public string categoryId { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 区域集合
        /// </summary>
        public List<AreaModel> aeras { get; set; }
    }

    public class AreaModel
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 类目编号
        /// </summary>
        public string categoryId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 产品集合
        /// </summary>
        public List<ProductHomeModel> products { get; set; }
    }

    public class ProductHomeModel {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 类目编号
        /// </summary>
        public string categoryId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public string AreaId { get; set; }
    }
    #endregion
}
