using System.Collections.Generic;
using Feng.Basic;
using Feng.Order.IService;
using Feng.Order.Model;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Order.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("approve")]
        public ReturnResult ApproveOrder([FromBody]ApproveOrdersModel model)
        {
            return orderService.ApproveOrder(Utils.Extensions.Plat, model);
        }


        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("querylist")]
        public ReturnResult<List<OrdersPrimaryModel>> QueryOrder()
        {
            return orderService.QueryOrders(Utils.Extensions.Plat);
        }

        /// <summary>
        /// 分页查询订单
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("query_pagelist")]
        public ReturnResult<PageList<OrdersPrimaryModel>> QueryPageOrder(int pageSize, int pageIndex)
        {
            return orderService.QueryOrdersList(Utils.Extensions.Plat, pageSize, pageIndex);
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
        /// 修改订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("modify")]
        public ReturnResult ModifyOrder([FromBody]ModifyOrdersModel model)
        {
            return orderService.ModifyOrder(Utils.Extensions.Plat, model);
        }
    }
}
