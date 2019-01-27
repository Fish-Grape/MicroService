using Feng.Pay.Alipay.Request;
using Feng.Pay.Core.Response;

namespace Feng.Pay.Alipay.Response
{
    public class WapPayResponse : IResponse
    {
        public WapPayResponse(WapPayRequest request)
        {
            Url = $"{request.RequestUrl}&{request.GatewayData.ToUrl()}";
        }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }

        public string Raw { get; set; }
    }
}
