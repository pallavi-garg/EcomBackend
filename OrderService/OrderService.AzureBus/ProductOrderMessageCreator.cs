using OrderService.Shared.Model;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.AzureBus
{
    /// <summary>
    /// Creates the message to be sent on message queue
    /// </summary>
    internal static class ProductOrderMessageCreator
    {
        internal static IEnumerable<ProductOrder> CreateUpdateProductinventoryMessage(IEnumerable<ProductOrderDetail> productOrderDetails)
        {
            List<ProductOrder> productOrders = new List<ProductOrder>();

            foreach (var orderDetail in productOrderDetails)
            {
                var order = productOrders.FirstOrDefault(po => po.ProductId.Equals(orderDetail.ProductId) && po.SKU.Equals(orderDetail.SKU));
                if (order != null)
                {
                    order.Quantity += 1;
                }
                else
                {
                    order = new ProductOrder() { ProductId = orderDetail.ProductId, SKU = orderDetail.SKU, OrderId = orderDetail.OrderId, Quantity = 1 };
                    productOrders.Add(order);
                }
            }

            return productOrders;
        }
    }
}
