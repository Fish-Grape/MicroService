using Feng.Pay.Alipay.Domain;
using Feng.Pay.Alipay.Response;

namespace Feng.Pay.Alipay.Request
{
    public class TransferRequest : BaseRequest<TransferModel, TransferResponse>
    {
        public TransferRequest()
            : base("alipay.fund.trans.toaccount.transfer")
        {
        }
    }
}
