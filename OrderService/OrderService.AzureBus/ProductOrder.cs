using System;

namespace OrderService.AzureBus
{
    public class ProductOrder
    {
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public uint Quantity { get; set; }
    }
}
