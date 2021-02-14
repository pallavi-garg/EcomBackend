using OrderService.DataAccess.SQL;
using System;

namespace OrderService.Shared.Model
{
    public class OrderDetails : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string PromotionId { get; set; }
        public string PaymentId { get; set; }
        public string AddressId { get; set; }
        public string CustomerId { get; set; }
    }
}
