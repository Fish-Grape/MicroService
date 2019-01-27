using Feng.Pay.Qpay.Domain;
using Feng.Pay.Qpay.Response;

namespace Feng.Pay.Qpay.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse>
    {
        public BillDownloadRequest()
        {
            RequestUrl = "/cgi-bin/sp_download/qpay_mch_statement_down.cgi";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("sign_type");
            GatewayData.Remove("notify_url");
        }
    }
}
