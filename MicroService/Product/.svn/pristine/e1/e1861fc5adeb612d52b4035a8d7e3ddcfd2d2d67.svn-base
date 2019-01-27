using Feng.Basic;
using Feng.Product.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.IService.Background
{
    public interface IShoppingCartService
    {
        ReturnResult<dynamic> AddToCart(string platKey, AddShoppingCartModel model);

        ReturnResult<int> RemoveProductFromCart(string platKey, RemoveCartModel model);

        ReturnResult ClearCart(string platKey, int user_id);

        ReturnResult ModifyProductInCartNum(string platKey, ModifyProNumModel model);

        ReturnResult<List<QueryShoppingCartModel>> QueryShoppingCartList(string platKey, int user_id);
    }
}
