using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;

namespace Feng.Pay.Wechat.Request
{
    public class CloseRequest : BaseRequest<CloseModel, CloseResponse>
    {
        public CloseRequest()
        {
            RequestUrl = "/pay/closeorder";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
