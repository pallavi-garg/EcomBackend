using ProductService.DataAccess;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductService.BusinessLogic
{
    public class ProductDetailsProvider : IProductDetailsProvider
    {
        IBaseDataAccessBridge _baseDataAccessBridge;
        public ProductDetailsProvider(IBaseDataAccessBridge baseDataAccessBridge)
        {
            _baseDataAccessBridge = baseDataAccessBridge;
        }

        public bool DeleteProductById(string productId)
        {
            return _baseDataAccessBridge.DeleteProductById(productId);
        }

        public SearchResult<ProductModel> GetAllProducts(string continuationToken)
        {
            return _baseDataAccessBridge.GetAllProducts(continuationToken);
        }

        public ProductModel GetProductById(string id, string skuId)
        {
            string query = $"Select * from c where c.ProductId = '{id}' and c.Sku = '{skuId}'";

            return _baseDataAccessBridge.SearchProduct(query, null).Data.FirstOrDefault();
        }

        public ProductModel GetProductByName(string name)
        {
            return _baseDataAccessBridge.GetProductByName(name);
        }

        public SearchResult<ProductModel> GetProductByDepartment(string department, string continuationToken)
        {
            SearchResult<ProductModel> searchResult = new SearchResult<ProductModel>();
            string query = $"Select * from c where c.Department = '{department.ToLower()}'";

            searchResult = _baseDataAccessBridge.SearchProduct(query, continuationToken);

            query = $"Select count(1) from c where c.Department = '{department.ToLower()}'";

            searchResult.TotalCount = _baseDataAccessBridge.GetProductCount(query);

            return searchResult;
        }

        public SearchResult<ProductModel> SearchProduct(List<SearchDTO> searchDetails, string continuationToken)
        {
            string query = "Select * from c where ";
            
            foreach(var item in searchDetails)
            {
                query = $"{query} c.Features.{item.Key} in ({string.Join(",", item.Value.Select(value => $"'{value.ToLower()}'"))})";
                if(searchDetails.Last() != item)
                {
                    query = $"{query} and";
                }
            }
            return _baseDataAccessBridge.SearchProduct(query, continuationToken);
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
