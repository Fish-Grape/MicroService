using Feng.Pay.Core;
using Feng.Pay.Core.Request;

namespace Feng.Pay.Wechat.Response
{
    public class PublicKeyResponse : BaseResponse
    {
        /// <summary>
        /// RSA 公钥
        /// </summary>
        [ReName(Name = "pub_key")]
        public string PublicKey { get; set; }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
        }
    }
}
