using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.BusinessLogic
{
    public interface IProductDetailsProvider
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel GetProductById(string id);
     
        ProductModel GetProductByName(string name);

        SearchResult<ProductModel> GetProductByDepartment(string department, string continuationToken);


        SearchResult<ProductModel> SearchProduct(List<string> searchDetails, string continuationToken);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        ProductModel AddProductDetail(ProductModel inputData);

        bool DeleteProductById(string productId);
    }
}
