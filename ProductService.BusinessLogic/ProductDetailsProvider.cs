using ProductService.BusinessLogic.Interfaces;
using ProductService.DataAccess.Interfaces;
using ProductService.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductService.BusinessLogic
{
    public class ProductDetailsProviders : IProductDetailsProvider
    {
        IBaseDataAccessBridge _baseDataAccessBridge;
        public ProductDetailsProviders(IBaseDataAccessBridge baseDataAccessBridge)
        {
            _baseDataAccessBridge = baseDataAccessBridge;
        }

        public bool DeleteProductById(string productId)
        {
            return _baseDataAccessBridge.DeleteProductById(productId);
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return _baseDataAccessBridge.GetAllProducts();
        }

        public ProductModel GetProductById(string id)
        {
            return _baseDataAccessBridge.GetProductById(id);
        }

        public ProductModel GetProductByName(string name)
        {
            return _baseDataAccessBridge.GetProductByName(name);
        }

        public bool UpdateProductDetail(ProductModel inputData, string productId)
        {
            return _baseDataAccessBridge.UpdateProductDetail(inputData, productId);
        }
    }
}
