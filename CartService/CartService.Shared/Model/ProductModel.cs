
using System;
using System.Collections.Generic;

namespace CartService.Shared.Model
{
    public class ProductModel
    {
        public Guid? Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }
        
        public string Sku { get; set; }
        
        public string Price { get; set; }
        
        public ImageDetail[] Media { get; set; }

        public Features Features { get; set; }

        public int CartQuantity { get; set; }
    }

}
