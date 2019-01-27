﻿using Feng.Pay.Core;
using Feng.Utils.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Feng.Pay.Qpay.Domain
{
    public class BarcodePayModel
    {
        /// <summary>
        /// 用户的付款码
        /// 18位字符串，开头两位为91。该字段由商户的扫码设备，从用户的手机QQ上读取，或者是店员输入
        /// </summary>
        [Required(ErrorMessage = "请设置授权码")]
        [StringLength(128, ErrorMessage = "授权码最大长度为128位")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 设备号
        /// 调用接口提交的终端设备号
        /// </summary>
        [Required(ErrorMessage = "请设置设备号")]
        [StringLength(32, ErrorMessage = "设备号最大长度为32位")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; } = Id.Guid();

        /// <summary>
        /// 商品简单描述，该字段请按照规范传递，具体请见参数规定
        /// </summary>
        [StringLength(128, ErrorMessage = "商品描述最大长度为128位")]
        [Required(ErrorMessage = "请设置商品描述")]
        public string Body { get; set; }

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        [StringLength(128, ErrorMessage = "附加数据最大长度为128位")]
        public string Attach { get; set; }

        /// <summary>
        /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。详见商户订单号
        /// </summary>
        [StringLength(32, ErrorMessage = "商户系统内部订单号最大长度为32位")]
        [Required(ErrorMessage = "请设置商户系统内部订单号")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 标价币种,符合ISO 4217标准的三位字母代码，默认人民币：CNY，详细列表请参见货币类型
        /// </summary>
        [ReName(Name = "fee_type")]
        [StringLength(16, ErrorMessage = "标价币种最大长度为16位")]
        public string AmountType { get; set; } = "CNY";

        /// <summary>
        /// 商户订单总金额，单位为分，只能为整数，详见交易金额
        /// </summary>
        [ReName(Name = "total_fee")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// QQ钱包活动标识
        /// 指定本单参与某个QQ钱包活动或活动档位的标识，包含两个标识：
        /// sale_tag --- 不同活动的匹配标志
        /// level_tag --- 同一活动不同优惠档位的标志，可不填。
        /// 格式如下（本字段参与签名）：promotion_tag=level_tag=xxx&sale_tag=xxx
        /// </summary>
        [StringLength(128, ErrorMessage = "QQ钱包活动标识最大长度为128位")]
        public string PromotionTag { get; set; }

        /// <summary>
        /// 支付方式限制,可以针对当前的交易，限制用户的支付方式，如：仅允许使用余额，或者是禁止使用信用卡。详情见支付方式限制
        /// </summary>
        [StringLength(32, ErrorMessage = "指定支付方式最大长度为32位")]
        public string LimitPay { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType => "MICROPAY";

        /// <summary>
        /// 用户IP
        /// </summary>
        [Required(ErrorMessage = "请设置用户IP")]
        [StringLength(16, ErrorMessage = "用户IP最大长度为16位")]
        public string SpbillCreateIp { get; set; } = Web.LocalIpAddress;
    }
}
