using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL;
using CartService.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Not MVP
        /// </summary>
        /// <param name="inputData"></param>
        [HttpPut("update/{id}")]
        public void UpdateCartDetail([FromBody] CartDetails inputData)
        {
            _cartProvider.UpdateCartDetail(inputData);
        }

        [HttpPost("addproduct")]
        public CartDetails AddNewItemInCart([FromBody] CartDetails inputData)
        {
            return _cartProvider.AddNewItemInCart(inputData);
        }

        /// <summary>
        /// return bool
        /// id - product id
        /// not mvp
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("deleteproduct/{id}")]
        public void DeleteItemFromCart(string id)
        {
            _cartProvider.DeleteItemFromCart(id);
        }

        ///// <summary>
        ///// return bool
        ///// id - product id
        ///// </summary>
        ///// <param name="id"></param>
        //[HttpDelete("deleteproduct/{id}/{sku}")]
        //public void DeleteItemFromCart(string id, string sku)
        //{
        //    //TODO pass sku
        //    _cartProvider.DeleteItemFromCart(id);
        //}

        /// <summary>
        /// Not MVP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CartProductMapping> GetAllCarts()
        {
            return _cartProvider.GetAllCartItems();
        }
    }
}
