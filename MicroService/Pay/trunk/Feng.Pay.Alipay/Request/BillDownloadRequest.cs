using Feng.Pay.Alipay.Domain;
using Feng.Pay.Alipay.Response;

namespace Feng.Pay.Alipay.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse>
    {
        public BillDownloadRequest()
            : base("alipay.data.dataservice.bill.downloadurl.query")
        {
        }
    }
}
