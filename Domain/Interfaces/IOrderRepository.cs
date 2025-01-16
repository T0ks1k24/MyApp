using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrderAsync();
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    Task<bool> AddOrderAsync(Order order);
    Task<string> UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> RemoveOrderAsync(int orderId);
}
