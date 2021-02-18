
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
        private readonly IRepository<OrderDetails> _repo;
        private readonly IRepository<ProductOrderDetail> _productDetailRepo;
        public OrderProvider(IRepository<OrderDetails> repo, IRepository<ProductOrderDetail> productDetailRepo)
        {
            _repo = repo;
            _productDetailRepo = productDetailRepo;
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

        public Order AddNewOrder(Order inputData)
        {
            Guid orderId = new Guid();
            OrderDetails orderDetails = new OrderDetails();
            List<ProductOrderDetail> productOrderDetails = new List<ProductOrderDetail>();
            FillOrderAndProductDetails(inputData, orderId, ref orderDetails, ref productOrderDetails);
            OrderDetails orderedEntered =_repo.Insert(orderDetails);
            _productDetailRepo.BulkInsert(productOrderDetails);

            inputData.InvoiceNumber = orderDetails.InvoiceNumber;
            //productOrderDetails.ForEach(item =>
            //{
            //    _productDetailRepo.Insert(item);
            //});

            MessageSender.SendOrderPlacedAsync(ProductOrderMessageCreator.CreateUpdateProductinventoryMessage(productOrderDetails)).Wait();

            return inputData;
        }

        private void FillOrderAndProductDetails(Order inputData, Guid orderId, ref OrderDetails orderDetails, ref List<ProductOrderDetail> productOrderDetails)
        {
            orderDetails.Id = Guid.NewGuid();
            orderDetails.OrderDate = DateTime.Now;
            orderDetails.PaymentId = Guid.NewGuid().ToString();
            orderDetails.BillingAddressId = inputData.BillingAddressId;
            orderDetails.ReceipentAddressId = inputData.ReceipentAddressId;
            orderDetails.CustomerId = inputData.CustomerId;
            orderDetails.InvoiceNumber = Guid.NewGuid().ToString();
            orderDetails.OrderStatus = 0;

            foreach(var product in inputData.Products)
            {
                productOrderDetails.Add(new ProductOrderDetail()
                {
                    OrderId = orderId.ToString(),
                    Id = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductPurchasePrice = product.ProductPurchasePrice,
                    SKU = product.SKU
                });
            }
            
        }

    }
}
