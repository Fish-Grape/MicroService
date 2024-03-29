﻿using System;
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
    /// 产品分类管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="model">分类模型</param>
        /// <returns></returns>
        [HttpPost,Route("add")]
        public ReturnResult<dynamic> AddCategoryAsync([FromBody]AddCategoryModel model) {
            string platKey = Utils.Extensions.Plat;
            return _categoryService.AddCategory(platKey,model);

        }
        /// <summary>
        /// 修改产品分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("modify")]
        public ReturnResult ModifyCategory([FromBody]CategoryModel model) {
            string platKey = Utils.Extensions.Plat;
            return _categoryService.ModifyCategory(platKey,model);
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet, Route("del")]
        public ReturnResult RemoveCategory(string cid) {
            string platKey = Utils.Extensions.Plat;
            return _categoryService.RemoveCategory(platKey,cid);
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("query")]
        public ReturnResult<List<ReturnListCategoryModel>> QueryCategory() {
            string platKey = Utils.Extensions.Plat;
            return _categoryService.QueryCategory(platKey);
        }

    }
}