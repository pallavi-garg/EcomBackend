using System;

namespace ProductService.AzureBus
{
    public class ProductOrder
    {
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
    }
}
