
using Microsoft.Extensions.Options;
using OrderService.AzureBus;
using OrderService.BusinessLogic.Interface;
using OrderService.DataAccess.SQL;
using OrderService.DataAccess.SQL.Interfaces;
using OrderService.DataAccess.WebClient;
using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.BusinessLogic
{
    public class OrderProvider : IOrderProvider
    {
        private readonly IRepository<OrderDetails> _repo;
        private readonly IRepository<ProductOrderDetail> _productDetailRepo;
        private readonly HttpCalls _httpCalls;
        private readonly AppSettings _appsettings;

        public OrderProvider(IRepository<OrderDetails> repo, IRepository<ProductOrderDetail> productDetailRepo, HttpCalls httpCalls, IOptions<AppSettings> appSettings)
        {
            _repo = repo;
            _productDetailRepo = productDetailRepo;
            _httpCalls = httpCalls;
            _appsettings = appSettings?.Value;
        }

        public void DeleteOrderById(string orderId)
        {
            var order = _repo.GetById(orderId);
            if (order != null)
            {
                order.OrderStatus = 4;
                order.ModifiedDate = DateTime.Now;
                _repo.Update(order);
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            foreach(var data in _repo.GetAll())
            {
                orders.Add(GetOrderDetails(data.Id.ToString(), false));
            }

            return orders;
        }

        public Order GetOrderById(string id)
        {
            return GetOrderDetails(id).Result;
        }

        public void UpdateOrderDetail(Order inputData)
        {
            var order = new OrderDetails
            {
                PromotionId = inputData.PromotionId,
                BillingAddressId = inputData.BillingAddressId,
                CustomerId = inputData.CustomerId,
                InvoiceNumber = inputData.InvoiceNumber,
                PaymentId = inputData.PaymentId,
                ReceipentAddressId = inputData.ReceipentAddressId,
                Id = Guid.Parse(inputData.OrderId),
                ModifiedDate = DateTime.Now
            };
            _repo.Update(order);

            var alreadyOrderedProducts = _productDetailRepo.GetProductByOrderId(order.Id.ToString()).ToList();
            var matchingProducts = inputData.Products.Where(p => alreadyOrderedProducts.Any(addedProduct => addedProduct.ProductId.Equals(p.ProductId)
                                                                                                          && addedProduct.SKU.Equals(p.Sku))).ToList();
            //add new products in database
            var newProducts = inputData.Products.Except(matchingProducts).ToList();
            List<ProductOrderDetail> newProductOrderDetails = new List<ProductOrderDetail>();
            FillProductDetails(newProducts, order.Id, ref newProductOrderDetails);
            _productDetailRepo.BulkInsert(newProductOrderDetails);

            //Update quantities for existing products
            List<ProductOrderDetail> matchingProductOrderDetails = new List<ProductOrderDetail>();          
            foreach (var product in matchingProducts)
            {
                var matchedProduct = alreadyOrderedProducts.FirstOrDefault(addedProduct => addedProduct.ProductId.Equals(product.ProductId) && addedProduct.SKU.Equals(product.Sku));

                if (matchedProduct != null)
                {
                    _productDetailRepo.Detach(matchedProduct);
                    int delta = product.Quantity - matchedProduct.Quantity;
                    var newproduct = new ProductOrderDetail()
                    {
                        Id = matchedProduct.Id,
                        OrderId = order.Id.ToString(),
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        ProductPurchasePrice = product.Price,
                        SKU = product.Sku,
                        Tax = product.Tax
                    };
                    _productDetailRepo.Update(newproduct);
                    //new instance of product is added
                    if (delta > 0)
                    {
                        newproduct.Quantity = delta;
                        matchingProductOrderDetails.Add(newproduct);
                    }
                }
            }

            MessageSender.SendOrderPlacedAsync(ProductOrderMessageCreator.CreateUpdateProductinventoryMessage(newProductOrderDetails.Union(matchingProductOrderDetails).ToList())).Wait();
        }

        public Order AddNewOrder(Order inputData)
        {
            Guid orderId = Guid.NewGuid();
            OrderDetails orderDetails = new OrderDetails();
            List<ProductOrderDetail> productOrderDetails = new List<ProductOrderDetail>();
            FillOrderAndProductDetails(inputData, orderId, ref orderDetails, ref productOrderDetails);
            OrderDetails orderedEntered =_repo.Insert(orderDetails);
            _productDetailRepo.BulkInsert(productOrderDetails);

            inputData.InvoiceNumber = orderDetails.InvoiceNumber;

            MessageSender.SendOrderPlacedAsync(ProductOrderMessageCreator.CreateUpdateProductinventoryMessage(productOrderDetails)).Wait();

            return inputData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>

        public Order GetOrderByCustomerId(string customerId)
        {
            var orderData = _repo.GetOrderByCustomerId(customerId).FirstOrDefault();
            Order order = new Order();
            if (orderData != null)
            {
                order = GetOrderDetails(orderData.Id.ToString()).Result;
            }

            return order;
        }

        public async Task<Order> GetOrderDetails(string orderId)
        {
            return GetOrderDetails(orderId, true);
        }

        private Order GetOrderDetails(string orderId, bool fetchProductDetails)
        {
            Order order = new Order();
            var productMappingData = _productDetailRepo.GetProductByOrderId(orderId);
            var orderData = _repo.GetById(orderId);
            if (orderData != null && productMappingData.Count() > 0)
            {
                List<ShortProductDetails> productList = new List<ShortProductDetails>();
                if (fetchProductDetails)
                {
                    Parallel.ForEach(productMappingData, (entry) =>
                    {
                        var serviceEnpoint = $"{_appsettings.ProductServiceEndpoint}{entry.ProductId}/sku/{entry.SKU}";
                        var result = _httpCalls.GetClient<string, ProductModel>(entry.ProductId, new Uri(serviceEnpoint, UriKind.Absolute));

                        if (result.Result != null)
                        {
                            var shortProductDeails = new ShortProductDetails
                            {
                                ProductId = result.Result.ProductId,
                                Quantity = entry.Quantity,
                                Sku = result.Result.Sku,
                                Features = result.Result.Features,
                                Media = result.Result.Media,
                                ShortDescription = result.Result.ShortDescription
                            };
                            productList.Add(shortProductDeails);
                        }
                    });
                }

                foreach (var product in productList)
                {
                    product.Quantity = productMappingData.Count(p => p.ProductId == product.ProductId && product.Sku == p.SKU);
                    product.Price = productMappingData.First(p => p.ProductId == product.ProductId && product.Sku == p.SKU).ProductPurchasePrice;
                    product.Tax = productMappingData.First(p => p.ProductId == product.ProductId && product.Sku == p.SKU).Tax;
                }

                order = new Order
                {
                    BillingAddressId = orderData.BillingAddressId,
                    CustomerId = orderData.CustomerId,
                    InvoiceNumber = orderData.InvoiceNumber,
                    PaymentId = orderData.PaymentId,
                    Products = productList,
                    PromotionId = orderData.PromotionId,
                    ReceipentAddressId = orderData.ReceipentAddressId
                };
            }
            return order;
        }

        private void FillOrderAndProductDetails(Order inputData, Guid orderId, ref OrderDetails orderDetails, ref List<ProductOrderDetail> productOrderDetails)
        {
            orderDetails.Id = orderId;
            orderDetails.OrderDate = DateTime.Now;
            orderDetails.PaymentId = Guid.NewGuid().ToString();
            orderDetails.BillingAddressId = inputData.BillingAddressId;
            orderDetails.ReceipentAddressId = inputData.ReceipentAddressId;
            orderDetails.CustomerId = inputData.CustomerId;
            orderDetails.InvoiceNumber = Guid.NewGuid().ToString();
            orderDetails.OrderStatus = 0;
            FillProductDetails(inputData.Products, orderId, ref productOrderDetails);

        }

        private static void FillProductDetails(List<ShortProductDetails> inputDataProducts, Guid orderId, ref List<ProductOrderDetail> productOrderDetails)
        {
            foreach (var product in inputDataProducts)
            {
                productOrderDetails.Add(new ProductOrderDetail()
                {
                    OrderId = orderId.ToString(),
                    Id = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductPurchasePrice = product.Price,
                    SKU = product.Sku,
                    Tax = product.Tax
                });
            }
        }
    }
}
