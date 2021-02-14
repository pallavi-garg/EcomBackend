﻿
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

        public string AddNewOrder(Order inputData)
        {
            Guid orderId = new Guid();
            OrderDetails orderDetails = new OrderDetails();
            List<ProductOrderDetail> productOrderDetails = new List<ProductOrderDetail>();
            FillOrderAndProductDetails(inputData, orderId, ref orderDetails, ref productOrderDetails);
            _repo.Insert(orderDetails);
            productOrderDetails.ForEach(item =>
            {
                _productDetailRepo.Insert(item);
            });

            MessageSender.SendOrderPlacedAsync(ProductOrderMessageCreator.CreateUpdateProductinventoryMessage(productOrderDetails)).Wait();

            return orderId.ToString();
        }

        private void FillOrderAndProductDetails(Order inputData, Guid orderId, ref OrderDetails orderDetails, ref List<ProductOrderDetail> productOrderDetails)
        {
            orderDetails.Id = orderId;
            orderDetails.OrderDate = DateTime.Now;
            orderDetails.PaymentId = inputData.PaymentId;
            orderDetails.AddressId = inputData.AddressId;
            orderDetails.CustomerId = inputData.CustomerId;

            foreach(var product in inputData.Products)
            {
                productOrderDetails.Add(new ProductOrderDetail()
                {
                    OrderId = orderId.ToString(),
                    Id = new Guid(),
                    ProductId = product.ProductId,
                    ProductPurchasePrice = product.ProductPurchasePrice,
                    SKU = product.SKU
                });
            }
            
        }
    }
}
