using Feng.Pay.Alipay.Request;
using Feng.Pay.Core.Response;

namespace Feng.Pay.Alipay.Response
{
    public class WebPayResponse : IResponse
    {
        public WebPayResponse(WebPayRequest request)
        {
            Html = request.GatewayData.ToForm(request.RequestUrl);
        }

        /// <summary>
        /// 生成的Html网页
        /// </summary>
        public string Html { get; set; }

        public string Raw { get; set; }
    }
}
