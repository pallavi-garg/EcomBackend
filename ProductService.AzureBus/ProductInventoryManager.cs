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
        public void UpdateProductQuantity(ProductOrder product)
        {
            var matchedProduct = _productDetailProvider.GetAllProducts(null).Data?.FirstOrDefault(p => p.Sku == product.SKU && p.ProductId == product.ProductId);
            if (matchedProduct != null)
            {
                matchedProduct.Quantity = matchedProduct.Quantity - product.Quantity;
                _productDetailProvider.UpdateProductDetail(matchedProduct, matchedProduct.ProductId);
            }
        }
    }
}
