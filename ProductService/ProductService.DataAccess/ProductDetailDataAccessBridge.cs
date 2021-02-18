using ProductService.Shared;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public partial class DataAccessBridge : IProductDetailDataAccessBridge
    {
        private readonly IReadService readService;
        private readonly IWriteService writeService;



        public DataAccessBridge(IReadService readService, IWriteService writeService)
        {
            this.readService = readService;
            this.writeService = writeService;
        }

        public bool DeleteProductById(string productId)
        {
            return true;
        }

        public SearchResult<ProductModel> GetAllProducts(string continuationToken)
        {
            return readService.GetAllItems<ProductModel>(continuationToken);
        }

        public ProductModel GetProductById(string id)
        {
            return readService.GetItemById<ProductModel>(id);
        }

        public ProductModel GetProductByName(string name)
        {
            return new ProductModel();
        }

        public SearchResult<ProductModel> SearchProduct(string query, string continuationToken)
        {
            return readService.GetItemByQuery<ProductModel>(query, continuationToken);
        }

        public int GetProductCount(string query)
        {
            return readService.GetItemCountByQuery(query);
        }


        public bool UpdateProductDetail(ProductModel inputData, string productId)
        {
            return true;
        }

        public ProductModel AddProductDetail(ProductModel inputData)
        {
            inputData.Id = Guid.NewGuid();
            return writeService.AddItem(inputData);
        }
    }
}
