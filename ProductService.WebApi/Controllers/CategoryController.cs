using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic;
using ProductService.Shared;
using System.Collections.Generic;

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
        public ActionResult<List<Category>> SearchCategoryByDepartment(string departmentId)
        {
            return _categoryProvider.GetCategoryByDepartment(departmentId);
        }


        [HttpPost]
        public Category AddCategory([FromBody] Category inputData)
        {
            return _categoryProvider.AddCategory(inputData);
        }


    }
}
