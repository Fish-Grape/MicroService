﻿using Feng.Pay.Core.Request;
using Feng.Utils.Helpers;
using System.Threading.Tasks;

namespace Feng.Pay.Alipay.Response
{
    public class BillDownloadResponse : BaseResponse
    {
        /// <summary>
        /// 账单下载地址链接，获取连接后30秒后未下载，链接地址失效。
        /// </summary>
        public string BillDownloadUrl { get; set; }

        private byte[] _billFile;

        /// <summary>
        /// 获取账单文件
        /// </summary>
        public byte[] GetBillFile()
        {
            if (_billFile == null && !string.IsNullOrEmpty(BillDownloadUrl))
            {
                _billFile = Web.Download(BillDownloadUrl);
            }

            return _billFile;
        }

        /// <summary>
        /// 获取账单文件
        /// </summary>
        public async Task<byte[]> GetBillFileAsync()
        {
            if (_billFile == null && !string.IsNullOrEmpty(BillDownloadUrl))
            {
                _billFile = await Web.DownloadAsync(BillDownloadUrl);
            }

            return _billFile;
        }

        internal override void Execute<TModel, TResponse>(Merchant merchant, Request<TModel, TResponse> request)
        {
        }
    }
}
