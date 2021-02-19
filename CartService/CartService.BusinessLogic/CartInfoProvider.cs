using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL.Interfaces;
using System;
using System.Collections.Generic;
using CartService.DataAccess.SQL;
using CartService.Shared.Model;
using CartService.DataAccess.WebClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CartService.BusinessLogic
{
    public class CartInfoProvider : ICartInfoProvider
    {
        private readonly HttpCalls _httpCalls;
        private readonly IConfiguration configuration;

        private readonly HttpClient httpClient;
        public CartInfoProvider(HttpCalls httpCalls, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
            _httpCalls = httpCalls;
            this.configuration = configuration;
        }

        public void DeleteItemFromCart(string cartId)
        {
            using (var dBContext = new DBContext())
            {
                Cart cart = dBContext.Cart.FirstOrDefault(x => x.Id.ToString() == cartId);
                if (cart != null)
                {
                    dBContext.Cart.Remove(cart);

                    dBContext.SaveChanges();
                }
            }
        }

        public void ResetCart(string customerId)
        {

            using (var dBContext = new DBContext())
            {
                Cart cart = dBContext.Cart.FirstOrDefault(x => x.CustomerId == customerId);
                if (cart != null)
                {
                    dBContext.Cart.Remove(cart);

                    dBContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Get cart details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>

        public CartDetails GetCartDetailsByCustomerId(string customerId)
        {

            using (var dBContext = new DBContext())
            {
                var cartData = dBContext.Cart.FirstOrDefault(x => x.CustomerId == customerId);
                List<CartProductMapping> cartProductMapping = new List<CartProductMapping>();
                if (cartData != null)
                {
                    cartProductMapping = dBContext.CartProductMapping.Where(x => x.CartId == cartData.Id).ToList();
                    if (cartProductMapping != null)
                    {
                        List<ProductModel> productModels = new List<ProductModel>();
                        foreach (var cartProduct in cartProductMapping)
                        {
                            var response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{configuration["AppSettings:ProductServiceEndpoint"]}/{cartProduct.ProductId}/sku/{cartProduct.SKU}")).Result;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                                productModel.Quantity = cartProduct.Quantity;
                                productModels.Add(productModel);
                            }
                        }

                        return new CartDetails
                        {
                            CartId = cartData.Id.ToString(),
                            CustomerId = cartData.CustomerId,
                            ProductInfo = productModels
                        };
                    }
                    return new CartDetails
                    {
                        CartId = cartData.Id.ToString(),
                        CustomerId = cartData.CustomerId,
                    };
                }
            }
            return null;
        }

        public async Task<CartDetails> GetCartDetails(string cartId)
        {

            using (var dBContext = new DBContext())
            {
                var cartData = dBContext.Cart.FirstOrDefault(x => x.Id == cartId);
                List<CartProductMapping> cartProductMapping = new List<CartProductMapping>();
                if (cartData != null)
                {
                    cartProductMapping = dBContext.CartProductMapping.Where(x => x.CartId == cartData.Id).ToList();
                    if (cartProductMapping != null)
                    {
                        List<ProductModel> productModels = new List<ProductModel>();
                        foreach (var cartProduct in cartProductMapping)
                        {
                            var response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{configuration["AppSettings:ProductServiceEndpoint"]}/{cartProduct.ProductId}/sku/{cartProduct.SKU}")).Result;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                                productModel.Quantity = cartProduct.Quantity;
                                productModels.Add(productModel);
                            }
                        }

                        dBContext.SaveChanges();


                        return new CartDetails
                        {
                            CartId = cartData.Id.ToString(),
                            CustomerId = cartData.CustomerId,
                            ProductInfo = productModels
                        };
                    }
                    dBContext.SaveChanges();

                    return new CartDetails
                    {
                        CartId = cartData.Id.ToString(),
                        CustomerId = cartData.CustomerId,
                    };
                }
                dBContext.SaveChanges();
            }

            return null;
        }

        public CartDetails GetCart(string cartId)
        {

            using (var dBContext = new DBContext())
            {
                var cartData = dBContext.Cart.FirstOrDefault(x => x.Id == cartId);
                dBContext.SaveChanges();

                return new CartDetails
                {
                    CartId = cartData.Id,
                    CustomerId = cartData.CustomerId,
                    CreatedDate = cartData.CreatedOn
                };
            }
        }

        public void AddCartDetail(CartDetails inputData)
        {
            using (var dBContext = new DBContext())
            {
                var cartData = new Cart
                {
                    CreatedOn = DateTime.UtcNow,
                    CustomerId = inputData.CustomerId,
                    Id = Guid.NewGuid().ToString(),
                    ModifiedOn = DateTime.UtcNow
                };
                dBContext.Cart.Add(cartData);

                if (inputData.ProductInfo != null && inputData.ProductInfo.Any())
                {
                    List<CartProductMapping> cartProductMappings = new List<CartProductMapping>();
                    inputData.ProductInfo.ForEach((item) =>
                    {
                        var cartProductMapping = new CartProductMapping
                        {
                            Id = Guid.NewGuid().ToString(),
                            CartId = cartData.Id,
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            SKU = item.Sku
                        };
                        cartProductMappings.Add(cartProductMapping);
                    });

                    dBContext.CartProductMapping.AddRange(cartProductMappings);
                }
                dBContext.SaveChanges();
            }
            
        }

        public void UpdateCartDetail(CartDetails inputData)
        {
            using (var dBContext = new DBContext())
            {
                var cartData = new Cart
                {
                    CreatedOn = inputData.CreatedDate,
                    CustomerId = inputData.CustomerId,
                    Id = inputData.CartId,
                    ModifiedOn = DateTime.UtcNow
                };

                dBContext.Cart.Update(cartData);

                if (inputData.ProductInfo != null && inputData.ProductInfo.Any())
                {
                    List<CartProductMapping> updatedcartProductMappings = new List<CartProductMapping>();
                    List<CartProductMapping> deletedcartProductMappings = new List<CartProductMapping>();
                    inputData.ProductInfo.ForEach((item) =>
                    {
                        var cartProductMapping = new CartProductMapping
                        {
                            Id = Guid.NewGuid().ToString(),
                            CartId = cartData.Id,
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            SKU = item.Sku
                        };

                        if (item.Quantity < 1)
                        {
                            deletedcartProductMappings.Add(cartProductMapping);
                        }
                        else
                        {
                            updatedcartProductMappings.Add(cartProductMapping);
                        }
                    });

                    if(updatedcartProductMappings != null && updatedcartProductMappings.Any())
                    {
                        dBContext.CartProductMapping.UpdateRange(updatedcartProductMappings);
                        dBContext.SaveChanges();
                    }
                    if (deletedcartProductMappings != null && deletedcartProductMappings.Any())
                    {
                        dBContext.CartProductMapping.RemoveRange(deletedcartProductMappings);
                        dBContext.SaveChanges();
                    }

                }
            }
        }

        public IEnumerable<CartDetails> GetAllCartItems()
        {
            using (var dBContext = new DBContext())
            {
                var cartData = dBContext.Cart;
                List<CartProductMapping> cartProductMapping = new List<CartProductMapping>();
            }
            return null;
        }
    }
}

