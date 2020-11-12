using ProductService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.BusinessLogic.Interfaces
{
    public interface IProductDetailsProvider
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel GetProductById(string id);
     
        ProductModel GetProductByName(string name);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        bool DeleteProductById(string productId);
    }
}
