using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.BusinessLogic
{
    public interface IProductDetailsProvider
    {
        SearchResult<ProductModel> GetAllProducts(string continuationToken);

        ProductModel GetProductById(string id, string skuId);
     
        ProductModel GetProductByName(string name);

        SearchResult<ProductModel> GetProductByDepartment(string department, string continuationToken);


        SearchResult<ProductModel> SearchProduct(List<SearchDTO> searchDetails, string continuationToken);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        ProductModel AddProductDetail(ProductModel inputData);

        bool DeleteProductById(string productId);
    }
}
