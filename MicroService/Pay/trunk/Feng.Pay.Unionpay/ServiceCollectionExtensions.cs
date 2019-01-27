using Feng.Pay.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace Feng.Pay.Unionpay
{
    public static class ServiceCollectionExtensions
    {
        public static IGateways UseUnionpay(this IGateways gateways, Action<Merchant> action)
        {
            if (action != null)
            {
                var merchant = new Merchant();
                action(merchant);
                gateways.Add(new UnionpayGateway(merchant));
            }

            return gateways;
        }

        public static IGateways UseUnionpay(this IGateways gateways, IConfiguration configuration)
        {
            var merchants = configuration.GetSection("FengPay:Unionpays").Get<Merchant[]>();
            if (merchants != null)
            {
                for (int i = 0; i < merchants.Length; i++)
                {
                    var unionpayGateway = new UnionpayGateway(merchants[i]);
                    var gatewayUrl = configuration.GetSection($"FengPay:Unionpays:{i}:GatewayUrl").Value;
                    if (!string.IsNullOrEmpty(gatewayUrl))
                    {
                        unionpayGateway.GatewayUrl = gatewayUrl;
                    }

                    gateways.Add(unionpayGateway);
                }
            }

            return gateways;
        }
    }
}
