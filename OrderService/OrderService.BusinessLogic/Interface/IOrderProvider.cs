using OrderService.DataAccess.SQL;
using OrderService.Shared.Model;
using System.Collections.Generic;

namespace OrderService.BusinessLogic.Interface
{
    public interface IOrderProvider
    {
        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(string id);

        void UpdateOrderDetail(Order inputData);

        string AddNewOrder(Order inputData);

        void DeleteOrderById(string orderId);
        
        Order GetOrderByCustomerId(string id);
    }
}
