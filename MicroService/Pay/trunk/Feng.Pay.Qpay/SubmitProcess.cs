﻿using Feng.Pay.Core;
using Feng.Pay.Core.Request;
using Feng.Pay.Core.Response;
using Feng.Pay.Qpay.Request;
using Feng.Pay.Qpay.Response;
using Feng.Utils.Helpers;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Feng.Pay.Qpay
{
    internal static class SubmitProcess
    {
        private static string _gatewayUrl;

        internal static TResponse Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request, string gatewayUrl = null) where TResponse : IResponse
        {
            AddMerchant(merchant, request, gatewayUrl);

            X509Certificate2 cert = null;
            if (((BaseRequest<TModel, TResponse>)request).IsUseCert)
            {
                cert = new X509Certificate2(merchant.SslCertPath, merchant.SslCertPassword, X509KeyStorageFlags.MachineKeySet);
            }

            string result = null;
            Task.Run(async () =>
            {
                result = await Web
                 .PostAsync(request.RequestUrl, request.GatewayData.ToXml(), cert);
            })
            .GetAwaiter()
            .GetResult();

            BaseResponse baseResponse;
            if (!(request is BillDownloadRequest))
            {
                var gatewayData = new GatewayData();
                gatewayData.FromXml(result);

                baseResponse = (BaseResponse)(object)gatewayData.ToObject<TResponse>(StringCase.Snake);
                baseResponse.Raw = result;
                baseResponse.GatewayData = gatewayData;
                if (baseResponse.ReturnCode == "SUCCESS")
                {
                    string sign = gatewayData.GetStringValue("sign");

                    if (!string.IsNullOrEmpty(sign) && !CheckSign(gatewayData, merchant.Key, sign))
                    {
                        throw new Exception("签名验证失败");
                    }

                    baseResponse.Sign = sign;
                    baseResponse.Execute(merchant, request);
                }
            }
            else
            {
                baseResponse = (BaseResponse)Activator.CreateInstance(typeof(TResponse));
                baseResponse.Raw = result;
                baseResponse.Execute(merchant, request);
            }

            return (TResponse)(object)baseResponse;
        }

        private static void AddMerchant<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request, string gatewayUrl) where TResponse : IResponse
        {
            if (!string.IsNullOrEmpty(gatewayUrl))
            {
                _gatewayUrl = gatewayUrl;
            }

            if (!request.RequestUrl.StartsWith("http"))
            {
                request.RequestUrl = _gatewayUrl + request.RequestUrl;
            }
            request.GatewayData.Add(merchant, StringCase.Snake);
            ((BaseRequest<TModel, TResponse>)request).Execute(merchant);

            string sign = BuildSign(request.GatewayData, merchant.Key);
            request.GatewayData.Add("sign", sign);
        }

        internal static string BuildSign(GatewayData gatewayData, string key)
        {
            gatewayData.Remove("sign");

            string data = $"{gatewayData.ToUrl(false)}&key={key}";
            return EncryptUtil.MD5(data);
        }

        internal static bool CheckSign(GatewayData gatewayData, string key, string sign)
        {
            return BuildSign(gatewayData, key) == sign.ToUpper();
        }
    }
}
