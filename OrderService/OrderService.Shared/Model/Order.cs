using System.Collections.Generic;

namespace OrderService.Shared.Model
{
    public class Order
    {
        public string PromotionId { get; set; }
        public string PaymentId { get; set; }
        public string BillingAddressId { get; set; }
        public string ReceipentAddressId { get; set; }
        public string CustomerId { get; set; }
        public List<ProductDetail> Products { get; set; }
    }
}
