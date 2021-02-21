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
            var matchedProduct = _productDetailProvider.GetProductById(product.ProductId, product.SKU);
            if (matchedProduct != null)
            {
                matchedProduct.Quantity = matchedProduct.Quantity - product.Quantity;
                _productDetailProvider.UpdateProductDetail(matchedProduct, matchedProduct.ProductId);
            }
        }
    }
}
