using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Product.Controllers
{
    /// <summary>
    /// 品牌管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandService"></param>
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("add")]
        public ReturnResult<dynamic> AddBrand([FromBody]AddBrandModel model) {
            string platKey = Utils.Extensions.Plat;
            return _brandService.AddBrand(platKey, model);
        }
        /// <summary>
        /// 修改品牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("modify")]
        public ReturnResult ModifyBrand([FromBody]BrandModel model)
        {
            string platKey = Utils.Extensions.Plat;
            return _brandService.ModifyBrand(platKey, model);
        }
        /// <summary>
        /// 修改品牌状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost,Route("status/{id}")]
        public ReturnResult ModifyBrandStatus(int id, bool status)
        {
            string platKey = Utils.Extensions.Plat;
            return _brandService.ModifyBrandStatus(platKey, id,status);
        }
        /// <summary>
        /// 分页查询品牌
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="name">品牌名称</param>
        /// <returns></returns>
        [HttpGet,Route("query")]
        public ReturnResult<PageList<BrandModel>> QueryBrand(int pageIndex, int pageSize, string name="")
        {
            string platKey = Utils.Extensions.Plat;
            return _brandService.QueryBrand(platKey, pageIndex, pageSize, name);
        }
    }
}