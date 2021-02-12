﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Shared.Model
{
    public class ProductOrderDetail
    {
        [Key]
        public Guid ProductOrderDetailID { get; set; }
        public decimal ProductPurchasePrice { get; set; }
        public decimal Tax { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string SKU { get; set; }

    }
}
