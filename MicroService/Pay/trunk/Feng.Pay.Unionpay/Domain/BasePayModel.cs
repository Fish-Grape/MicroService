﻿using Feng.Pay.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Feng.Pay.Unionpay.Domain
{
    public class BasePayModel : BaseModel
    {
        /// <summary>
        /// 交易币种,默认156
        /// </summary>
        public int CurrencyCode { get; set; } = 156;

        /// <summary>
        /// 交易金额,单位分
        /// </summary>
        [Required(ErrorMessage = "请设置交易金额")]
        [ReName(Name = "txnAmt")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        [Required(ErrorMessage = "请设置渠道类型")]
        public string ChannelType { get; protected set; } = "08";

        /// <summary>
        /// 商户订单号，不应含“-”或“_”
        /// </summary>
        [Required(ErrorMessage = "请设置商户订单号")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "商户订单号最小长度为8位,最大长度为40位")]
        public string OrderId { get; set; }

        /// <summary>
        /// 1、当账号类型为02-存折时需填写
        /// 2、在前台类交易时填写默认银行代码，支持直接跳转到网银。
        /// 银行简码列表参考附录：C.1、C.2， 其中C.2银行列表仅支持借记卡
        /// </summary>
        public string IssInsNo { get; set; }

        /// <summary>
        /// 分期付款信息域
        /// 相关子域见注 10。 格式如下：{子域名1=值&子域名2=值&子域名3=值}
        /// </summary>
        [StringLength(1024, ErrorMessage = "分期付款信息域最大长度为1024位")]
        public string InstalTransInfo { get; set; }

        /// <summary>
        /// 银行卡验证信息及身份信息
        /// 该域需整体做 Base64 编码。 
        /// 所有子域需用“有子域包含，子域间以“含，符号链接。 
        /// 格式如下：{子域名 1=值&子域名 2=值&子域名 3=值} 
        /// 各子域取值见注 
        /// 1。 借记卡可上送姓名、手机号、证件类型、证件号码；
        /// 贷记卡可上送姓名、手机号、证件类型、证件号码、有效 期、CVN2。
        /// 前台交易可以由银联页面采集，无需商户上送，后台交 易必须由商户上送。 
        /// 前台交易支持商户上送并返显的要素包含手机号，姓 名，证件号。
        /// </summary>
        [StringLength(1024, ErrorMessage = "银行卡验证信息及身份信息最大长度为1024位")]
        public string CustomerInfo { get; set; }

        /// <summary>
        /// 交易账号。
        /// 1、 后台类消费交易时上送全卡号或卡号后4位 
        /// 2、 跨行收单且收单机构收集银行卡信息时上送、 
        /// 3、前台类交易可通过配置后返回，卡号可选上送
        /// </summary>
        [StringLength(1024, ErrorMessage = "交易账号最大长度为1024位")]
        public string AccNo { get; set; }

        /// <summary>
        /// 保留域
        /// </summary>
        [StringLength(2048, ErrorMessage = "保留域最大长度为2048位")]
        public string Reserved { get; set; }

        /// <summary>
        /// 分账域
        /// </summary>
        [StringLength(512, ErrorMessage = "分账域最大长度为512位")]
        public string AccSplitData { get; set; }

        /// <summary>
        /// 风控信息域
        /// </summary>
        [StringLength(2048, ErrorMessage = "风控信息域最大长度为2048位")]
        public string RiskRateInfo { get; set; }

        /// <summary>
        /// 控制规则
        /// </summary>
        public string CtrlRule { get; set; }

        /// <summary>
        /// 默认支付方式
        /// </summary>
        public string DefaultPayType { get; set; }

        /// <summary>
        /// 商户自定义保留域，交易应答时会原样返回
        /// </summary>
        [StringLength(1024, ErrorMessage = "商户自定义保留域最大长度为1024位")]
        public string ReqReserved { get; set; }

        /// <summary>
        /// 支付超时时间,订单支付超时时间，超过此时间用户支付成功的交易，
        /// 不通知商户，系统自动退款，大约 5 个工作日金额返还 到用户账户
        /// </summary>
        public string PayTimeout { get; set; } = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");
    }
}
