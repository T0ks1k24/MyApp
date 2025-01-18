using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get Cart By UserID
    public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
    {
        return await _context.ShoppingCarts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    //Get or Create Cart
    public async Task<ShoppingCart> GetOrCreateCartAsync(int userId)
    {
        var cart = await GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            cart = new ShoppingCart { UserId = userId };
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<bool> AddCartItemAsync(CartItem cartItem)
    {
        if(cartItem == null)
            return false;

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveCartItemAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);

        if(cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
    {
        if (cartItem == null)
            return false;

        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> ClearCartAsync(int cartId)
    {
        if (cartId < 0)
            return false;

        var cart = await _context.ShoppingCarts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if(cart != null )
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
    }

}
