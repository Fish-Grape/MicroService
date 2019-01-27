using Feng.Basic;
using Feng.Product.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.IService
{
    public partial interface IProductService
    {
        /// <summary>
        /// 获取首页产品
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        ReturnResult<List<CategoryHomeModel>> QueryProductList_home(string platKey, string CategoryId);
        ReturnResult<List<QueryProductWebModel>> QueryProductList_web(string platKey);
        ReturnResult<List<QueryProductWebModel>> QueryProductList_web(string platKey, string CategoryId);
        ReturnResult<QueryProductWebModel> QueryProduct_web(string platKey, string ProductId);
    }
}
