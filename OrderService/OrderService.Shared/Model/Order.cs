using System.Collections.Generic;

namespace OrderService.Shared.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public string PromotionId { get; set; }
        public string PaymentId { get; set; }
        public string BillingAddressId { get; set; }
        public string ReceipentAddressId { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<ShortProductDetails> Products { get; set; }
    }
}
