using Microsoft.Extensions.Options;
using Feng.Pay.Core;
using Feng.Pay.Core.Request;
using Feng.Pay.Qpay.Response;
using System.Threading.Tasks;
using Feng.Utils.Helpers;
using System;

namespace Feng.Pay.Qpay
{
    /// <summary>
    /// QQ钱包网关
    /// </summary>
    public sealed class QpayGateway : BaseGateway
    {
        #region 私有字段

        private readonly Merchant _merchant;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化QQ钱包网关
        /// </summary>
        /// <param name="merchant">商户数据</param>
        public QpayGateway(Merchant merchant)
            : base(merchant)
        {
            _merchant = merchant;
        }
        

        /// <summary>
        /// 初始化QQ钱包网关
        /// </summary>
        /// <param name="merchant">商户数据</param>
        public QpayGateway(IOptions<Merchant> merchant)
            : this(merchant.Value)
        {
        }


        #endregion

        #region 属性

        public override string GatewayUrl { get; set; } = "https://qpay.qq.com";

        public new Merchant Merchant => _merchant;

        public new NotifyResponse NotifyResponse => (NotifyResponse)base.NotifyResponse;

        protected override bool IsPaySuccess => NotifyResponse.TradeState == "SUCCESS";

        protected override bool IsRefundSuccess { get; }

        protected override bool IsCancelSuccess { get; }

        protected override string[] NotifyVerifyParameter => new string[]
        {
            "appid","trade_state", "mch_id", "fee_type"
        };

        #endregion

        #region 方法

        protected override async Task<bool> ValidateNotifyAsync()
        {
            base.NotifyResponse = await GatewayData.ToObjectAsync<NotifyResponse>(StringCase.Snake);
            base.NotifyResponse.Raw = GatewayData.Raw;

            if (NotifyResponse.Sign != SubmitProcess.BuildSign(GatewayData, _merchant.Key))
            {
                throw new Exception("签名不一致");
            }

            return true;
        }

        protected override void WriteSuccessFlag()
        {
            GatewayData.Clear();
            GatewayData.Add("return_code", "SUCCESS");
            Web.Write(GatewayData.ToXml());
        }

        protected override void WriteFailureFlag()
        {
            GatewayData.Clear();
            GatewayData.Add("return_code", "FAIL");
            Web.Write(GatewayData.ToXml());
        }

        public override TResponse Execute<TModel, TResponse>(Request<TModel, TResponse> request)
        {
            return SubmitProcess.Execute(_merchant, request, GatewayUrl);
        }

        #endregion
    }
}
