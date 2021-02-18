using CartService.DataAccess.SQL;
using CartService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BusinessLogic.Interface
{
    public interface ICartInfoProvider
    {
        Task<CartDetails> GetCartDetails(string cartId);

        CartDetails GetCartDetailsByCustomerId(string customerId);

        void UpdateCartDetail(CartDetails inputData);

        void AddNewItemInCart(CartDetails inputData);

        void DeleteItemFromCart(string productId);

        void ResetCart(string customerId);
        IEnumerable<CartProductMapping> GetAllCartItems();
    }
}
