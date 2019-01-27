using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Product.Areas.Auth.Controllers
{
    /// <summary>
    /// 认证产品管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{area}/v{version:apiVersion}/prod")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// 产品服务接口
        /// </summary>
        public readonly IProductService _productService;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        [Authorize("permission")]
        public ReturnResult<dynamic> AddProduct([FromBody]AddAuthProductModel model)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.AddAuthProduct(platKey, model);
        }
        /// <summary>
        /// 查询认证产品列表
        /// </summary>
        /// <param name="cid">默认为空，查询全部</param>
        /// <returns></returns>
        [HttpGet, Route("query")]
        [Authorize("permission")]
        public ReturnResult<List<QueryAuthProductModel>> QueryAuthProductList(string cid="") {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryAuthProductList(platKey, cid);
        }
        /// <summary>
        /// 修改产品状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("modify")]
        [Authorize("permission")]
        public ReturnResult ModifyProductStatus([FromBody]ModifyProductStatus model)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.ModifyProductStatus(platKey, model);
        }
    }
}