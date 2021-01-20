using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.DataAccess.SQL.Interfaces
{
    public interface IRepository
    {
        IEnumerable<OrderDetails> GetAll();

        OrderDetails GetById(int OrderId);

        void Insert(OrderDetails orderDetail);

        void Update(OrderDetails orderDetails);

        void Delete(int orderId);

        void Save();       
    }
}
