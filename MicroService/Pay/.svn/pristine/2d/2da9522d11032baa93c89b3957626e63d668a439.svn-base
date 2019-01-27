using Feng.Pay.Qpay.Domain;
using Feng.Pay.Qpay.Response;

namespace Feng.Pay.Qpay.Request
{
    public class CloseRequest : BaseRequest<CloseModel, CloseResponse>
    {
        public CloseRequest()
        {
            RequestUrl = "/cgi-bin/pay/qpay_close_order.cgi";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
