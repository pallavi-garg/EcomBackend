
using OrderService.BusinessLogic.Interface;
using OrderService.DataAccess.SQL.Interfaces;
using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using OrderService.AzureBus;

namespace OrderService.BusinessLogic
{
    public class OrderProvider : IOrderProvider
    {
        IRepository _repo;
        public OrderProvider(IRepository repo)
        {
            _repo = repo;
        }

        public void DeleteOrderById(string orderId)
        {
            _repo.Delete(orderId);
        }

        public IEnumerable<OrderDetails> GetAllOrders()
        {
            return _repo.GetAll();
        }

        public OrderDetails GetOrderById(string id)
        {
            return _repo.GetById(id);
        }

        public void UpdateOrderDetail(OrderDetails inputData)
        {
            _repo.Update(inputData);
        }

        public string AddNewOrder(Order inputData)
        {
            Guid orderId = new Guid();
            OrderDetails orderDetails = new OrderDetails();
            List<ProductOrderDetail> productOrderDetails = new List<ProductOrderDetail>();
            FillOrderAndProductDetails(inputData, orderId, ref orderDetails, ref productOrderDetails);
            _repo.Insert(orderDetails, productOrderDetails);

            MessageSender.SendOrderPlacedAsync(ProductOrderMessageCreator.CreateUpdateProductinventoryMessage(productOrderDetails)).Wait();

            return orderId.ToString();
        }

        private void FillOrderAndProductDetails(Order inputData, Guid orderId, ref OrderDetails orderDetails, ref List<ProductOrderDetail> productOrderDetails)
        {
            orderDetails.OrderId = orderId;
            orderDetails.OrderDate = DateTime.Now;
            orderDetails.PaymentId = inputData.PaymentId;
            orderDetails.AddressId = inputData.AddressId;
            orderDetails.CustomerId = inputData.CustomerId;

            foreach(var product in inputData.Products)
            {
                productOrderDetails.Add(new ProductOrderDetail()
                {
                    OrderId = orderId.ToString(),
                    ProductOrderDetailID = new Guid(),
                    ProductId = product.ProductId,
                    ProductPurchasePrice = product.ProductPurchasePrice,
                    SKU = product.SKU
                });
            }
            
        }
    }
}
