using ProductService.Shared;
using System.Collections.Generic;

namespace ProductService.BusinessLogic
{
    public interface ICategoryProvider
    {

        List<Category> GetCategoryByDepartment(string department);

        bool UpdateCategory(Category inputData, string categoryId);

        Category AddCategory(Category inputData);

        bool DeleteCategoryById(string categoryId);
    }
}
