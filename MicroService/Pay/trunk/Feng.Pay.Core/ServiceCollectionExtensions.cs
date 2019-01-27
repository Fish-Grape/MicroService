using Microsoft.Extensions.DependencyInjection;
using System;

namespace Feng.Pay.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加支付
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        public static void AddFengPay(this IServiceCollection services, Action<IGateways> setupAction) {
            services.AddScoped<IGateways>(a =>
            {
                var gateways = new Gateways();
                setupAction(gateways);

                return gateways;
            });
        }

    }
}
