using Feng.Pay.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace Feng.Pay.Wechat
{
    public static class ServiceCollectionExtensions
    {
        public static IGateways UseWechatpay(this IGateways gateways, Action<Merchant> action)
        {
            if (action != null)
            {
                var merchant = new Merchant();
                action(merchant);
                gateways.Add(new WechatGateway(merchant));
            }

            return gateways;
        }
        public static IGateways UseWechatpay(this IGateways gateways, IConfiguration configuration)
        {
            var merchants = configuration.GetSection("FengPay:Wechatpays").Get<Merchant[]>();
            if (merchants != null)
            {
                for (int i = 0; i < merchants.Length; i++)
                {
                    var wechatGateway = new WechatGateway(merchants[i]);
                    var gatewayUrl = configuration.GetSection($"FengPay:Wechatpays:{i}:GatewayUrl").Value;
                    if (!string.IsNullOrEmpty(gatewayUrl))
                    {
                        wechatGateway.GatewayUrl = gatewayUrl;
                    }

                    gateways.Add(wechatGateway);
                }
            }

            return gateways;
        }
    }
}
