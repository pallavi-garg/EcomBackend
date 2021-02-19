using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

// TODO restrict methods lie add update delete
namespace ProductService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductDetailController : ControllerBase
    {
        IProductDetailsProvider _productDetailProvider;
        public ProductDetailController(IProductDetailsProvider productDetailProvider)
        {
            _productDetailProvider = productDetailProvider;
        }
        
        [HttpGet]
        public IEnumerable<ProductModel> GetAllProductList()
        {

            var a = HttpContext.User;
            //foreach (var item in User.Claims)
            //{
            //    Console.WriteLine(item.Type+":"+item.Value);
            //}
            //  var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            // var role = User.Claims.FirstOrDefault(c => c.Type == "extension_Role")?.Value;

            return _productDetailProvider.GetAllProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductModel> GetProductById(string id)
        {
            return _productDetailProvider.GetProductById(id);
        }

        [HttpGet("productName/{productName}")]
        public ActionResult<ProductModel> GetProductByProductName(string productName)
        {
            return _productDetailProvider.GetProductByName(productName);
        }

        [HttpGet("search")]
        public ActionResult<List<ProductModel>> SearchProduct(List<SearchDTO> searchDetails)
        {
            return _productDetailProvider.SearchProduct(searchDetails);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public bool UpdateProductDetail(string productId, [FromBody] ProductModel inputData)
        {
            return _productDetailProvider.UpdateProductDetail(inputData, productId);
        }

        [HttpPost]
        public ProductModel AddProductDetail([FromBody] ProductModel inputData)
        {
            return _productDetailProvider.AddProductDetail(inputData);
        }

        [HttpDelete("{id}")]
        public bool DeleteProduct(string id)
        {
            return _productDetailProvider.DeleteProductById(id);
        }

    }
}
