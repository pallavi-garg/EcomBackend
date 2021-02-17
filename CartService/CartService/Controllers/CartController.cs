using CartService.BusinessLogic.Interface;
using CartService.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {

        private readonly ILogger<CartController> _logger;
        private readonly ICartInfoProvider _cartProvider;

        public CartController(ILogger<CartController> logger, ICartInfoProvider cartProvider)
        {
            _logger = logger;
            _cartProvider = cartProvider;
        }

        [HttpGet("{customerId}")]
        public ActionResult<CartDetails> GetCartDetails(string customerId)
        {
            return _cartProvider.GetCartDetails(customerId);
        }

        [HttpGet("customerId/{customerId}")]
        public ActionResult<CartDetails> GetCartDetailsByCustomerId(string customerId)
        {
            return _cartProvider.GetCartDetailsByCustomerId(customerId);
        }

        [HttpPut("UpdateCart/{Id}")]
        public void UpdateCartDetail([FromBody] CartDetails inputData)
        {
            _cartProvider.UpdateCartDetail(inputData);
        }

        [HttpPost("AddNewProduct")]
        public void AddNewItemInCart([FromBody] CartDetails inputData)
        {
            _cartProvider.AddNewItemInCart(inputData);
        }

        [HttpDelete("DeleteItem/{ProductId}")]
        public void DeleteItemFromCart(string productId)
        {
            _cartProvider.DeleteItemFromCart(productId);
        }
    }
}
