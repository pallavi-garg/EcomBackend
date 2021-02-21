using CartService.BusinessLogic.Interface;
using CartService.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        /// <summary>
        /// id - cart id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CartDetails> GetCartDetails(string id)
        {
            return _cartProvider.GetCartDetails(id).Result;
        }

        /// <summary>
        /// Id is id of custmer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("customer/{id}")]
        public ActionResult<CartDetails> GetCartDetailsByCustomerId(string id)
        {
            return _cartProvider.GetCartDetailsByCustomerId(id);
        }

        /// <summary>
        /// Id is id of custmer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("cartItemCount/{customerId}")]
        public ActionResult<int> GetCartItemsCount(string customerId)
        {
            return _cartProvider.GetCartItemsCount(customerId);
        }

        /// <summary>
        /// Add/Update carts
        /// </summary>
        /// <param name="inputData"></param>
        [HttpPost]
        public ActionResult<string> UpsertCartDetail([FromBody] CartDetails inputData)
        {
            if(string.IsNullOrWhiteSpace(inputData.CartId))
            {
                // create new cart
               return _cartProvider.AddCartDetail(inputData);
            }
            else
            {
                CartDetails existingCart = _cartProvider.GetCart(inputData.CartId);
                if(existingCart != null)
                {
                    if (existingCart.CustomerId == inputData.CustomerId)
                    {
                        inputData.CreatedDate = existingCart.CreatedDate;
                        //Update Cart
                        return _cartProvider.UpdateCartDetail(inputData);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return null;
        }
    }
}
