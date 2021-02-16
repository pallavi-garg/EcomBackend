using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.DataAccess
{
    public interface ICategoryDataAccessBridge
    {
        Category GetCategoryById(string id);

        Category GetCategoryByName(string name);

        SearchResult<Category> SearchCategory(string query, string continuationToken);

        bool UpdateCategory(Category inputData, string categoryId);

        bool DeleteCategoryById(string productId);

        Category AddCategory(Category inputData);

    }
}
