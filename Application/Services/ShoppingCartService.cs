using Application.DTOs;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        //Get Cart By UserId
        public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
        {
            return await _shoppingCartRepository.GetCartByUserIdAsync(userId);
        }

        // Get or create cart
        public async Task<ShoppingCart> GetOrCreateCartAsync(int userId)
        {
            return await _shoppingCartRepository.GetOrCreateCartAsync(userId);
        }

        // Add item to cart
        public async Task<bool> AddCartItemAsync(int userId, int productId, int quantity)
        {
            var cart = await _shoppingCartRepository.GetOrCreateCartAsync(userId);

            if (cart == null)
                return false;

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return await _shoppingCartRepository.UpdateCartItemAsync(existingItem);
            }

            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            };

            return await _shoppingCartRepository.AddCartItemAsync(newCartItem);
        }

        // Remove item from cart
        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            return await _shoppingCartRepository.RemoveCartItemAsync(cartItemId);
        }

        // Update cart item quantity
        public async Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _shoppingCartRepository.GetCartItemByIdAsync(cartItemId);

            if (cartItem == null)
                return false;

            cartItem.Quantity = quantity;
            return await _shoppingCartRepository.UpdateCartItemAsync(cartItem);
        }

        // Clear cart
        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return false;

            return await _shoppingCartRepository.ClearCartAsync(cart.Id);
        }

        // Get cart item by ID
        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _shoppingCartRepository.GetCartItemByIdAsync(cartItemId);
        }
    }
}
