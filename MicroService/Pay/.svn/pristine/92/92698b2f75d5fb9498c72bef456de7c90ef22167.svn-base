using Feng.Pay.Qpay.Domain;
using Feng.Pay.Qpay.Response;

namespace Feng.Pay.Qpay.Request
{
    public class CancelRequest : BaseRequest<CancelModel, CancelResponse>
    {
        public CancelRequest()
        {
            RequestUrl = "https://api.qpay.qq.com/cgi-bin/pay/qpay_reverse.cgi";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
