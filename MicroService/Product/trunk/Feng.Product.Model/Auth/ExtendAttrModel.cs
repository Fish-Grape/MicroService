using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.Model.Background
{
    public class ExtendAttrModel : AddExtendAttrModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }
    }

    public class AddExtendAttrModel
    {
        /// <summary>
        /// 值
        /// </summary>
        public string ExtendName { get; set; }
        /// <summary>
        /// 附加属性值类型
        /// </summary>
        public ExtendType ExType { get; set; }
        /// <summary>
        /// 关联id
        /// </summary>
        public string ExtendId { get; set; }
        /// <summary>
        /// 附加价格
        /// </summary>
        public decimal AddPrice { get; set; }
    }
}
