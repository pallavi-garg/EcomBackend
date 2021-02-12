namespace OrderService.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using OrderService.DataAccess.SQL;
    using OrderService.DataAccess.SQL.Interfaces;
    using OrderService.Shared.Model;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Provide database interaction in form of entities and functionality to save changes to database.
    /// </summary>
    public sealed class Repository : IRepository
    {

        private readonly OrderContext _context;

        public Repository(OrderContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderDetails> GetAll()
        {
            return _context.Order.ToList();
        }
        public OrderDetails GetById(string OrderId)
        {
            return _context.Order.Find(OrderId);
        }
        public void Insert(OrderDetails orderDetail, List<ProductOrderDetail> productOrderDetails)
        {
            //TODO: pass productOrderDetails
            _context.Order.Add(orderDetail);
        }
        public void Update(OrderDetails orderDetails)
        {
            _context.Entry(orderDetails).State = EntityState.Modified;
        }
        public void Delete(string orderId)
        {
            OrderDetails employee = _context.Order.Find(orderId);
            _context.Order.Remove(employee);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}