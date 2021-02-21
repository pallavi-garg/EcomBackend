using System;

namespace OrderService.DataAccess.SQL
{
    public class OrderDetails : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public short OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string PromotionId { get; set; }
        public string PaymentId { get; set; }
        public string BillingAddressId { get; set; }
        public string ReceipentAddressId { get; set; }
        public string CustomerId { get; set; }


        // Order Status 0 - Payment Pending, 1 = Payment Done, 2 = Shipped,3 = Order completed, 4= Cancelled/Inactive, 5= Returned
    }
}
