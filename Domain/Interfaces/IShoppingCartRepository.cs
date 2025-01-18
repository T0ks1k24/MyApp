using Domain.Entities;

namespace Domain.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> GetCartByUserIdAsync(int userId);
    Task<ShoppingCart> GetOrCreateCartAsync(int userId);
    Task<bool> AddCartItemAsync(CartItem cartItem);
    Task<bool> RemoveCartItemAsync(int cartItemId);
    Task<bool> UpdateCartItemAsync(CartItem cartItem);
    Task<bool> ClearCartAsync(int cartId);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
}
