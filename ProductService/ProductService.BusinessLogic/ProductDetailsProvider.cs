using ProductService.DataAccess;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<ProductModel> SearchProduct(List<SearchDTO> searchDetails)
        {
            string query = "Select * from c where ";
            
            foreach(var item in searchDetails)
            {
                query = $"{query} c.Features.{item.Key} in ({string.Join(",", item.Value.Select(value => $"'{value}'"))})";
                if(searchDetails.Last() != item)
                {
                    query = $"{query} and";
                }
            }
            return _baseDataAccessBridge.SearchProduct(query);
        }

        public bool UpdateProductDetail(ProductModel inputData, string productId)
        {
            return _baseDataAccessBridge.UpdateProductDetail(inputData, productId);
        }

        public ProductModel AddProductDetail(ProductModel inputData)
        {
            inputData.CreateDate = DateTime.UtcNow;
            inputData.ModifiedDate = DateTime.UtcNow;
            return _baseDataAccessBridge.AddProductDetail(inputData);
        }
        
    }
}
