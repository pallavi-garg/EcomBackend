using OrderService.Shared.Model;
using System.Collections.Generic;

namespace OrderService.BusinessLogic.Interface
{
    public interface IOrderProvider
    {
        IEnumerable<OrderDetails> GetAllOrders();

        OrderDetails GetOrderById(string id);

        void UpdateOrderDetail(OrderDetails inputData);

        string AddNewOrder(Order inputData);

        void DeleteOrderById(string orderId);
    }
}
