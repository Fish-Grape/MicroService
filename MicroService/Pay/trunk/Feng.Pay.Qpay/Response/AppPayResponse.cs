﻿using Feng.Pay.Core;
using Feng.Pay.Core.Request;
using Feng.Utils;
using Feng.Utils.Helpers;
using System;

namespace Feng.Pay.Qpay.Response
{
    public class AppPayResponse : BaseResponse
    {
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// QQ钱包生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string PrepayId { get; set; }

        /// <summary>
        /// 用于唤起App的订单参数
        /// </summary>
        public string OrderInfo { get; set; }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
            if (ResultCode == "SUCCESS")
            {
                string data = $"appId={merchant.AppId}&bargainorId={merchant.MchId}&"
                            + $"nonce={Id.Guid()}&pubAcc=&tokenId={PrepayId}";
                string sign = EncryptUtil.HMACSHA1(data, $"{merchant.Key}&");

                var gatewayData = new GatewayData();
                gatewayData.Add("pubAccHint", "欢迎关注");
                gatewayData.Add("sigType", "HMAC-SHA1");
                gatewayData.Add("timeStamp", DateTime.Now.ToTimeStamp());
                gatewayData.Add("sig", sign);

                OrderInfo = gatewayData.ToJson();
            }
        }
    }
}
