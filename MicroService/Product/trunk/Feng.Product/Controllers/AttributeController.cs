﻿using System.Collections.Generic;
using Feng.Basic;
using Feng.Product.IService;
using Feng.Product.Model;
using Feng.Product.Model.Background;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Product.Controllers
{
    /// <summary>
    /// 属性管理
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AttributeController : ControllerBase
    {
        public readonly IAttributeService _attributeService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeService"></param>
        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }
        /// <summary>
        /// 添加属性分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("group/add")]
        public ReturnResult<dynamic> AddAttributeGroup([FromBody]AddAttributeGroupModel model) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.AddAttributeGroup(platKey, model);
        }
        /// <summary>
        /// 根据分类编号查询属性分组列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet,Route("group/query")]
        public ReturnResult<List<AttributeGroupModel>> QueryAttributeGroup(string categoryId) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.QueryAttributeGroup(platKey, categoryId);
        }
        /// <summary>
        /// 根据编号删除属性分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("group/del")]
        public ReturnResult RemoveAttributeGroup(string id) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.RemoveAttributeGroup(platKey, id);
        }
        /// <summary>
        /// 修改属性分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("group/modify")]
        public ReturnResult ModifyAttributeGroup([FromBody]AttributeGroupModel model) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.ModifyAttributeGroup(platKey, model);
        }


        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        public ReturnResult<dynamic> AddAttribute([FromBody]AddAttributeModel model) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.AddAttribute(platKey, model);
        }
        /// <summary>
        /// 根据分类编号查询属性列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet, Route("query")]
        public ReturnResult<List<AttributeModel>> QueryAttribute(string categoryId) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.QueryAttribute(platKey, categoryId);
        }
        /// <summary>
        /// 根据分类编号查询属性/属性值列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet, Route("AttrAndVal")]
        public ReturnResult<List<ProductAttrModel>> QueryAttributeAndVal(string categoryId)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.QueryAttributeAndVal(platKey, categoryId);
        }
        /// <summary>
        /// 根据编号删除属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("del")]
        public ReturnResult RemoveAttribute(string id) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.RemoveAttribute(platKey, id);
        }
        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("modify")]
        public ReturnResult ModifyAttribute([FromBody]AttributeModel model) {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.ModifyAttribute(platKey, model);
        }





        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("value/add")]
        public ReturnResult<dynamic> AddAttributeValue([FromBody]AddAttributeValueModel model)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.AddAttributeValue(platKey, model);
        }
        /// <summary>
        /// 根据属性编号查询属性值列表
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        [HttpGet, Route("value/query")]
        public ReturnResult<List<AttributeValueModel>> QueryAttributeValue(string attributeId)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.QueryAttributeValue(platKey, attributeId);
        }
        /// <summary>
        /// 根据编号删除属性值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("value/del")]
        public ReturnResult RemoveAttributeValue(string id)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.RemoveAttributeValue(platKey, id);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("value/modify")]
        public ReturnResult ModifyAttributeValue([FromBody]AttributeValueModel model)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.ModifyAttributeValue(platKey, model);
        }




        #region 附加属性

        /// <summary>
        /// 添加附加属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("ext/add")]
        public ReturnResult<dynamic> AddExtendAttribute([FromBody]AddExtendAttrModel model)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.AddExtendAttribute(platKey, model);
        }
        /// <summary>
        /// 根据关联id查询附加属性
        /// </summary>
        /// <param name="extId"></param>
        /// <returns></returns>
        [HttpGet, Route("ext/query")]
        public ReturnResult<List<ExtendAttrModel>> QueryExtendAttr(string extId)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.QueryExtendAttr(platKey, extId);
        }
        /// <summary>
        /// 根据编号删除附加属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("ext/del")]
        public ReturnResult RemoveExtendAttr(string id)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.RemoveExtendAttr(platKey, id);
        }
        /// <summary>
        /// 修改附加属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("ext/modify")]
        public ReturnResult ModifyExtendAttr([FromBody]ExtendAttrModel model)
        {
            string platKey = Utils.Extensions.Plat;
            return _attributeService.ModifyExtendAttr(platKey, model);
        }
        #endregion
    }
}