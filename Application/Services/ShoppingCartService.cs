using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {


        public Task AddItemToCartAsync(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCartDto> GetCartByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemFromCartAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
