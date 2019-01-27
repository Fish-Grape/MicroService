using Feng.Pay.Core;
using System.ComponentModel.DataAnnotations;

namespace Feng.Pay.Unionpay.Domain
{
    public class AppPayModel : BasePayModel
    {
        /// <summary>
        /// 订单描述
        /// </summary>
        [ReName(Name = "orderDesc")]
        [StringLength(32, ErrorMessage = "订单描述最大长度为32位")]
        public string Body { get; set; }

        /// <summary>
        /// 支付卡类型
        /// </summary>
        public string PayCardType { get; set; }
    }
}
