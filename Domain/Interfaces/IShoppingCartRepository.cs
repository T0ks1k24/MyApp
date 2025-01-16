using Domain.Entities;

namespace Domain.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> GetCartByUserIdAsync(int userId);
    Task<ShoppingCart> GetOrCreateCartAsync(int userId);
    Task AddCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(int cartItemId);
    Task UpdateCartItemAsync(CartItem cartItem);
    Task ClearCartAsync(int cartId);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
}
