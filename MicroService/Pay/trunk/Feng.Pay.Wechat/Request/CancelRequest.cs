using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;

namespace Feng.Pay.Wechat.Request
{
    public class CancelRequest : BaseRequest<CancelModel, CancelResponse>
    {
        public CancelRequest()
        {
            RequestUrl = "/secapi/pay/reverse";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
