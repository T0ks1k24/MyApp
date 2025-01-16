using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get All Order
    public async Task<IEnumerable<Order>> GetAllOrderAsync()
    {
        var order = await _context.Orders
            .AsNoTracking()
            .ToListAsync();
        return order;
    }

    //Get Orders By User Id
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

    //Add New Order
    public async Task<bool> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return true;
    }

    //Update order.status
    public async Task<string> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new ArgumentException($"Order with ID {orderId} not found.");

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return $"Status updated to {status}";
    }

    //Remove order
    public async Task<bool> RemoveOrderAsync(int orderId)
    {
        var query = await _context.Orders.FindAsync(orderId);
        if (query == null) return false;
        
        _context.Remove(query);
        await _context.SaveChangesAsync();
        return true;
    }

    

   
}
