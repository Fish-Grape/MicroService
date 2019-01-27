using Feng.Pay.Alipay.Domain;
using Feng.Pay.Alipay.Response;

namespace Feng.Pay.Alipay.Request
{
    public class WapPayRequest : BaseRequest<WapPayModel, WapPayResponse>
    {
        public WapPayRequest()
            : base("alipay.trade.wap.pay")
        {
        }
    }
}
