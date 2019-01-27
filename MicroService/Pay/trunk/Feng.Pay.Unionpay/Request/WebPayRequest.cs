using Feng.Pay.Unionpay.Domain;
using Feng.Pay.Unionpay.Response;

namespace Feng.Pay.Unionpay.Request
{
    public class WebPayRequest : BaseRequest<WebPayModel, WebPayResponse>
    {
        public WebPayRequest()
        {
            RequestUrl = "/gateway/api/frontTransReq.do";
        }
    }
}
