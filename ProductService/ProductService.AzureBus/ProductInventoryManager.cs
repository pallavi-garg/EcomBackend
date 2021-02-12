using ProductService.BusinessLogic;
using System.Linq;

namespace ProductService.AzureBus
{
    public class ProductInventoryManager : IProductInventoryManager
    {
        IProductDetailsProvider _productDetailProvider;
        public ProductInventoryManager(IProductDetailsProvider productDetailProvider)
        {
            _productDetailProvider = productDetailProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productModel"></param>
        /// <param name="delta"></param>
        public void UpdateProductQuantity(ProductOrder product, int delta)
        {
            var matchedProduct = _productDetailProvider.GetAllProducts().FirstOrDefault(p => p.Sku == product.SKU && p.ProductId == product.ProductId);
            if (matchedProduct != null)
            {
                matchedProduct.Quantity = (uint)((int)matchedProduct.Quantity + delta);
                _productDetailProvider.UpdateProductDetail(matchedProduct, matchedProduct.ProductId);
            }
        }
    }
}
