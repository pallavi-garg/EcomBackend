using ProductService.DataAccess.Interfaces;
using ProductService.Shared.Models;
using System;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public partial class DataAccessBridge: IBaseDataAccessBridge
    {
        
        public bool DeleteProductById(string productId)
        {
            return true;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return new List<ProductModel> { };
        }

        public ProductModel GetProductById(string id)
        {
            return new ProductModel() { 
            IsAvailable = true,
            Price = 123300,
            ProductId = "XYZABC",
            ProductName = "G-SHOCK",
            ProductCategory = "Watches"
            };
        }

        public ProductModel GetProductByName(string name)
        {
            return new ProductModel();
        }

        public bool UpdateProductDetail(ProductModel inputData, string productId)
        {
            return true;
        }
    }
}
