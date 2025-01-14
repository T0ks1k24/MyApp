using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        //Get Cart By UserId
        public async Task<ShoppingCartDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return null;

            return _mapper.Map<ShoppingCartDto>(cart);
        }


        //Add item to card
        public async Task AddItemToCartAsync(AddCartItemDto cartItemDto)
        {
            var cart = await _shoppingCartRepository.GetOrCreateCartAsync(cartItemDto.UserId);

            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            cartItem.CartId = cart.Id;

            await _shoppingCartRepository.AddCartItemAsync(cartItem);
        }

        //Remove cart
        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            await _shoppingCartRepository.RemoveCartItemAsync(cartItemId);
        }

        //Update
        public async Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem =await _shoppingCartRepository.GetCartItemByIdAsync(cartItemId);
            if(cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _shoppingCartRepository.UpdateCartItemAsync(cartItem);
            }
        }

        //Clear Cart
        public async Task ClearCartAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserIdAsync(userId);

            if (cart != null)
                await _shoppingCartRepository.ClearCartAsync(cart.Id);
        }


    }
}
