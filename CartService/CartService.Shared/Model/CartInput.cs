using System;

namespace CartService.Shared.Model
{
    public class CartInput
    {
        public Guid CartId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string SKU { get; set; }
    }
}
