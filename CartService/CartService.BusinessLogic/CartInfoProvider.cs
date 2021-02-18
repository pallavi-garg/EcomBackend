using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL.Interfaces;
using System;
using System.Collections.Generic;
using CartService.DataAccess.SQL;
using CartService.Shared.Model;
using CartService.DataAccess.WebClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CartService.BusinessLogic
{
    public class CartInfoProvider : ICartInfoProvider
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartProductMapping> _cartProductMappingRepo;
        private readonly HttpCalls _httpCalls;
        private readonly AppSettings _appsettings;
        public CartInfoProvider(IRepository<Cart> cartRepo, IRepository<CartProductMapping> cartProductMappingRepo, HttpCalls httpCalls, IOptions<AppSettings> appSettings)
        {
            _cartRepo = cartRepo;
            _cartProductMappingRepo = cartProductMappingRepo;
            _httpCalls = httpCalls;
            _appsettings = appSettings?.Value;
        }

        public void DeleteItemFromCart(string cartId)
        {
            _cartRepo.Delete(cartId);
            _cartProductMappingRepo.DeleteByCartId(cartId);
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

        public async Task<CartDetails> GetCartDetails(string cartId)
        {
            var cartProductMappingData = _cartProductMappingRepo.GetByCartId(cartId);
            var cartData = _cartRepo.GetById(cartId);
            if (cartProductMappingData != null && cartProductMappingData.Count > 0 && cartData != null)
            {
                List<ShortProductDetails> productList = new List<ShortProductDetails>();
                Parallel.ForEach(cartProductMappingData, (entry) =>
                 {
                     var result = _httpCalls.PostClient<string, ProductModel>(entry.ProductId, new Uri(_appsettings.ProductServiceEndpoint, UriKind.Absolute));

                     if (result.Result != null)
                     {
                         var shortProductDeails = new ShortProductDetails
                         {
                             ProductId = result.Result.Id.ToString(),
                             Quantity = entry.Quantity,
                             Sku = result.Result.Sku
                         };
                         productList.Add(shortProductDeails);
                     }
                 });
                return new CartDetails
                {
                    CartId = cartData.Id.ToString(),
                    CustomerId = cartData.CustomerId,
                    CreatedDate = cartData.CreatedAt,
                    LastUpdated = cartData.ModifiedDate,
                    productInfo = productList
                };
            }
            return null;
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
            inputData.productInfo.ForEach((item) =>
            {
                var cartProductMapping = new CartProductMapping
                {
                    CartId = inputData.CartId,
                    CreatedAt = inputData.CreatedDate,
                    ModifiedDate = inputData.LastUpdated,
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SKU = item.Sku
                };
                _cartProductMappingRepo.Update(cartProductMapping);
            });
        }

        public void AddNewItemInCart(CartDetails inputData)
        {
            if (inputData is null)
                throw new Exception("Input Data is not present");

            var cartData = new Cart
            {
                CreatedAt = inputData.CreatedDate,
                CustomerId = inputData.CustomerId,
                Id = Guid.Parse(inputData.CartId),
                ModifiedDate = inputData.LastUpdated
            };
            _cartRepo.Insert(cartData);
            inputData.productInfo.ForEach((item) =>
            {
                var cartProductMapping = new CartProductMapping
                {
                    CartId = inputData.CartId,
                    CreatedAt = inputData.CreatedDate,
                    ModifiedDate = inputData.LastUpdated,
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SKU = item.Sku
                };
                _cartProductMappingRepo.Insert(cartProductMapping);
            });
        }

        public IEnumerable<CartProductMapping> GetAllCartItems()
        {
            return _cartProductMappingRepo.GetAll();
        }
    }
}
