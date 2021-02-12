using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderService.Shared.Model
{
    public class OrderDetails
    {
        [Key]
        public Guid OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string PromotionId { get; set; }
        public string PaymentId { get; set; }
        public string AddressId { get; set; }
        public string CustomerId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
