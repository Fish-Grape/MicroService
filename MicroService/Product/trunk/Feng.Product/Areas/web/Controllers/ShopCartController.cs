using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.IService.Background;
using Feng.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Feng.Product.Areas.web.Controllers.v1
{
    /// <summary>
    /// 购物车管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{area}/v{version:apiVersion}/[controller]")]
    public class ShopCartController : ControllerBase
    {
        /// <summary>
        /// 购物车管理接口
        /// </summary>
        public readonly IShoppingCartService _shoppingCart;

        public ShopCartController(IShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// 添加产品到购物车
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("add")]
        public ReturnResult<dynamic> AddToCart([FromBody]AddShoppingCartModel model) {
            var platKey = Utils.Extensions.Plat;
            return _shoppingCart.AddToCart(platKey,model);
        }

        /// <summary>
        /// 查询购物车列表
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet,Route("query")]
        public ReturnResult<List<QueryShoppingCartModel>> QueryProductList(int user_id) {
            var platKey = Utils.Extensions.Plat;
            return _shoppingCart.QueryShoppingCartList(platKey, user_id);
        }

        /// <summary>
        /// 从购物车中移除产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("delete")]
        public ReturnResult RemoveProductFromCart([FromBody]RemoveCartModel model)
        {
            var platKey = Utils.Extensions.Plat;
            return _shoppingCart.RemoveProductFromCart(platKey, model);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet, Route("clear")]
        public ReturnResult ClearCart(int user_id)
        {
            var platKey = Utils.Extensions.Plat;
            return _shoppingCart.ClearCart(platKey, user_id);
        }
    }
}