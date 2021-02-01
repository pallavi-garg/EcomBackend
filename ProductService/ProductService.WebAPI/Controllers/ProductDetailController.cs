using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic;
using ProductService.Shared;
using System.Collections.Generic;

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
