﻿
using System;

namespace OrderService.Shared.Model
{
    public class ProductModel
    {
        public Guid? Id { get; set; }

        
        public string ProductId { get; set; }

        
        public string ShortDescription { get; set; }

        
        public string LongDescription { get; set; }

        
        public string[] SuperCategory { get; set; }

        
        public string Title { get; set; }

        
        public string Department { get; set; }

        
        public string Brand { get; set; }
        
        
        public string Sku { get; set; }
        
        
        public string Price { get; set; }
        
        
        public DateTime CreateDate { get; set; }
        
        
        public DateTime ModifiedDate { get; set; }
        
        
        public ImageDetail[] Media { get; set; }
        
        
        public FeatureDetail[] Features { get; set; }
    }

}
