using CartService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.BusinessLogic.Interface
{
    public interface ICartInfoProvider
    {
        CartDetails GetCartDetails(string customerId);

        void UpdateCartDetail(CartDetails inputData);

        void AddNewItemInCart(CartDetails inputData);

        void DeleteItemFromCart(string productId);

        void ResetCart(string customerId);
    }
}
