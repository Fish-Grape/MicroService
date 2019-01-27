using Feng.Pay.Wechat.Domain;
using Feng.Pay.Wechat.Response;
using System;

namespace Feng.Pay.Wechat.Request
{
    public class OAuthRequest : BaseRequest<OAuthModel, OAuthResponse>
    {
        public OAuthRequest()
        {
            RequestUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
        }

        internal override void Execute(Merchant merchant)
        {
            if (string.IsNullOrEmpty(merchant.AppSecret))
            {
                throw new Exception("请设置AppSecret");
            }

            GatewayData.Add("secret", merchant.AppSecret);
            GatewayData.Remove("notify_url");
            GatewayData.Remove("sign_type");
            GatewayData.Remove("mch_id");
        }
    }
}
