using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic;
using ProductService.Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

// TODO restrict methods lie add update delete
namespace ProductService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryProvider _categoryProvider;
        public CategoryController(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }


        [HttpGet("department/{departmentId}")]
        public ActionResult<SearchResult<Category>> SearchCategoryByDepartment(string departmentId, [FromHeader] string continuationToken)
        {
            return _categoryProvider.GetCategoryByDepartment(departmentId, continuationToken);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public Category AddCategory([FromBody] Category inputData)
        {
            return _categoryProvider.AddCategory(inputData);
        }


    }
}
