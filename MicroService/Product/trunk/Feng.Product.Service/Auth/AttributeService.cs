﻿using Feng.Basic;
using Feng.Core;
using Feng.Core.Config;
using Feng.Product.Entity;
using Feng.Product.IService;
using Feng.Product.Model;
using Feng.Product.Model.Background;
using Feng.Redis;
using Feng.Utils.Helpers;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feng.Product.Service
{
    public class AttributeService : IAttributeService
    {
        public readonly SqlSugarClient _dbContext;
        private readonly IJsonHelper _jsonHelper;
        public readonly IConfig _config;
        private readonly RedisClient _redisClient;
        private readonly IDatabase redis;

        public AttributeService(SqlSugarClient dbContext, IJsonHelper jsonHelper, IConfig config, RedisClient redisClient)
        {
            _dbContext = dbContext;
            _jsonHelper = jsonHelper;
            _config = config;
            _redisClient = redisClient;
            var rediscontect = _config.StringGet("product_redis_connectString");
            redis = _redisClient.GetDatabase(rediscontect, 3);
        }

        public ReturnResult<dynamic> AddAttributeGroup(string platKey, AddAttributeGroupModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            attribute_group item = new attribute_group()
            {
                id = Id.ObjectId(),
                categoryid = model.categoryId,
                name = model.groupName,
                order = model.displayOrder,
                platkey = platKey
            };

            int result = _dbContext.Insertable(item).ExecuteCommand();

            if (result <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_SqlData, null, "添加失败!");


            return new ReturnResult<dynamic>(new { item.id });

        }
        public ReturnResult ModifyAttributeGroup(string platKey, AttributeGroupModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");


            int result = _dbContext.Updateable<attribute_group>()
                .UpdateColumns(a => new attribute_group() { name = model.groupName, order = model.displayOrder })
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == model.id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "更新失败!");

            return new ReturnResult();
        }
        public ReturnResult<List<AttributeGroupModel>> QueryAttributeGroup(string platKey, string categoryId)
        {
            var list = _dbContext.Queryable<attribute_group>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.categoryid == categoryId)
                .OrderBy(x => x.order, OrderByType.Asc)
                .ToList();

            if (list != null && list.Count > 0)
            {
                List<AttributeGroupModel> item = new List<AttributeGroupModel>();
                list.ForEach(a =>
                {
                    item.Add(new AttributeGroupModel()
                    {
                        id = a.id,
                        groupName = a.name,
                        displayOrder = a.order
                    });
                });
                return new ReturnResult<List<AttributeGroupModel>>(item);
            }
            else
            {
                return new ReturnResult<List<AttributeGroupModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有查到数据!");
            }

        }
        public ReturnResult RemoveAttributeGroup(string platKey, string id)
        {
            bool exsit = _dbContext.Queryable<attribute>()
                .Where(a => a.groupid == id)
                .Where(a => a.platkey == platKey)
                .Count() > 0;

            if (exsit)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "该项已被属性使用,请先清除关联项!");

            int result = _dbContext.Deleteable<attribute_group>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "删除失败!");

            return new ReturnResult();
        }





        public ReturnResult<dynamic> AddAttribute(string platKey, AddAttributeModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            attribute item = new attribute()
            {
                id = Id.ObjectId(),
                categoryid = model.categoryId,
                name = model.AttributeName,
                order = model.displayOrder,
                groupid = model.groupId,
                show_type = model.showType,
                is_filter = model.isFilter,
                is_multi = model.isMulti,
                is_must = model.isMust,
                is_sales = model.isSales,
                is_search = model.isSearch,
                platkey = platKey
            };

            int result = _dbContext.Insertable(item).ExecuteCommand();

            if (result <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_SqlData, null, "添加失败!");


            return new ReturnResult<dynamic>(new { item.id });
        }
        public ReturnResult ModifyAttribute(string platKey, AttributeModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");


            int result = _dbContext.Updateable<attribute>()
                .UpdateColumns(a => new attribute()
                {
                    name = model.AttributeName,
                    order = model.displayOrder,
                    groupid=model.groupId,
                    show_type = model.showType,
                    is_filter = model.isFilter,
                    is_multi = model.isMulti,
                    is_must = model.isMust,
                    is_sales = model.isSales,
                    is_search = model.isSearch
                })
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == model.id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "更新失败!");

            return new ReturnResult();
        }
        public ReturnResult<List<AttributeModel>> QueryAttribute(string platKey, string categoryId)
        {
            var list = _dbContext.Queryable<attribute,attribute_group>((a, b) =>
                  new object[]{
                    JoinType.Left,a.groupid==b.id
                  })
                .Where((a, b) => a.platkey == platKey)
                .Where((a, b) => a.categoryid == categoryId)
                .OrderBy((a, b) => a.order, OrderByType.Asc)
                .Select((a,b)=>new AttributeModel() {
                    id = a.id,
                    AttributeName = a.name,
                    showType = a.show_type,
                    groupId = a.groupid,
                    groupName=b.name,
                    isFilter = a.is_filter,
                    isMulti = a.is_multi,
                    isMust = a.is_must,
                    isSales = a.is_sales,
                    isSearch = a.is_search,
                    displayOrder = a.order
                })
                .ToList();

            if (list != null && list.Count > 0)
            { 
                return new ReturnResult<List<AttributeModel>>(list);
            }
            else
            {
                return new ReturnResult<List<AttributeModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有查到数据!");
            }
        }
        public ReturnResult RemoveAttribute(string platKey, string id)
        {
            bool exsit = _dbContext.Queryable<attribute_values>()
                .Where(a => a.attributeid == id)
                .Where(a => a.platkey == platKey)
                .Count() > 0;

            if (exsit)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "该项存在多个属性值,请先清除关联项!");

            int result = _dbContext.Deleteable<attribute>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "删除失败!");

            return new ReturnResult();
        }





        public ReturnResult<dynamic> AddAttributeValue(string platKey, AddAttributeValueModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            attribute_values item = new attribute_values()
            {
                id = Id.ObjectId(),
                attributeid = model.attributeId,
                name = model.valueName,
                image_url = model.valueImage,
                order = model.displayOrder,
                platkey = platKey
            };

            int result = _dbContext.Insertable(item).ExecuteCommand();

            if (result <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_SqlData, null, "添加失败!");


            return new ReturnResult<dynamic>(new { item.id });
        }
        public ReturnResult ModifyAttributeValue(string platKey, AttributeValueModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");


            int result = _dbContext.Updateable<attribute_values>()
                .UpdateColumns(a => new attribute_values()
                {
                    name = model.valueName,
                    image_url = model.valueImage,
                    order = model.displayOrder
                })
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == model.id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "更新失败!");

            return new ReturnResult();
        }
        public ReturnResult<List<AttributeValueModel>> QueryAttributeValue(string platKey, string attributeId)
        {
            var list = _dbContext.Queryable<attribute_values>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.attributeid == attributeId)
                .OrderBy(x => x.order, OrderByType.Asc)
                .ToList();

            if (list != null && list.Count > 0)
            {
                List<AttributeValueModel> item = new List<AttributeValueModel>();
                list.ForEach(a =>
                {
                    item.Add(new AttributeValueModel()
                    {
                        id = a.id,
                        valueName = a.name,
                        valueImage = a.image_url,
                        displayOrder = a.order
                    });
                });
                return new ReturnResult<List<AttributeValueModel>>(item);
            }
            else
            {
                return new ReturnResult<List<AttributeValueModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有查到数据!");
            }
        }
        public ReturnResult RemoveAttributeValue(string platKey, string id)
        {
            int result = _dbContext.Deleteable<attribute_values>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "删除失败!");
            return new ReturnResult();
        }

        public ReturnResult<List<ProductAttrModel>> QueryAttributeAndVal(string platKey, string categoryId)
        {
            List<ProductAttrModel> list = new List<ProductAttrModel>();
            //var kv = redis.StringGet("QueryAttributeAndVal_" + categoryId);
            //if (!kv.IsNullOrEmpty)
            //    return new ReturnResult<List<ProductAttrModel>>(_jsonHelper.DeserializeObject<List<ProductAttrModel>>(kv));

            list = _dbContext.Queryable<attribute>()
              .Where(a => a.platkey == platKey)
              .Where(a => a.categoryid == categoryId)
              .OrderBy(a => a.order, OrderByType.Asc)
              .Select(a => new ProductAttrModel
              {
                  attributeId = a.id,
                  AttributeName = a.name,
                  isSales = a.is_sales,
                  isMulti = a.is_multi,
                  isMust = a.is_must
              })
              .ToList();

            if (list != null && list.Count > 0)
            {
                var values=_dbContext.Queryable<attribute_values>()
                    .In(a => a.attributeid, (from x in list.ToArray() select x.attributeId).ToArray())
                    .Select(a => new AttributeValueModel() {
                        id=a.id,
                        attributeId=a.attributeid,
                        valueName=a.name
                    }).ToList();

                list.ForEach(a=> {
                    a.attributeValues = values.FindAll(x => x.attributeId == a.attributeId);
                });
                //存入redis
                //redis.StringSet("QueryAttributeAndVal_" + categoryId, _jsonHelper.SerializeObject(list));
                return new ReturnResult<List<ProductAttrModel>>(list);
            }
            else
            {
                return new ReturnResult<List<ProductAttrModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有查到数据!");
            }
        }


        #region 附加属性值
        public ReturnResult<dynamic> AddExtendAttribute(string platKey, AddExtendAttrModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            extend_attr item = new extend_attr()
            {
                id = Id.ObjectId(),
                ex_type = (int)model.ExType,
                ex_name = model.ExtendName,
                ex_id = model.ExtendId,
                add_price = model.AddPrice,
                create_time=DateTime.Now,
                platkey = platKey
            };

            int result = _dbContext.Insertable(item).ExecuteCommand();

            if (result <= 0)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_SqlData, null, "添加失败!");

            return new ReturnResult<dynamic>(new { item.id });
        }

        public ReturnResult<List<ExtendAttrModel>> QueryExtendAttr(string platKey, string ex_id)
        {
            List<ExtendAttrModel> list = new List<ExtendAttrModel>();

            list = _dbContext.Queryable<extend_attr>()
              .Where(a => a.platkey == platKey)
              .Where(a => a.ex_id == ex_id)
              .OrderBy(a => a.create_time, OrderByType.Desc)
              .Select(a => new ExtendAttrModel
              {
                  id=a.id,
                  ExtendId=a.ex_id,
                  AddPrice=a.add_price,
                  ExtendName=a.ex_name,
                  ExType=(ExtendType)a.ex_type,
              })
              .ToList();

            if (list != null && list.Count > 0)
            {
                return new ReturnResult<List<ExtendAttrModel>>(list);
            }
            else
            {
                return new ReturnResult<List<ExtendAttrModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有查到数据!");
            }
        }

        public ReturnResult RemoveExtendAttr(string platKey, string id)
        {
            int result = _dbContext.Deleteable<extend_attr>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Failed, "删除失败!");

            return new ReturnResult();
        }

        public ReturnResult ModifyExtendAttr(string platKey, ExtendAttrModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");
            int exType = (int)model.ExType;
            int result = _dbContext.Updateable<extend_attr>()
                .UpdateColumns(a => new extend_attr()
                {
                    id = model.id,
                    ex_type = exType,
                    ex_name = model.ExtendName,
                    ex_id = model.ExtendId,
                    add_price = model.AddPrice,
                })
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == model.id)
                .ExecuteCommand();

            if (result <= 0)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "更新失败!");

            return new ReturnResult();
        }
        #endregion
    }
}
