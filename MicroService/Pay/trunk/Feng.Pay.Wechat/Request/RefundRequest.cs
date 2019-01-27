using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;

namespace Feng.Pay.Wechat.Request
{
    public class RefundRequest : BaseRequest<RefundModel, RefundResponse>
    {
        public RefundRequest()
        {
            RequestUrl = "/secapi/pay/refund";
            IsUseCert = true;
        }
    }
}
