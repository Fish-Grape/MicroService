using Feng.Pay.Alipay.Domain;
using Feng.Pay.Alipay.Response;

namespace Feng.Pay.Alipay.Request
{
    public class CancelRequest : BaseRequest<CancelModel, CancelResponse>
    {
        public CancelRequest()
            : base("alipay.trade.cancel")
        {
        }
    }
}
