using Feng.Basic;
using Feng.Product.Entity;
using Feng.Product.IService;
using Feng.Product.Model;
using Feng.Utils.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feng.Product.Service
{
    public partial class ProductService : IProductService
    {
        #region 认证产品
        public ReturnResult<dynamic> AddAuthProduct(string platKey, AddAuthProductModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            if (model.skuList.Count <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_Data_State, null, "数据异常!");

            try
            {
                _dbContext.Ado.BeginTran();
                #region 产品基础信息
                product item = new product()
                {
                    id = Id.ObjectId(),
                    categoryid = model.categoryId,
                    name = model.goodsName,
                    product_code = "",
                    brandid = 0,
                    platkey = platKey,
                    sales_price = 0,
                    market_price = 0,
                    price = 0,
                    weight = 0,
                    xxx = 0,
                    yyy = 0,
                    zzz = 0,
                    density = 0,
                    is_publish = model.isPublish,
                    short_desc = model.shortDesc,
                    description = model.description,
                    addtime = DateTime.Now
                };
                _dbContext.Insertable(item).ExecuteCommand();
                #endregion

                #region  产品SKU
                //添加规格
                List<product_sku> skuList = new List<product_sku>();
                model.skuList.ForEach((s) =>
                {
                    skuList.Add(new product_sku()
                    {
                        id = Id.ObjectId(),
                        productid = item.id,
                        attr_json = s.attrJson,
                        barcode = "",
                        count = 0,
                        name = "",
                        price = s.price,
                        platkey = platKey,
                        addtime = DateTime.Now
                    });
                });
                _dbContext.Insertable(skuList).ExecuteCommand();
                #endregion

                #region 产品属性
                //添加属性
                List<product_attribute> attrList = new List<product_attribute>();
                if (model.attrList.Count > 0)
                {
                    model.attrList.ForEach((a) =>
                    {
                        a.attributeValues.ForEach(b =>
                        {
                            attrList.Add(new product_attribute()
                            {
                                attributeid = a.attributeId,
                                attr_val_id = b,
                                productid = item.id,
                                platkey = platKey,
                                skuid = ""
                            });
                        });
                    });
                }
                skuList.ForEach((s) =>
                {
                    if (!string.IsNullOrWhiteSpace(s.attr_json))
                    {
                        string[] strArray = s.attr_json.Split(',');
                        foreach (string str in strArray)
                        {
                            string[] strItem = str.Split(':');
                            attrList.Add(new product_attribute()
                            {
                                attributeid = strItem[0],
                                attr_val_id = strItem[1],
                                productid = item.id,
                                skuid = s.id,
                                platkey = platKey
                            });
                        }

                    }
                });
                _dbContext.Insertable(attrList).ExecuteCommand();
                #endregion

                _dbContext.Ado.CommitTran();

                return new ReturnResult<dynamic>(new { item.id });

            }
            catch (Exception ex)
            {

                _dbContext.Ado.RollbackTran();
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Failed, null, "添加失败!");
            }
        }

        public ReturnResult<List<QueryAuthProductModel>> QueryAuthProductList(string platKey, string cid)
        {
            var list = _dbContext.Queryable<product>()
                 .Where(a => a.platkey == platKey)
                 .WhereIF(!string.IsNullOrEmpty(cid), a => a.categoryid == cid)
                 .Select(a => new QueryAuthProductModel()
                 {
                     productId = a.id,
                     productName = a.name,
                     isPublish = a.is_publish,
                     addTime = a.addtime
                 })
                 .ToList();

            if (list.Count > 0)
                return new ReturnResult<List<QueryAuthProductModel>>(list);

            return new ReturnResult<List<QueryAuthProductModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有数据!");

        }

        #endregion



        public ReturnResult<dynamic> AddProduct(string platKey, AddProductModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing,null,"参数异常!");

            //验证用户

            if (model.skuList.Count <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_Data_State, null, "数据异常!");

            try {
                _dbContext.Ado.BeginTran();
                #region 产品基础信息
                product item = new product()
                {
                    id = Id.ObjectId(),
                    categoryid = model.categoryId,
                    name = model.goodsName,
                    product_code = model.productCode,
                    brandid = model.brandId,
                    platkey = platKey,
                    sales_price = model.salesPrice,
                    market_price = model.marketPrice,
                    price = model.price,
                    weight = model.weight,
                    xxx = model.xxx,
                    yyy = model.yyy,
                    zzz = model.zzz,
                    density = model.density,
                    is_publish = model.isPublish,
                    short_desc = model.shortDesc,
                    description = model.description,
                    addtime = DateTime.Now
                };
                _dbContext.Insertable(item).ExecuteCommand();
                #endregion

                #region  产品SKU
                //添加规格
                List <product_sku> skuList = new List<product_sku>();
                model.skuList.ForEach((s) =>
                {
                    skuList.Add(new product_sku()
                    {
                        id = Id.ObjectId(),
                        productid = item.id,
                        attr_json = s.attrJson,
                        barcode = s.barCode,
                        count = s.count,
                        name = s.name,
                        price = s.price,
                        platkey = platKey,
                        addtime = DateTime.Now
                    });
                });
                _dbContext.Insertable(skuList).ExecuteCommand();
                #endregion

                #region 产品属性
                //添加属性
                List<product_attribute> attrList = new List<product_attribute>();
                if (model.attrList.Count > 0)
                {
                    model.attrList.ForEach((a) =>
                    {
                        a.attributeValues.ForEach(b =>
                        {
                            attrList.Add(new product_attribute()
                            {
                                attributeid = a.attributeId,
                                attr_val_id = b.id,
                                productid = item.id,
                                platkey = platKey,
                                skuid = ""
                            });
                        });
                      });
                }
                skuList.ForEach((s) =>
                {
                    if (!string.IsNullOrWhiteSpace(s.attr_json))
                    {
                        string[] strArray = s.attr_json.Split(',');
                        foreach (string str in strArray)
                        {
                            string[] strItem = str.Split(':');
                            attrList.Add(new product_attribute()
                            {
                                attributeid = strItem[0],
                                attr_val_id = strItem[1],
                                productid = item.id,
                                skuid = s.id,
                                platkey = platKey
                            });
                        }

                    }
                });
                _dbContext.Insertable(attrList).ExecuteCommand();
                #endregion

                _dbContext.Ado.CommitTran();

                return new ReturnResult<dynamic>(new { item.id });

            } catch (Exception ex) {

                _dbContext.Ado.RollbackTran();
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Failed, null, "添加失败!");
            }
        }

        public ReturnResult<ProductModel> QueryProductById(string platKey, string id,bool isWeb = false)
        {
            #region 基本信息
            ProductModel product = _dbContext.Queryable<product>()
                .Where(a => a.id == id && a.platkey == platKey)
                .WhereIF(isWeb, a => a.is_publish)
                .Select(model => new ProductModel()
                {
                    id = id,
                    categoryId = model.categoryid,
                    goodsName = model.name,
                    productCode = model.product_code,
                    brandId = model.brandid,
                    salesPrice = model.sales_price,
                    marketPrice = model.market_price,
                    price = model.price,
                    weight = model.weight,
                    xxx = model.xxx,
                    yyy = model.yyy,
                    zzz = model.zzz,
                    density = model.density,
                    isPublish = model.is_publish,
                    shortDesc = model.short_desc,
                    description = model.description
                }).First();
            #endregion
            //前台页面未上架则不展示
            if(product==null || !product.isPublish)
                return new ReturnResult<ProductModel>();

            #region 媒体资源
            product.mediaList = _dbContext.Queryable<product_media>()
                .Where(a => a.productid == id && a.platkey == platKey)
                .Select(x => new ProductMediaModel()
                {
                    mediaType = x.media_type,
                    mediaUrl = x.media_url,
                    displayOrder = x.order
                }).ToList();
            #endregion

            #region 属性信息
            List<ProductAttrModel> attrs=_dbContext.Queryable<product_attribute, attribute>
                ((a,b)=>a.attributeid==b.id)
                .Where((a,b) => a.productid == id && a.platkey == platKey)
                .GroupBy((a,b)=>new {
                    attributeId = b.id,
                    AttributeName = b.name,
                    displayOrder = b.order
                })
                .Select((a,b)=>new ProductAttrModel() {
                    attributeId=b.id,
                    AttributeName=b.name,
                    displayOrder=b.order
                }).ToList();
            List<AttributeValueModel> vals= _dbContext.Queryable<attribute_values>().In(
                a=>a.attributeid,(from x in attrs select x.attributeId).ToArray()
                ).GroupBy(a => new
                {
                    a.id,
                    a.attributeid,
                    valueName = a.name,
                    displayOrder = a.order,
                    valueImage = a.image_url
                })
                .Select(a => new AttributeValueModel() {
                    id=a.id,
                    attributeId=a.attributeid,
                    valueName=a.name,
                    displayOrder=a.order,
                    valueImage=a.image_url
                }).ToList();
            attrs.ForEach(a=> {
                a.attributeValues = vals.FindAll(x => a.attributeId == x.attributeId);
            });
            product.attrList = attrs;
            #endregion

            #region 产品SKU
            product.skuList = _dbContext.Queryable<product_sku>()
                 .Where(a => a.productid == id && a.platkey == platKey)
                 .Select(x => new ProductSkuModel()
                 {
                     count = x.count,
                     name = x.name,
                     price = x.price,
                     attrJson = x.attr_json,
                     barCode = x.barcode
                 }).ToList();
            #endregion
            return new ReturnResult<ProductModel>(product);
        }

        public ReturnResult<PageList<QueryProductModel>> QueryProductList(string platKey, int pageSize, int pageIndex, string productName, string cid, string brandName, string startDate, string endDate, int isPublish)
        {
            PageList<QueryProductModel> page = null;
            int totalNumber = 0;
            var list = _dbContext.Queryable<product, brand>((a, b) =>
                  new object[]{
                    JoinType.Left,a.brandid==b.id
                  })
                 .Where((a, b) => a.platkey == platKey)
                 .Where((a, b) => a.platkey == b.platkey)
                 .WhereIF(!string.IsNullOrEmpty(cid), (a, b) => a.categoryid == cid)
                 .WhereIF(!string.IsNullOrEmpty(productName), (a, b) => a.name.Contains(productName))
                 .WhereIF(!string.IsNullOrEmpty(brandName), (a, b) => b.cn_name.Contains(brandName) || b.en_name.Contains(brandName))
                 .WhereIF(isPublish > -1, (a, b) => a.is_publish == SqlFunc.IIF(isPublish == 0, false, true))
                 .WhereIF(!string.IsNullOrEmpty(startDate), (a, b) => a.addtime >= SqlFunc.ToDate(startDate))
                 .WhereIF(!string.IsNullOrEmpty(endDate), (a, b) => a.addtime <= SqlFunc.ToDate(endDate))
                 .Select((a, b) => new QueryProductModel() {
                     productId = a.id,
                     productName = a.name,
                     brandName = b.cn_name,
                     isPublish = a.is_publish,
                     salesPrice = a.sales_price,
                     marketPrice = a.market_price,
                     addTime = a.addtime
                 })
                 .ToPageList(pageIndex, pageSize, ref totalNumber);
            
            if (list.Count > 0) {
                page = new PageList<QueryProductModel>();
                page.DataList = list;
                page.Page = pageIndex;
                page.PageSize = pageSize;
                page.TotalCount = totalNumber;
                return new ReturnResult<PageList<QueryProductModel>>(page);
            }
            return new ReturnResult<PageList<QueryProductModel>>((int)ErrorCodeEnum.Error_NoData,page,"没有数据!");
        }


        #region 修改产品
        /// <summary>
        /// 修改产品状态
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="pid"></param>
        /// <param name="isPublish"></param>
        /// <returns></returns>
        public ReturnResult ModifyProductStatus(string platKey, ModifyProductStatus model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");
            int result = _dbContext.Updateable<product>()
                .UpdateColumns(p => new product() { is_publish = model.isPublish })
                .Where(p => p.platkey == platKey)
                .Where(p => p.id == model.productId)
                .ExecuteCommand();
            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");

            return new ReturnResult();
        }


        #region 媒体信息

        /// <summary>
        /// 查询产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<ProductMediaModel>> QueryProductMediaInfo(string platKey, string pid)
        {
            if (string.IsNullOrEmpty(pid))
                return new ReturnResult<List<ProductMediaModel>>((int)ErrorCodeEnum.Error_NoData, null, "参数为空!");

            List<ProductMediaModel> result = _dbContext.Queryable<product_media>()
                .Where(a => a.productid == pid)
                .OrderBy(a=>a.order,OrderByType.Asc)
                .Select(a => new ProductMediaModel()
                {
                    id = a.id,
                    mediaUrl = a.media_url,
                    mediaType = a.media_type,
                    displayOrder = a.order
                }).ToList();

            if (result != null && result.Count > 0)
                return new ReturnResult<List<ProductMediaModel>>(result);
            else
                return new ReturnResult<List<ProductMediaModel>>((int)ErrorCodeEnum.Error_SqlData, null, "未查到数据!");
        }
        /// <summary>
        /// 修改产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult UpdateProductMediaInfo(string platKey, UpdateProdMediaModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Error_NoData, "参数为空!");

            product_media media = new product_media()
            {
                id=model.id,
                platkey = platKey,
                productid = model.productId,
                media_type = model.mediaType,
                media_url = model.mediaUrl,
                order = model.displayOrder
            };
            int result = _dbContext.Updateable(media).ExecuteCommand();

            if (result > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");
        }

        /// <summary>
        /// 删除产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult DeleteProductMediaInfo(string platKey, int id)
        {
            if (id <=0)
                return new ReturnResult((int)ErrorCodeEnum.Error_NoData, "参数为空!");

            int result = _dbContext.Deleteable<product_media>(id).ExecuteCommand();
            if (result > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");
        }

        /// <summary>
        /// 新增产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<int> AddProductMediaInfo(string platKey, AddProductMediaModel model)
        {
            if (model == null)
                return new ReturnResult<int>((int)ErrorCodeEnum.Error_NoData,0, "参数为空!");

            product_media media = new product_media()
            {
                platkey = platKey,
                productid = model.productId,
                media_type = model.mediaType,
                media_url = model.mediaUrl,
                order = model.displayOrder
            };
            int result = _dbContext.Insertable(media).ExecuteReturnIdentity();

            if (result > 0)
                return new ReturnResult<int>(result);
            else
                return new ReturnResult<int>((int)ErrorCodeEnum.Error_SqlData, 0, "新增失败!");
        }

        #endregion 媒体信息

        #region 基本信息

        /// <summary>
        /// 查询产品基本信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<AuthProductModel> QueryProductPrimaryInfo(string platKey, string pid)
        {
            if (string.IsNullOrEmpty(pid))
                return new ReturnResult<AuthProductModel>((int)ErrorCodeEnum.Error_NoData,null, "参数为空!");
            List<AuthProductModel> result = _dbContext.Queryable<product>()
                .Where(a=>a.id==pid).Select(a=>new AuthProductModel() {
                    goodsName=a.name,
                    salesPrice=a.sales_price,
                    shortDesc=a.short_desc,
                    description=a.description,
                    isPublish=a.is_publish,
                    marketPrice=a.market_price,
                    price=a.price
                }).ToList();
            if (result!=null && result.Count > 0)
                return new ReturnResult<AuthProductModel>(result[0]);
            else
                return new ReturnResult<AuthProductModel>((int)ErrorCodeEnum.Error_SqlData,null, "未查到数据!");
        }

        /// <summary>
        /// 修改产品基本信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult ModifyProductPrimaryInfo(string platKey, UpdatePrimaryModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Error_NoData, "参数为空!");
            product item = new product()
            {
                id = model.id,
                name = model.goodsName,
                platkey = platKey,
                sales_price = model.salesPrice,
                market_price = model.marketPrice,
                price = model.price,
                is_publish = model.isPublish,
                short_desc = model.shortDesc,
                description = model.description,
                addtime = DateTime.Now
            };
            int result = _dbContext.Updateable(item).ExecuteCommand();
            if (result > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");
        }

        #endregion 基本信息

        /// <summary>
        /// 修改产品属性信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult ModifyProductAttrInfo(string platKey, List<UpdateProductAttrModel> model)
        {
 

            return new ReturnResult();
        }

        /// <summary>
        /// 修改产品sku信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult ModifyProductSku(string platKey, List<UpdateSkuModel> model)
        {
            return new ReturnResult();
        }

        



        #endregion
    }
}
