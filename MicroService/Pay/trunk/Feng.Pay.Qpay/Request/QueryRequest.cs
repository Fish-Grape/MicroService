using Feng.Pay.Qpay.Domain;
using Feng.Pay.Qpay.Response;

namespace Feng.Pay.Qpay.Request
{
    public class QueryRequest : BaseRequest<QueryModel, QueryResponse>
    {
        public QueryRequest()
        {
            RequestUrl = "/cgi-bin/pay/qpay_order_query.cgi";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
