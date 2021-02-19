using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public SearchResult<ProductModel> GetAllProductList(string continuationToken)
        {
            //foreach (var item in User.Claims)
            //{
            //    Console.WriteLine(item.Type+":"+item.Value);
            //}
            //  var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            // var role = User.Claims.FirstOrDefault(c => c.Type == "extension_Role")?.Value;
            return _productDetailProvider.GetAllProducts(continuationToken);
        }

        [HttpGet("{id}/sku/{skuId}")]
        public ActionResult<ProductModel> GetProductById(string id, string skuId)
        {
            return _productDetailProvider.GetProductById(id, skuId);
        }

        [HttpGet("productName/{productName}")]
        public ActionResult<ProductModel> GetProductByProductName(string productName)
        {
            return _productDetailProvider.GetProductByName(productName);
        }

        [HttpGet("search")]
        public ActionResult<SearchResult<ProductModel>> SearchProduct(List<SearchDTO> searchDetails, [FromHeader] string continuationToken)
        {
            return _productDetailProvider.SearchProduct(searchDetails, continuationToken);
        }

        [HttpGet("department/{departmentId}")]
        public ActionResult<SearchResult<ProductModel>> SearchProductByDepartment(string departmentId, [FromHeader] string continuationToken)
        {
            return _productDetailProvider.GetProductByDepartment(departmentId, continuationToken);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public bool UpdateProductDetail(string productId, [FromBody] ProductModel inputData)
        {
            return _productDetailProvider.UpdateProductDetail(inputData, productId);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public ProductModel AddProductDetail([FromBody] ProductModel inputData)
        {
            return _productDetailProvider.AddProductDetail(inputData);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public bool DeleteProduct(string id)
        {
            return _productDetailProvider.DeleteProductById(id);
        }

    }
}
