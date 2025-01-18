using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrderAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    Task<bool> AddOrderAsync(Order order);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> RemoveOrderAsync(int orderId);
}
