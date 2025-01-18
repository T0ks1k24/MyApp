using Application.DTOs;

namespace Application.Interfaces.IServices;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrderAsync();
    Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId);
    Task<bool> AddOrderAsync(int userId, OrderDto orderDto);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> DeleteOrderAsync(int id);
}
