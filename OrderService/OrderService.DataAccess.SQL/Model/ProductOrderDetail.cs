namespace OrderService.DataAccess.SQL
{
    public class ProductOrderDetail: BaseEntity
    {
        public decimal ProductPurchasePrice { get; set; }
        public decimal Tax { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }

    }
}
