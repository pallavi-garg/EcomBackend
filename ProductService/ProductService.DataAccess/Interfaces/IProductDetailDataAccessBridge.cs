using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public interface IProductDetailDataAccessBridge
    {
        SearchResult<ProductModel> GetAllProducts(string continuationToken);

        ProductModel GetProductById(string id);

        ProductModel GetProductByName(string name);


        SearchResult<ProductModel> SearchProduct(string query, string continuationToken);

        int GetProductCount(string query);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        bool DeleteProductById(string productId);

        ProductModel AddProductDetail(ProductModel inputData);

    }
}
