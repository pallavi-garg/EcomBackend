using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.BusinessLogic
{
    public interface ICategoryProvider
    {

        SearchResult<Category> GetCategoryByDepartment(string department, string continuationToken);

        bool UpdateCategory(Category inputData, string categoryId);

        Category AddCategory(Category inputData);

        bool DeleteCategoryById(string categoryId);
    }
}
