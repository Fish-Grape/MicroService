using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;

namespace Feng.Pay.Wechat.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse>
    {
        public BillDownloadRequest()
        {
            RequestUrl = "/pay/downloadbill";
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("notify_url");
        }
    }
}
