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
        /// <param name="delta"></param>
        void UpdateProductQuantity(ProductOrder product, int delta)
    }
}
