using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);
        Task<ShoppingCart> GetOrCreateCartAsync(int userId);
        Task<bool> AddCartItemAsync(int userId, int productId, int quantity);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        Task<bool> ClearCartAsync(int userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
    }
}
