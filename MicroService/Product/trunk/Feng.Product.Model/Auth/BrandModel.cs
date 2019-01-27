using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.Model
{
    public class BrandModel:AddBrandModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addTime { get; set; }
    }

    public class AddBrandModel {
        /// <summary>
        /// 品牌中文名称
        /// </summary>
        public string cnName { get; set; }
        /// <summary>
        /// 品牌英文名称
        /// </summary>
        public string enName { get; set; }
        /// <summary>
        /// 品牌描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 品牌Logo
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 官方Url
        /// </summary>
        public string siteUrl { get; set; }
    }
    
}
