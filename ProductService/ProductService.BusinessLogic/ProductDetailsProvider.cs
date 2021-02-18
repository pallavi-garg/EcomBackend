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

        public SearchResult<ProductModel> GetProductByDepartment(string department, string continuationToken)
        {
            SearchResult<ProductModel> searchResult = new SearchResult<ProductModel>();
            string query = $"Select * from c where c.Department = '{department.ToLower()}'";

            searchResult = _baseDataAccessBridge.SearchProduct(query, continuationToken);

            query = $"Select count(1) from c where c.Department = '{department.ToLower()}'";

            searchResult.TotalCount = _baseDataAccessBridge.GetProductCount(query);

            return searchResult;
        }

        public SearchResult<ProductModel> SearchProduct(List<string> searchDetails, string continuationToken)
        {
            string query = $"Select * from c where c.Department in ({string.Join(",", searchDetails.Select(value => $"'{value.ToLower()}'"))}) or " +
                $"c.SuperCategory in ({string.Join(",", searchDetails.Select(value => $"'{value.ToLower()}'"))}) or ";
            
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
