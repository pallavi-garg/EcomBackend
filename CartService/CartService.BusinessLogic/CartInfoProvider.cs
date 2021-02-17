using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL.Interfaces;
using System;
using System.Collections.Generic;
using CartService.DataAccess.SQL;
using CartService.Shared.Model;

namespace CartService.BusinessLogic
{
    public class CartInfoProvider : ICartInfoProvider
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartProductMapping> _cartProductMappingRepo;
        public CartInfoProvider(IRepository<Cart> cartRepo, IRepository<CartProductMapping> cartProductMappingRepo)
        {
            _cartRepo = cartRepo;
            _cartProductMappingRepo = cartProductMappingRepo;
        }

        public void DeleteItemFromCart(string productId)
        {
            _cartRepo.Delete(productId);
        }

        public void ResetCart(string customerId)
        {
            // _cartRepo.DeleteAll();
        }

        

        public CartDetails GetCartDetailsByCustomerId(string customerId)
        {
            var cartData = _cartRepo.GetById(customerId);
            return new CartDetails
            {
                CartId = cartData.Id.ToString(),
                CustomerId = cartData.CustomerId,
                CreatedDate = cartData.CreatedAt,
                LastUpdated = cartData.ModifiedDate,
                productInfo = new List<ShortProductDetails>()
            };
        }

        public CartDetails GetCartDetails(string cartId)
        {
            var cartData = _cartRepo.GetById(cartId);
            return new CartDetails
            {
                CartId = cartData.Id.ToString(),
                CustomerId = cartData.CustomerId,
                CreatedDate = cartData.CreatedAt,
                LastUpdated = cartData.ModifiedDate,
                productInfo = new List<ShortProductDetails>()
            };
        }


        public void UpdateCartDetail(CartDetails inputData)
        {
            var cartData = new Cart
            {
                CreatedAt = inputData.CreatedDate,
                CustomerId = inputData.CustomerId,
                Id = Guid.Parse(inputData.CartId),
                ModifiedDate = inputData.LastUpdated
            };
            _cartRepo.Update(cartData);
        }

        public void AddNewItemInCart(CartDetails inputData)
        {
            var cartData = new Cart
            {
                CreatedAt = inputData.CreatedDate,
                CustomerId = inputData.CustomerId,
                Id = Guid.Parse(inputData.CartId),
                ModifiedDate = inputData.LastUpdated
            };
            _cartRepo.Insert(cartData);
        }
    }
}
