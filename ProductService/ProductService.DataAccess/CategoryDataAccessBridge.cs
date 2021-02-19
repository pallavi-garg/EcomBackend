using ProductService.Shared;
using System;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public partial class DataAccessBridge : ICategoryDataAccessBridge
    {

        public bool DeleteCategoryById(string categoryId)
        {
            return true;
        }

        public Category GetCategoryById(string id)
        {
            return readService.GetItemById<Category>(id);
        }

        public Category GetCategoryByName(string name)
        {
            return new Category();
        }

        public SearchResult<Category> SearchCategory(string query, string continuationToken)
        {
            return readService.GetItemByQuery<Category>(query, continuationToken);
        }

        public bool UpdateCategory(Category inputData, string categoryId)
        {
            return true;
        }

        public Category AddCategory(Category inputData)
        {
            inputData.Id = Guid.NewGuid();
            return writeService.AddItem(inputData);
        }
    }
}
