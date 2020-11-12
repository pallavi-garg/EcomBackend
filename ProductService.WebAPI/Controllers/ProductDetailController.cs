using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.Interfaces;
using ProductService.Shared.Models;

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

        [HttpGet("GetAllProductList")]
        public IEnumerable<ProductModel> GetAllProductList()
        {
            return _productDetailProvider.GetAllProducts();
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<ProductModel> GetProductById(string id)
        {
            return _productDetailProvider.GetProductById(id);
        }

        [HttpGet("GetProductByName/{productName}")]
        public ActionResult<ProductModel> GetProductByProductName(string productName)
        {
            return _productDetailProvider.GetProductByName(productName);
        }

        [HttpPost("UpdateProductDetail/{id}")]
        public bool UpdateProductDetail(string productId, [FromBody] ProductModel inputData)
        {
            return _productDetailProvider.UpdateProductDetail(inputData, productId);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public bool DeleteProduct(string id)
        {
            return _productDetailProvider.DeleteProductById(id);
        }

    }
}
