using Feng.Basic;
using Feng.Core.Config;
using Feng.Product.Entity;
using Feng.Product.IService.Background;
using Feng.Product.Model;
using Feng.Utils.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Product.Service.Background
{
    public class ShoppingCartService : IShoppingCartService
    {
        public readonly SqlSugarClient _dbContext;
        public readonly IConfig _config;

        public ShoppingCartService(SqlSugarClient dbContext, IConfig config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public ReturnResult<dynamic> AddToCart(string platKey, AddShoppingCartModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");
            string strId = string.Empty;
            var result = _dbContext.Ado.UseTran(() =>
            {
                List<shopping_cart> carts = new List<shopping_cart>();
                List<shopping_cart_detail> shopping_Cart_s = new List<shopping_cart_detail>();
                model.SkuDetails.ForEach(a=> {
                    strId = Id.ObjectId();
                    carts.Add(new shopping_cart()
                    {
                        id = strId,
                        platkey = platKey,
                        user_id = model.user_id,
                        user_name = model.user_name,
                        is_product_exists = true,
                        productid = model.productId,
                        product_name=model.productName,
                        buy_method=model.BuyMethod,
                        buy_period=model.BuyPeriod,
                        skuid = a.skuId,
                        price = a.price,
                        good_name = a.skuName,
                        valid_type=model.valid_type,
                        create_time = DateTime.Now
                    });
                    a.cartDetailModels.ForEach(b=> {
                        shopping_Cart_s.Add(new shopping_cart_detail()
                        {
                            id = Id.ObjectId(),
                            platkey = platKey,
                            extend_attr_id = b.extend_attr_id,
                            extend_attr_name = b.extend_attr_name,
                            shopping_cart_id = strId,
                            create_time = DateTime.Now
                        });
                    });
                });

                List<string> ids = _dbContext.Queryable<shopping_cart>().Where(it =>
                      it.user_id == model.user_id
                      && it.productid == model.productId)
                      .WhereIF(!string.IsNullOrEmpty(model.valid_type), it => it.valid_type == model.valid_type)
                .In(it=>it.skuid,(from sku in model.SkuDetails select sku.skuId).ToArray())
                .Select(it => it.id).ToList();
                if (ids == null || ids.Count <= 0)
                {
                    _dbContext.Insertable(carts).ExecuteCommand();
                    _dbContext.Insertable(shopping_Cart_s).ExecuteCommand();
                }
                else
                {
                    ids.ForEach(a =>
                    {
                        //先删在加
                        _dbContext.Deleteable<shopping_cart_detail>()
                        .Where(b => b.shopping_cart_id == a).ExecuteCommand();
                        _dbContext.Deleteable<shopping_cart>()
                        .Where(b => b.id == a).ExecuteCommand();
                    });
                    //购物车&详情
                    _dbContext.Insertable(carts).ExecuteCommand();
                    _dbContext.Insertable(shopping_Cart_s).ExecuteCommand();
                }
            });
            if (result.Data)
                return new ReturnResult<dynamic>(GetPordNumInCart(model.user_id));
            else
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "添加失败!");
        }

        public ReturnResult ClearCart(string platKey, int user_id)
        {
            if (string.IsNullOrEmpty(platKey))
                return new ReturnResult<int>((int)ErrorCodeEnum.Failed, 0, "参数异常!");
            int prodNum = 0;//购物车中的商品数量
            var result = _dbContext.Ado.UseTran(() =>
            {
                List<string> ids = _dbContext.Queryable<shopping_cart>()
                .Where(a => a.user_id == user_id)
                 .Select(it => it.id).ToList();
                //先删购物车详情表，再删购物车表
                _dbContext.Deleteable<shopping_cart_detail>()
                .In(it => it.shopping_cart_id, ids).ExecuteCommand();
                _dbContext.Deleteable<shopping_cart>()
                .In(ids).ExecuteCommand();
                prodNum = GetPordNumInCart(user_id);
            });

            if (result.Data)
                return new ReturnResult<int>(prodNum);
            else
                return new ReturnResult<int>((int)ErrorCodeEnum.Failed, 0, "删除失败!");
        }

        public ReturnResult ModifyProductInCartNum(string platKey, ModifyProNumModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");

            shopping_cart cart = new shopping_cart()
            {
                id = model.id,
                number = model.number
            };
            var result = _dbContext.Updateable(cart).UpdateColumns(it => new { it.number }).ExecuteCommand();
            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");
            return new ReturnResult();
        }

        public ReturnResult<List<QueryShoppingCartModel>> QueryShoppingCartList(string platKey, int user_id)
        {
            if (string.IsNullOrEmpty(platKey))
                return new ReturnResult<List<QueryShoppingCartModel>>((int)ErrorCodeEnum.Parameter_Missing,null, "参数异常!");
            string flag=_config.StringGet("Valid_TypeId");

            //查询审核类型
            List<AttrValPrimaryModel> validTypes = _dbContext.Queryable<attribute_values>()
                .Where(a => a.attributeid == flag).Select(a => new AttrValPrimaryModel() {
                    id = a.id,
                    valueName = a.name
                }).ToList();
            if(validTypes==null || validTypes.Count==0)
                return new ReturnResult<List<QueryShoppingCartModel>>((int)ErrorCodeEnum.Error_SqlData,null,"审核类型为空！请联系管理员!");
            
            List<QueryShoppingCartModel> list =_dbContext.Queryable<shopping_cart,product>((a,b)=>new object[]{
                JoinType.Inner,a.productid==b.id
            })
                .Where((a,b) => a.user_id == user_id && a.platkey == platKey)
                .GroupBy((a, b) => new {
                    b.categoryid,
                    a.user_id,
                    a.user_name,
                    productId = a.productid,
                    productName = a.product_name,
                    BuyMethod = a.buy_method,
                    BuyPeriod = a.buy_period,
                    a.valid_type,
                })
                .Select((a, b) => new QueryShoppingCartModel()
                {
                    categoryId=b.categoryid,
                    user_id=a.user_id,
                    user_name=a.user_name,
                    productId=a.productid,
                    productName=a.product_name,
                    BuyMethod=a.buy_method,
                    BuyPeriod=a.buy_period,
                    valid_type=a.valid_type,
                }).ToList();


            List<QueryShopSkuModel> skuList = _dbContext.Queryable<shopping_cart,product_sku>((a,b)=>new object[] {
                JoinType.Inner,a.skuid==b.id
            })
                .Where((a, b) => a.user_id == user_id && a.platkey == platKey)
                .OrderBy((a, b) => a.create_time, OrderByType.Desc)
                .Select((a, b) => new QueryShopSkuModel()
                {
                    ShoppingCartId=a.id,
                    productId=a.productid,
                    skuId = a.skuid,
                    price = a.price,
                    skuName = a.good_name,
                    skuJson=b.attr_json
                }).ToList();

            List<QueryShoppingCartDetailModel> detailModels = _dbContext.Queryable<shopping_cart_detail>()
                .In(a => a.shopping_cart_id, (from x in skuList select x.ShoppingCartId).ToArray())
                .Select(a => new QueryShoppingCartDetailModel()
                {
                    ShoppingCartId = a.shopping_cart_id,
                    extend_attr_id = a.extend_attr_id,
                    extend_attr_name = a.extend_attr_name
                }).ToList();


            List<QueryShoppingCartModel> result = new List<QueryShoppingCartModel>();
            QueryShoppingCartModel queryShopping = null;
            List<QueryShopSkuModel> shopSku = null;
            skuList.ForEach(b => {
                b.cartDetailModels = detailModels.FindAll(c => c.ShoppingCartId == b.ShoppingCartId);
            });
            validTypes.ForEach(a => {
                list.ForEach(b =>
                {
                    queryShopping = new QueryShoppingCartModel() {
                        categoryId=b.categoryId,
                        ShoppingCartId = b.ShoppingCartId,
                        user_id = b.user_id,
                        user_name = b.user_name,
                        productId = b.productId,
                        productName = b.productName,
                        BuyMethod = b.BuyMethod,
                        BuyPeriod = b.BuyPeriod
                    };
                    shopSku = skuList.FindAll(c => c.skuJson.Contains(a.id));
                    queryShopping.productName += "-" + a.valueName;
                    queryShopping.skuDetails = shopSku;
                    result.Add(queryShopping);
                });
            });

            return new ReturnResult<List<QueryShoppingCartModel>>(result);
        }

        public ReturnResult<int> RemoveProductFromCart(string platKey, RemoveCartModel model)
        {
            if (string.IsNullOrEmpty(platKey))
                return new ReturnResult<int>((int)ErrorCodeEnum.Failed, 0, "参数异常!");
            int prodNum = 0;//购物车中的商品数量
            var result=_dbContext.Ado.UseTran(() =>
            {
                List<string> ids = _dbContext.Queryable<shopping_cart>()
                .Where(a=>a.user_id==model.user_id && a.productid==model.product_id)
                .WhereIF(!string.IsNullOrEmpty(model.valid_type) ,a=> a.valid_type == model.valid_type)
                 .Select(it => it.id).ToList();
                //先删购物车详情表，再删购物车表
                _dbContext.Deleteable<shopping_cart_detail>()
                .In(it => it.shopping_cart_id, ids).ExecuteCommand();
                _dbContext.Deleteable<shopping_cart>()
                .In(ids).ExecuteCommand();
                prodNum = GetPordNumInCart(model.user_id);
            });

            if (result.Data)
                return new ReturnResult<int>(prodNum);
            else
                return new ReturnResult<int>((int)ErrorCodeEnum.Failed,0, "删除失败!");
        }

        /// <summary>
        /// 获取购物车中的商品数量
        /// </summary>
        /// <returns></returns>
        public int GetPordNumInCart(int user_id)
        {
            return _dbContext.Queryable<shopping_cart>()
                .Where(a=>a.user_id==user_id).Count();
        }
    }
}
