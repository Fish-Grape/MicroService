using System.Collections.Generic;
using Feng.Basic;
using Feng.Order.IService;
using Feng.Order.Model;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Order.web.Controllers.v1
{
    /// <summary>
    /// 订单管理_前台
    /// </summary>
    [ApiVersion("1.0")]
    [Route("{aera}/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersDetailWebController : ControllerBase
    {
        private readonly IOrderDetailService orderService;

        public OrdersDetailWebController(IOrderDetailService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("querylist")]
        public ReturnResult<List<OrdersDetailModel>> QueryOrder()
        {
            return orderService.QueryOrdersDetail(Utils.Extensions.Plat);
        }

        /// <summary>
        /// 分页查询订单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("query_pagelist")]
        public ReturnResult<PageList<OrdersDetailModel>> QueryPageOrder(int pageSize, int pageIndex)
        {
            return orderService.QueryOrdersDetailList(Utils.Extensions.Plat, pageSize, pageIndex);
        }

        /// <summary>
        /// 根据id查询订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("query_byid")]
        public ReturnResult<OrdersDetailModel> QueryOrderById(string id)
        {
            return orderService.QueryOrderDetailById(Utils.Extensions.Plat, id);
        }
    }
}
