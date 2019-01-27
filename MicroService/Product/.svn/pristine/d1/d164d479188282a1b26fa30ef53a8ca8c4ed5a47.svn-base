using System;
using System.Collections.Generic;

namespace Feng.Product.Model
{
    public class AttributeGroupModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
    }
    public class AddAttributeGroupModel
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 所属分类编号
        /// </summary>
        public string categoryId { get; set; }
    }



    public class AttributeModel : AddAttributeModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }
    }
    public class AddAttributeModel {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 展示类型(0文字,1图片)
        /// </summary>
        public int showType { get; set; }
        /// <summary>
        /// 所属分类编号
        /// </summary>
        public string categoryId { get; set; }
        /// <summary>
        /// 所属分组编号
        /// </summary>
        public string groupId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 是否筛选属性
        /// </summary>
        public bool isFilter { get; set; }
        /// <summary>
        /// 否销售属性
        /// </summary>
        public bool isSales { get; set; }
        /// <summary>
        /// 是否搜索字段
        /// </summary>
        public bool isSearch { get; set; }
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool isMulti { get; set; }
        /// <summary>
        /// 是否必须
        /// </summary>
        public bool isMust { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
    }




    public class AttributeValueModel : AddAttributeValueModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string productId { get; set; }
    }
    public class AddAttributeValueModel {
        /// <summary>
        /// 值
        /// </summary>
        public string valueName { get; set; }
        /// <summary>
        /// 值图片
        /// </summary>
        public string valueImage { get; set; }
        /// <summary>
        /// 所属属性编号
        /// </summary>
        public string attributeId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
    }
}
