using System.Collections.Generic;
using Feng.Basic;
using Feng.Order.IService;
using Feng.Order.Model;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Order.web.Controllers.v1
{
    /// <summary>
    /// 订单管理
    /// </summary>
    [ApiVersion("1.0")]
    [Route("{aera}/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersWebController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersWebController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="order_status"></param>
        /// <returns></returns>
        [HttpGet, Route("querylist")]
        public ReturnResult<List<OrdersPrimaryModel>> QueryOrder(int userId, int order_status = -1)
        {
            if (userId <= 0)
                return new ReturnResult<List<OrdersPrimaryModel>>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常！请输入用户id!");
            return orderService.QueryOrders(Utils.Extensions.Plat);
        }

        /// <summary>
        /// 分页查询订单
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <param name="order_status"></param>
        /// <returns></returns>
        [HttpGet, Route("query_pagelist")]
        public ReturnResult<PageList<OrdersPrimaryModel>> QueryPageOrder(int pageSize, int pageIndex, int userId, int order_status = -1)
        {
            if (userId <= 0)
                return new ReturnResult<PageList<OrdersPrimaryModel>>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常！请输入用户id!");
            return orderService.QueryOrdersList(Utils.Extensions.Plat, pageSize, pageIndex, userId, order_status);
        }

        /// <summary>
        /// 根据id查询订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("query_byid")]
        public ReturnResult<OrdersModel> QueryOrderById(string id)
        {
            return orderService.QueryOrderById(Utils.Extensions.Plat, id);
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        public ReturnResult<ReturnOrdersWebModel> AddOrder(AddOrdersWebModel model)
        {
            return orderService.AddOrder(Utils.Extensions.Plat, model);
        }
    }
}
