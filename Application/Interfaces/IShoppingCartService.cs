using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartByUserIdAsync(int userId);
        Task AddItemToCartAsync(CartItemDto cartItemDto);
        Task RemoveItemFromCartAsync(int cartItemId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        Task ClearCartAsync(int userId);
    }
}
