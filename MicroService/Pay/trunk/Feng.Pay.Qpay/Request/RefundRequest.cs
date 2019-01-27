using Feng.Pay.Qpay.Domain;
using Feng.Pay.Qpay.Response;

namespace Feng.Pay.Qpay.Request
{
    public class RefundRequest : BaseRequest<RefundModel, RefundResponse>
    {
        public RefundRequest()
        {
            RequestUrl = "https://api.qpay.qq.com/cgi-bin/pay/qpay_refund.cgi";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
