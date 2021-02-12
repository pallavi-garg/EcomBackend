using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.DataAccess.SQL.Interfaces
{
    public interface IRepository
    {
        IEnumerable<OrderDetails> GetAll();

        OrderDetails GetById(string OrderId);

        void Insert(OrderDetails orderDetail, List<ProductOrderDetail> productOrderDetails);

        void Update(OrderDetails orderDetails);

        void Delete(string orderId);

        void Save();       
    }
}
