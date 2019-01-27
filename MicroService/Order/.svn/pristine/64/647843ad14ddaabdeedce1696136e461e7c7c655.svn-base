using Feng.Basic;
using Feng.Order.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Order.IService
{
    public partial interface IOrderService
    {
        ReturnResult ApproveOrder(string platkey, ApproveOrdersModel model);
        ReturnResult<List<OrdersPrimaryModel>> QueryOrders(string platkey,int userId= -1, int order_status = -1);
        ReturnResult<OrdersModel> QueryOrderById(string platkey, string id);
        ReturnResult ModifyOrder(string platkey, ModifyOrdersModel model);
        ReturnResult<PageList<OrdersPrimaryModel>> QueryOrdersList(string platkey, int pageSize, int pageIndex, int userId = -1, int order_status = -1);

    }
}
