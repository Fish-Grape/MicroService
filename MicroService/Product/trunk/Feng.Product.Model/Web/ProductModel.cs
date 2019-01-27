using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.Model
{
    #region 前台模型
    public class ProductWebModel
    {
        public ProductWebModel()
        {
            SalesAttrList = new List<ProductAttrWebModel>();
            NoSalesAttrList = new List<ProductAttrWebModel>();
            productSkus = new List<ProductSkuWebModel>();
            productMedias = new List<ProductMediaWebModel>();
        }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 销售属性信息
        /// </summary>
        public List<ProductAttrWebModel> SalesAttrList { get; set; }
        /// <summary>
        /// 非销售属性信息
        /// </summary>
        public List<ProductAttrWebModel> NoSalesAttrList { get; set; }
        /// <summary>
        /// 产品sku信息
        /// </summary>
        public List<ProductSkuWebModel> productSkus { get; set; }
        /// <summary>
        /// 产品媒体信息
        /// </summary>
        public List<ProductMediaWebModel> productMedias { get; set; }
    }
    public class ProductAttrWebModel
    {
        public ProductAttrWebModel()
        {
            AttributeValues = new List<AttrValueWebModel>();
        }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 属性编号
        /// </summary>
        public string AttributeId { get; set; }
        /// <summary>
        /// 是否必选
        /// </summary>
        public bool is_must { get; set; }
        /// <summary>
        /// 是否销售属性
        /// </summary>
        public bool is_sales { get; set; }
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool is_multi { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 属性值,多个用“,”分割
        /// </summary>
        public List<AttrValueWebModel> AttributeValues { get; set; }
    }
    /// <summary>
    /// 认证属性模型
    /// </summary>
    public class IdentifyAttrWebModel : ProductAttrWebModel
    {
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 周期,单位：天
        /// </summary>
        public int periods { get; set; }
    }

    public class QueryProductWebModel
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 父类编号,为空或0为一级
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public List<ProductWebModel> Products { get; set; }
        /// <summary>
        /// 子分类
        /// </summary>
        public List<QueryProductWebModel> Children { get; set; }
    }

    public class ProductSkuWebModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// SKU名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 属性字符串
        /// </summary>
        public string attrJson { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string barCode { get; set; }
        /// <summary>
        /// 周期,单位：天
        /// </summary>
        public int periods { get; set; }
    }

    public class ProductMediaWebModel
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// 媒体类型(0图片,1视频)
        /// </summary>
        public int mediaType { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string mediaUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int displayOrder { get; set; }
    }
    #endregion
}
