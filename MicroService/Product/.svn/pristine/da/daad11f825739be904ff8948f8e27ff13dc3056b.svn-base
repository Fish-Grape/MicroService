using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Feng.Product.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
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
        [HttpPost,Route("add")]
        public ReturnResult<dynamic> AddProduct([FromBody]AddProductModel model) {
            var platKey = Utils.Extensions.Plat;
            return _productService.AddProduct(platKey,model);
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="productName"></param>
        /// <param name="cid"></param>
        /// <param name="brandName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isPublish"></param>
        /// <returns></returns>
        [HttpGet,Route("query")]
        public ReturnResult<PageList<QueryProductModel>> QueryProductList(int pageSize, int pageIndex, string productName = "", string cid = "", string brandName = "", string startDate = "", string endDate = "", int isPublish = -1) {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryProductList(platKey,pageSize,pageIndex, productName,cid,brandName,startDate,endDate,isPublish);
        }

        /// <summary>
        /// 根据id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("query_byid")]
        public ReturnResult<ProductModel> QueryProductById(string id)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryProductById(platKey, id);
        }

        #region 基本信息

        /// <summary>
        /// 查询产品基本信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet, Route("primary/query")]
        public ReturnResult QueryProductPrimaryInfo(string productId)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryProductPrimaryInfo(platKey, productId);
        }

        /// <summary>
        /// 修改产品基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("primary/modify")]
        public ReturnResult UpdateProductPrimaryInfo(UpdatePrimaryModel model)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.ModifyProductPrimaryInfo(platKey, model);
        }

        #endregion

        #region 媒体信息

        /// <summary>
        /// 修改产品媒体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("media/modify")]
        public ReturnResult UpdateProductMediaInfo([FromBody]UpdateProdMediaModel model)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.UpdateProductMediaInfo(platKey, model);
        }

        /// <summary>
        /// 查询产品媒体信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet, Route("media/query")]
        public ReturnResult<List<ProductMediaModel>> QueryProductMediaInfo(string productId)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.QueryProductMediaInfo(platKey, productId);
        }

        /// <summary>
        /// 新增产品媒体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("media/add")]
        public ReturnResult AddProductMediaInfo([FromBody]AddProductMediaModel model)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.AddProductMediaInfo(platKey, model);
        }

        /// <summary>
        /// 删除产品媒体信息
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [HttpGet, Route("media/delete")]
        public ReturnResult DeleteProductMediaInfo(int mediaId)
        {
            var platKey = Utils.Extensions.Plat;
            return _productService.DeleteProductMediaInfo(platKey, mediaId);
        }

        #endregion
    }
}