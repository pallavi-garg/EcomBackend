namespace ProductService.Shared
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
