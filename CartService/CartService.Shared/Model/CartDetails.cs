using System;
using System.Collections.Generic;

namespace CartService.Shared.Model
{
    public class CartDetails
    {
        public string CartId { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<ProductModel> productInfo { get; set; }
    }
}
