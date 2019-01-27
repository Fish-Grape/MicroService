using Feng.Basic;
using Feng.Core;
using Feng.DbContext;
using Feng.Order.Entity;
using Feng.Order.IService;
using Feng.Order.Model;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Feng.Order.Service
{
    public partial class OrderService : IOrderService
    {
        public readonly IRepositoryBase<orders> _repositoryBase;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        private readonly ILogger<OrderService> logger;

        public OrderService(IRepositoryBase<orders> repositoryBase, IJsonHelper jsonHelper, IUser user, ILogger<OrderService> logger)
        {
            _repositoryBase = repositoryBase;
            _jsonHelper = jsonHelper;
            _user = user;
            this.logger = logger;
        }

        public ReturnResult ApproveOrder(string platkey, ApproveOrdersModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");
            orders item = new orders
            {
                id = model.id,
                order_status_id = model.order_statusId,
                order_status=model.order_status,
                approve_by = model.approve_by,
                approve_time = DateTime.Now
            };
            int count = _repositoryBase.DbContext.Updateable(item).
                UpdateColumns(it => new {
                    it.order_status_id,
                    it.order_status,
                    it.approve_by,
                    it.approve_time
                }).ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult ModifyOrder(string platkey, ModifyOrdersModel model)
        {
            var result=_repositoryBase.DbContext.Ado.UseTran(() =>
            {
                o_userinfo user = new o_userinfo()
                {
                    email = model.userInfo.email,
                    phone = model.userInfo.phone,
                    province = model.delivery.province,
                    city = model.delivery.city,
                    area = model.delivery.area,
                    address = model.delivery.address,
                    region_id = model.delivery.region_id,
                    receive_name=model.userInfo.receive_name,
                    zip=model.delivery.zip,
                };

                 _repositoryBase.DbContext.Updateable(user)
                    .UpdateColumns(a => new {
                        a.email,
                        a.phone,
                        a.province,
                        a.city,
                        a.area,
                        a.address,
                        a.region_id,
                        a.zip,
                        a.receive_name,
                    }).Where(a => a.order_id == model.id).ExecuteCommand();
                _repositoryBase.DbContext.Updateable<orders>()
                .UpdateColumns(a => new orders() {
                    remark = model.userInfo.remark
                })
                .Where(a => a.id == model.id).ExecuteCommand();
            });

            if (!result.Data)
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");
            return new ReturnResult();
        }

        public ReturnResult<OrdersModel> QueryOrderById(string platkey, string id)
        {
            OrdersInfoModel orders=_repositoryBase.DbContext.Queryable<orders>()
                .Where(st => st.platkey == platkey)
                         .WhereIF(!string.IsNullOrEmpty(id), st => st.id == id)
                         .OrderBy(st => st.create_time, OrderByType.Desc)
                         .Select(st => new OrdersInfoModel()
                         {
                             id = st.id,
                             approve_by = st.approve_by,
                             approve_time = st.approve_time,
                             final_price = st.final_price,
                             order_status = st.order_status,
                             order_status_id = st.order_status_id,
                             close_time = st.close_time,
                             create_time = st.create_time,
                             pay_status = st.pay_status,
                             end_time = st.end_time,
                             pay_status_id = st.pay_status_id,
                             original_price = st.original_price,
                             pay_method = st.pay_method,
                             pay_method_id = st.pay_method_id,
                             pay_time = st.pay_time,
                             update_time = st.update_time
                         }).First();

            QueryUserModel userinfo = _repositoryBase.DbContext.Queryable<orders, o_userinfo>((a, b) => new object[] {
                JoinType.Left,a.id==b.order_id
            })
                          .Where((a, b) => a.platkey == platkey)
                          .WhereIF(!string.IsNullOrEmpty(id), a => a.id == id)
                          .OrderBy((a, b) => a.create_time, OrderByType.Desc)
                          .Select((a, b) => new QueryUserModel()
                          {
                              user_id = a.user_id,
                              email = b.email,
                              phone = b.phone,
                              remark = a.remark,
                              user_name = a.user_name,
                              user_real_name=a.user_real_name,
                              receive_name=b.receive_name
                          }).First();

            Delivery delivery=_repositoryBase.DbContext.Queryable<o_userinfo>()
                .WhereIF(!string.IsNullOrEmpty(id), sc => sc.order_id == id)
                .Select(sc => new Delivery()
                {
                    region_id = sc.region_id,
                    address = sc.address,
                    area = sc.area,
                    city = sc.city,
                    province = sc.province,
                    zip = sc.zip
                }).First();

            OrdersModel item = new OrdersModel();
            item.delivery = delivery;
            item.ordersInfo = orders;
            item.queryUser = userinfo;

            if (item == null)
                return new ReturnResult<OrdersModel>((int)ErrorCodeEnum.Error_NoData, null, "没有找到数据!");
            List<OrdersDetailModel> details = _repositoryBase.DbContext.Queryable<o_product>()
               .Where(a => a.platkey == platkey)
               .Where(a => a.order_id == id)
               .Select(a => new OrdersDetailModel
               {
                   category = a.category,
                   product_name = a.product_name,
                   product_image = a.product_image,
                   short_desc = a.short_desc,
                   description = a.description,
                   final_price = a.final_price,
                   original_price = a.original_price,
                   attr_json = a.attr_json,
               }).ToList();

            if (details != null && details.Count > 0)
            {
                details.ForEach(x =>
                 {
                     item.OrdersDetailsList.Add(x);
                 });
            }
            return new ReturnResult<OrdersModel>(item);
        }

        public ReturnResult<List<OrdersPrimaryModel>> QueryOrders(string platkey, int userId=-1 , int order_status = -1)
        {
            List<OrdersPrimaryModel> list = GetModelList(platkey, userId, order_status).ToList();
            if (list == null || list.Count == 0)
                return new ReturnResult<List<OrdersPrimaryModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有找到数据!");
            return new ReturnResult<List<OrdersPrimaryModel>>(list);
        }

        public ReturnResult<PageList<OrdersPrimaryModel>> QueryOrdersList(string platkey, int pageSize, int pageIndex,int order_status = -1, int userId = -1)
        {
            int totalNumber = 0;
            List<OrdersPrimaryModel> list = GetModelList(platkey,userId,order_status).ToPageList(pageIndex, pageSize, ref totalNumber);
            if (list == null || list.Count == 0)
                return new ReturnResult<PageList<OrdersPrimaryModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有找到数据!");
            PageList<OrdersPrimaryModel> page = new PageList<OrdersPrimaryModel>();
            page.TotalCount = totalNumber;
            page.PageSize = pageSize;
            page.Page = pageIndex;
            list.ForEach(x => {
                page.DataList.Add(x);
            });

            return new ReturnResult<PageList<OrdersPrimaryModel>>(page);
        }

        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="platkey"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private ISugarQueryable<OrdersPrimaryModel> GetModelList(string platkey, int userId = -1, int order_status = -1)
        {
            var result = _repositoryBase.DbContext.Queryable<orders, or_status>((a,b)=> new object[]{
                JoinType.Left,a.order_status_id==b.status_id && b.status_type=="OrderStatus"
            })
                .WhereIF(userId != -1, a => a.user_id == userId)
                .WhereIF(order_status !=-1, a=>a.order_status_id == order_status)
                .Where((a, b) => a.platkey == platkey)
                .OrderBy((a, b) => a.create_time, OrderByType.Desc)
                .Select((a, b) => new OrdersPrimaryModel
                {
                    id = a.id,
                    user_id = a.user_id,
                    user_name = a.user_name,
                    order_statusId=b.status_id,
                    order_status = b.status_val,
                    final_price = a.final_price,
                    create_time = a.create_time,
                });
            return result;
        }

        /// <summary>
        /// 获取状态信息
        /// </summary>
        /// <param name="status_id"></param>
        /// <param name="status_type"></param>
        /// <returns></returns>
        public string GetStatusByIdAndType(int status_id,string status_type)
        {
            return _repositoryBase.DbContext.Queryable<or_status>()
                .Where(a => a.status_id == status_id && a.status_type == status_type)
                .Select(a => a.status_val).First();
        }


    }
}
