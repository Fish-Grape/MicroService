using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Product.Areas.web.Controllers.v1
{
    /// <summary>
    /// 前台接口
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{area}/v{version:apiVersion}/[controller]")]
    public class ProductWebController : ControllerBase
    {
        /// <summary>
        /// 产品服务接口
        /// </summary>
        public readonly IProductService _productService;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="productService"></param>
        public ProductWebController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 查询产品列表（分类id可选）
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet, Route("query_home")]
        public ReturnResult<List<CategoryHomeModel>> QueryProductList_home(string CategoryId = "")
        {
            var platKey = Utils.Extensions.Plat;
            if (!string.IsNullOrEmpty(CategoryId))
                return _productService.QueryProductList_home(platKey, CategoryId);
            return _productService.QueryProductList_home(platKey,"");
        }

        /// <summary>
        /// 查询产品列表（分类id可选）
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet, Route("query")]
        public ReturnResult<List<QueryProductWebModel>> QueryProductList(string CategoryId="")
        {
            var platKey = Utils.Extensions.Plat;
            if (!string.IsNullOrEmpty(CategoryId))
                return _productService.QueryProductList_web(platKey, CategoryId);
            return _productService.QueryProductList_web(platKey);
        }

        /// <summary>
        /// 根据id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("query_byid")]
        public ReturnResult<QueryProductWebModel> QueryProductById(string id)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryProduct_web(platKey, id);
        }
    }
}