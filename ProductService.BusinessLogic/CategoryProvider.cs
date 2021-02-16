using ProductService.DataAccess;
using ProductService.Shared;
using System;
using System.Collections.Generic;

namespace ProductService.BusinessLogic
{
    public class CategoryProvider : ICategoryProvider
    {
        IBaseDataAccessBridge _baseDataAccessBridge;
        public CategoryProvider(IBaseDataAccessBridge baseDataAccessBridge)
        {
            _baseDataAccessBridge = baseDataAccessBridge;
        }

        public SearchResult<Category> GetCategoryByDepartment(string department, string continuationToken)
        {
            string query = $"Select * from c where c.Department = '{department.ToLower()}'";

            return _baseDataAccessBridge.SearchCategory(query, continuationToken);

        }

        public bool UpdateCategory(Category inputData, string categoryId)
        {
            return _baseDataAccessBridge.UpdateCategory(inputData, categoryId);
        }

        public Category AddCategory(Category inputData)
        {

            inputData.CreateDate = DateTime.UtcNow;
            inputData.ModifiedDate = DateTime.UtcNow;
            return _baseDataAccessBridge.AddCategory(inputData);
        }

        public bool DeleteCategoryById(string categoryId)
        {
            return _baseDataAccessBridge.DeleteCategoryById(categoryId);
        }

        
    }
}
