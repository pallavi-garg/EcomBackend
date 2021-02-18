using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL.Interfaces;
using System;
using System.Collections.Generic;
using CartService.DataAccess.SQL;
using CartService.Shared.Model;
using CartService.DataAccess.WebClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>

        public CartDetails GetCartDetailsByCustomerId(string customerId)
        {
            var cartData = _cartRepo.GetByCustomertId(customerId).FirstOrDefault();
            CartDetails cartDetails = new CartDetails();
            if (cartData != null)
            {
                cartDetails = GetCartDetails(cartData.CartId).Result;
            }
            
            return cartDetails;
        }

        public async Task<CartDetails> GetCartDetails(string cartId)
        {

            CartDetails cartDetails = new CartDetails();
            var cartProductMappingData = _cartProductMappingRepo.GetByCartId(cartId);
            var cartData = _cartRepo.GetByCartId(cartId).FirstOrDefault();
            if (cartProductMappingData != null && cartProductMappingData.Count > 0 && cartData != null)
            {
                List<ShortProductDetails> productList = new List<ShortProductDetails>();
                Parallel.ForEach(cartProductMappingData, (entry) =>
                {
                    var serviceEnpoint = $"{_appsettings.ProductServiceEndpoint}/{entry.ProductId}";
                    var result = _httpCalls.GetClient<string, ProductModel>(entry.ProductId, new Uri(serviceEnpoint, UriKind.Absolute));

                    if (result.Result != null)
                    {
                        var shortProductDeails = new ShortProductDetails
                        {
                            ProductId = result.Result.Id.ToString(),
                            Quantity = entry.Quantity,
                            Sku = result.Result.Sku,
                            Features = result.Result.Features,
                            Media = result.Result.Media,
                            Price = result.Result.Price,
                            ShortDescription = result.Result.ShortDescription
                        };
                        productList.Add(shortProductDeails);
                    }
                });
                cartDetails = new CartDetails
                {
                    CartId = cartData.CartId.ToString(),
                    CustomerId = cartData.CustomerId,
                    CreatedDate = cartData.CreatedAt,
                    LastUpdated = cartData.ModifiedDate,
                    ProductInfo = productList
                };
            }
            return cartDetails;
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
            inputData.ProductInfo.ForEach((item) =>
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

        public CartDetails AddNewItemInCart(CartDetails inputData)
        {
            if (inputData is null)
                throw new Exception("Input Data is not present");
            Cart cart = null;
            if (!string.IsNullOrEmpty(inputData.CartId))
            {
                cart = CreateNewCartIfRequired(inputData);
            }
            inputData.ProductInfo.ForEach((item) =>
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

            return inputData;
        }

        private Cart CreateNewCartIfRequired(CartDetails inputData)
        {
            Cart cart = _cartRepo.GetByCartId(inputData.CartId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart()
                {
                    CreatedAt = inputData.CreatedDate,
                    CustomerId = inputData.CustomerId,
                    Id = Guid.Parse(inputData.CartId),
                    ModifiedDate = inputData.LastUpdated
                };
                _cartRepo.Insert(cart);
            }

            return cart;
        }

        public IEnumerable<CartProductMapping> GetAllCartItems()
        {
            return _cartProductMappingRepo.GetAll();
        }
    }
}
