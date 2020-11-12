using ProductService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.DataAccess.Interfaces
{
    public interface IProductDetailDataAccessBridge
    {
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel GetProductById(string id);

        ProductModel GetProductByName(string name);

        bool UpdateProductDetail(ProductModel inputData, string productId);

        bool DeleteProductById(string productId);
    }
}
