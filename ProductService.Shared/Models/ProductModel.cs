using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Shared.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public long Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
