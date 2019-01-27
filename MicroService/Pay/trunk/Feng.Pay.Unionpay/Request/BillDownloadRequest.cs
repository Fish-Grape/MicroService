﻿using Feng.Pay.Unionpay.Domain;
using Feng.Pay.Unionpay.Response;

namespace Feng.Pay.Unionpay.Request
{
    public class BillDownloadRequest : BaseRequest<BillDownloadModel, BillDownloadResponse>
    {
        public BillDownloadRequest()
        {
#if DEBUG
            RequestUrl = "https://filedownload.test.95516.com/";
#else
            RequestUrl = "https://filedownload.95516.com/";
#endif
        }

        internal override void Execute(Merchant merchant)
        {
            GatewayData.Remove("backUrl");
            GatewayData.Remove("frontUrl");
        }
    }
}