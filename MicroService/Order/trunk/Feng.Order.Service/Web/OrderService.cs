using Feng.Basic;
using Feng.Core;
using Feng.DbContext;
using Feng.Order.Entity;
using Feng.Order.IService;
using Feng.Order.Model;
using Feng.Utils.Helpers;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Feng.Order.Service
{
    public partial class OrderService : IOrderService
    {
        public ReturnResult<ReturnOrdersWebModel> AddOrder(string platkey, AddOrdersWebModel model)
        {
            if (model == null)
                return new ReturnResult<ReturnOrdersWebModel>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");
            string strId = Id.ObjectId();
            var result = _repositoryBase.DbContext.Ado.UseTran(() =>
             {
                 orders item = new orders
                 {
                     id = strId,
                     platkey = platkey,
                     user_id = model.orderPrimary.user_id,
                     user_name = model.orderPrimary.user_name,
                     user_real_name=model.orderPrimary.realUserName,
                     buy_method=model.orderPrimary.buy_method,
                     buy_period=model.orderPrimary.buy_period,
                     remark = model.orderPrimary.remark,
                     is_disable = 0,
                     order_status_id=5,
                     order_status = "未确认",
                     pay_method_id=5,
                     pay_status = "未支付",
                     approve_by = "",
                     pay_method = "",
                     final_price = model.orderPrimary.final_price,
                     original_price = model.orderPrimary.original_price,
                     create_time = DateTime.Now,
                     update_time = DateTime.Now,
                     pay_time = null,
                     approve_time = null,
                     end_time = DateTime.Now.AddYears(model.orderPrimary.buy_period),
                     close_time = null
                 };
 
                 List<o_product> details = new List<o_product>();
                 List<o_ext_attr> ext_Attrs= new List<o_ext_attr>();
                 o_userinfo userinfo = new o_userinfo() {
                     o_userinfo_id=Id.ObjectId(),
                     platkey=platkey,
                     order_id=strId,
                     address=model.addO_UserInfoWeb.address,
                     area=model.addO_UserInfoWeb.area,
                     city= model.addO_UserInfoWeb.city,
                     email= model.addO_UserInfoWeb.email,
                     phone= model.addO_UserInfoWeb.phone,
                     province= model.addO_UserInfoWeb.province,
                     receive_name= model.addO_UserInfoWeb.receive_name,
                     zip= model.addO_UserInfoWeb.zip,
                     region_id= model.addO_UserInfoWeb.region_id
                 };
                 model.addOrdersDetailsList.ForEach(x =>
                 {
                     x.addO_Products.ForEach(p => {
                         details.Add(new o_product() {
                             id = Id.ObjectId(),
                             skuid = p.skuid,
                             order_id = strId,
                             category = p.category,
                             attr_json = p.attr_json,
                             final_price = p.final_price,
                             sku_json = p.sku_json,
                             short_desc = p.short_desc,
                             original_price = p.original_price,
                             description = p.description,
                             platkey = platkey,
                             product_image = p.product_image,
                             product_name = p.product_name,
                             product_id=p.product_id
                         });
                         p.addO_Ext_AttrfoWebs.ForEach(e =>
                         {
                             ext_Attrs.Add(new o_ext_attr() {
                                 id = Id.ObjectId(),
                                 skuid = e.skuid,
                                 extend_attr_id = e.extend_attr_id,
                                 extend_attr_name = e.extend_attr_name,
                                 order_id = strId,
                                 platkey = platkey
                             });
                         });
                     });
                 });
                 var sql=_repositoryBase.DbContext.Insertable(item).ToSql();
                 //新增主订单
                 _repositoryBase.DbContext.Insertable(item).ExecuteCommand();
                 //新增订单用户
                 _repositoryBase.DbContext.Insertable(userinfo).ExecuteCommand();
                 //新增订单产品
                 _repositoryBase.DbContext.Insertable(details).ExecuteCommand();
                 //新增订单附加属性
                 _repositoryBase.DbContext.Insertable(ext_Attrs).ExecuteCommand();
             }
            );
            if (result.Data)
                return new ReturnResult<ReturnOrdersWebModel>(new ReturnOrdersWebModel() { id = strId });
            else
                return new ReturnResult<ReturnOrdersWebModel>(new ReturnOrdersWebModel() { id = "" });
        }


        public ReturnResult CancelOrder(string platkey, CancelOrdersModel model)
        {
            var result = _repositoryBase.DbContext.Ado.UseTran(() =>
            {
                orders order = _repositoryBase.DbContext.Queryable<orders>()
                .Where(a => a.id == model.id).First();

                if (order.pay_status_id != 0)
                {
                    throw new Exception("订单状态不为待支付，无法取消订单！");
                }
                _repositoryBase.DbContext.Updateable<orders>()
                .UpdateColumns(a => new orders
                {
                    order_status_id = 0,
                    order_status = "已关闭",
                    id = model.id,
                    remark = "用户取消订单",
                    is_disable = 1,
                    close_time = DateTime.Now,
                    update_time = DateTime.Now
                });
            });

            if (result.Data)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, result.ErrorMessage);
        }

        public ReturnResult DeleteOrder(string platkey, string id)
        {
            var result=_repositoryBase.DbContext.Ado.UseTran(() =>
            {
                orders order = _repositoryBase.DbContext.Queryable<orders>()
                .Where(a => a.id == id).First();

                if (order.order_status_id != 0)
                {
                    throw new Exception("订单状态不为已关闭，无法删除！");
                }
                _repositoryBase.DbContext.Updateable<orders>()
                .UpdateColumns(a => new orders
                {
                    order_status_id = -1,
                    order_status="已删除",
                    id = id,
                    remark = "用户删除订单",
                    is_disable = 1,
                    close_time = DateTime.Now,
                    update_time = DateTime.Now
                });
            });

            if (result.Data)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, result.ErrorMessage);
        }

        public ReturnResult ModifySku(string platkey, ModifySkuModel modifySku)
        {
            if(modifySku==null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数为空!");
            var result=_repositoryBase.DbContext.Ado.UseTran(() =>
            {
                List<o_ext_attr> ext_Attrs = new List<o_ext_attr>();
                modifySku.addO_Ext_Attrfos.ForEach(a =>
                {
                    ext_Attrs.Add(new o_ext_attr() {
                        skuid=a.skuid,
                        extend_attr_id=a.extend_attr_id,
                        extend_attr_name=a.extend_attr_name,
                        order_id=modifySku.order_id,
                    });
                });
                 _repositoryBase.DbContext.Updateable(ext_Attrs)
                    .UpdateColumns(a => new
                    {
                        a.extend_attr_id,
                        a.extend_attr_name,
                    }).WhereColumns(a => a.order_id)
                    .WhereColumns(a=>a.skuid)
                    .ExecuteCommand();

            });

            if (result.Data)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, result.ErrorMessage);
        }
    }
}
