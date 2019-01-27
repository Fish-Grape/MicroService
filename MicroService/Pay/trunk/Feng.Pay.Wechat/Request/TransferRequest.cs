using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;

namespace Feng.Pay.Wechat.Request
{
    public class TransferRequest : BaseRequest<TransferModel, TransferResponse>
    {
        public TransferRequest()
        {
            RequestUrl = "/mmpaymkttransfers/promotion/transfers";
            IsUseCert = true;
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Add("mch_appid", merchant.AppId);
            GatewayData.Add("mchid", merchant.MchId);

            GatewayData.Remove("appid");
            GatewayData.Remove("mch_id");
            GatewayData.Remove("notify_url");
            GatewayData.Remove("sign_type");
        }
    }
}
