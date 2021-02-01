
using OrderService.BusinessLogic.Interface;
using OrderService.DataAccess.SQL.Interfaces;
using OrderService.Shared.Model;
using System;
using System.Collections.Generic;

namespace OrderService.BusinessLogic
{
    public class OrderProvider: IOrderProvider
    {
        IRepository _repo;
        public OrderProvider(IRepository repo)
        {
            _repo = repo;
        }

        public void DeleteOrderById(int orderId)
        {
            _repo.Delete(orderId);
        }

        public IEnumerable<OrderDetails> GetAllOrders()
        {
            return _repo.GetAll();
        }

        public OrderDetails GetOrderById(int id)
        {
            return _repo.GetById(id);
        }

        public void UpdateOrderDetail(OrderDetails inputData)
        {
            _repo.Update(inputData);
        }

        public void AddNewOrder(OrderDetails inputData)
        {
            _repo.Insert(inputData);

        }
    }
}
