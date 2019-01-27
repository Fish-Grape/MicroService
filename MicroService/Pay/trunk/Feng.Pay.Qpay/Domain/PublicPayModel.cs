namespace Feng.Pay.Qpay.Domain
{
    public class PublicPayModel : BasePayModel
    {
        public PublicPayModel()
        {
            TradeType = "JSAPI";
        }
    }
}
