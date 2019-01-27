using System;
using System.Collections.Generic;

namespace Feng.Product.Model
{
    public class CategoryModel
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
        /// 图标
        /// </summary>
        public string categoryIcon { get; set; }
    }

    public class AddCategoryModel
    {
        /// <summary>
        /// 类目名称
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 父类编号,为空或0为一级
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string categoryIcon { get; set; }
    }

    public class ReturnListCategoryModel
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
}
