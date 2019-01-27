using Feng.Basic;
using Feng.Product.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Product.IService
{
    public partial interface IProductService
    {

        #region 认证服务
        /// <summary>
        /// 新增认证产品（媒体信息单独新增）
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult<dynamic> AddAuthProduct(string platKey, AddAuthProductModel model);

        ReturnResult<List<QueryAuthProductModel>> QueryAuthProductList(string platKey,string cid);

        #endregion



        /// <summary>
        /// 新增产品（媒体信息单独新增）
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult<dynamic> AddProduct(string platKey, AddProductModel model);

        /// <summary>
        /// 分页查询产品列表
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="productName"></param>
        /// <param name="cid"></param>
        /// <param name="brandName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isPublish"></param>
        /// <returns></returns>
        ReturnResult<PageList<QueryProductModel>> QueryProductList(string platKey,int pageSize,int pageIndex,string productName,string cid,string brandName, string startDate, string endDate,int isPublish);

        /// <summary>
        /// 根据id查询产品
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="id"></param>
        /// <param name="isWeb"></param>
        /// <returns></returns>
        ReturnResult<ProductModel> QueryProductById(string platKey, string id,bool isWeb=false);


        #region 修改产品
        /// <summary>
        /// 修改产品状态
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="pid"></param>
        /// <param name="isPublish"></param>
        /// <returns></returns>
        ReturnResult ModifyProductStatus(string platKey, ModifyProductStatus model);

        /// <summary>
        /// 查询产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult<List<ProductMediaModel>> QueryProductMediaInfo(string platKey, string pid);

        /// <summary>
        /// 修改产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult UpdateProductMediaInfo(string platKey, UpdateProdMediaModel model);

        /// <summary>
        /// 删除产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult DeleteProductMediaInfo(string platKey, int id);

        /// <summary>
        /// 新增产品媒体信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult<int> AddProductMediaInfo(string platKey, AddProductMediaModel model);

        /// <summary>
        /// 修改产品基本信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult ModifyProductPrimaryInfo(string platKey, UpdatePrimaryModel model);

        /// <summary>
        /// 查询产品基本信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        ReturnResult<AuthProductModel> QueryProductPrimaryInfo(string platKey, string pid);


        /// <summary>
        /// 修改产品属性信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult ModifyProductAttrInfo(string platKey, List<UpdateProductAttrModel> model);

        /// <summary>
        /// 修改产品sku信息
        /// </summary>
        /// <param name="platKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnResult ModifyProductSku(string platKey, List<UpdateSkuModel> model);

        #endregion
    }
}
