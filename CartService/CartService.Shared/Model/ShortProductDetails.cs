namespace CartService.Shared.Model
{
    public class ShortProductDetails
    {
        public string ProductId { get; set; }

        public string Sku { get; set; }

        public int Quantity { get; set; }

        public ImageDetail[] Media { get; set; }

        public FeatureDetail[] Features { get; set; }

        public string Price { get; set; }

        public string ShortDescription { get; set; }
    }
}
