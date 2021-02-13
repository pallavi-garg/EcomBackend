using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public interface IProductDetailDataAccessBridge
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel GetProductById(string id);

        ProductModel GetProductByName(string name);


        List<ProductModel> SearchProduct(string query);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        bool DeleteProductById(string productId);

        ProductModel AddProductDetail(ProductModel inputData);

    }
}
