namespace ProductService.AzureBus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductInventoryManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        void UpdateProductQuantity(ProductOrder product);
    }
}
