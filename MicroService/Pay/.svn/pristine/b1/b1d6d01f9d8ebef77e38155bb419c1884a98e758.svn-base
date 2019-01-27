using Feng.Pay.Core.Response;
using Feng.Pay.Unionpay.Request;

namespace Feng.Pay.Unionpay.Response
{
    public class WapPayResponse : IResponse
    {
        public WapPayResponse(WapPayRequest request)
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
