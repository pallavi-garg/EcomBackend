using System;

namespace ProductService.AzureBus
{
    public class ProductOrder
    {
        public string ProductId { get; internal set; }
        public string SKU { get; internal set; }
        public uint Quantity { get; internal set; }
    }
}
