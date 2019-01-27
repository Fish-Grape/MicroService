using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.Model
{
    /// <summary>
    /// 订单详情表
    /// </summary>
    public class OrdersDetailWebModel : AddOrdersDetailWebModel
    {
        /// <summary>
        /// 非自增主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? update_time { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_time { get; set; }
    }
    /// <summary>
    /// 订单详情信息
    /// </summary>
    public class AddOrdersDetailWebModel
    {
        public AddOrdersDetailWebModel()
        {
            addO_Products = new List<AddO_ProductWebModel>();
        }
        public List<AddO_ProductWebModel> addO_Products { get; set; }
    }

    public class AddO_ProductWebModel
    {
        public AddO_ProductWebModel()
        {
            addO_Ext_AttrfoWebs = new List<AddO_Ext_AttrfoWebModel>();
        }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 产品成交价
        /// </summary>
        public decimal final_price { get; set; }
        /// <summary>
        /// 产品原价
        /// </summary>
        public decimal original_price { get; set; }
        /// <summary>
        /// 产品图片地址
        /// </summary>
        public string product_image { get; set; }
        /// <summary>
        /// 产品简述
        /// </summary>
        public string short_desc { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 产品属性
        /// </summary>
        public string attr_json { get; set; }
        /// <summary>
        /// sku字符串
        /// </summary>
        public string sku_json { get; set; }
        /// <summary>
        /// skuID
        /// </summary>
        public string skuid { get; set; }

        public List<AddO_Ext_AttrfoWebModel> addO_Ext_AttrfoWebs { get; set; }
    }

    public class AddO_Ext_AttrfoWebModel
    {
        /// <summary>
        /// skuID
        /// </summary>
        public string skuid { get; set; }
        /// <summary>
        /// 附加属性值id
        /// </summary>
        public string extend_attr_id { get; set; }
        /// <summary>
        /// 附加属性值名称
        /// </summary>
        public string extend_attr_name { get; set; }
    }

    /// <summary>
    /// 修改订单规格
    /// </summary>
    public class ModifySkuModel 
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string order_id { get; set; }
        
        /// <summary>
        /// 扩展属性
        /// </summary>
        public List<AddO_Ext_AttrfoWebModel> addO_Ext_Attrfos { get; set; }
    }
}