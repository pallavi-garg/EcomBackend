using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.BusinessLogic.Interface
{
    public interface IOrderProvider
    {
        IEnumerable<OrderDetails> GetAllOrders();

        OrderDetails GetOrderById(int id);

        void UpdateOrderDetail(OrderDetails inputData);

        void AddNewOrder(OrderDetails inputData);

        void DeleteOrderById(int orderId);
    }
}
