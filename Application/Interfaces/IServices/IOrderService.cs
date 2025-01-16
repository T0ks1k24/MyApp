using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrderAsync();
        Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId);
        Task<OrderDto> AddOrderAsync(int userId, Order order);
        Task<OrderDto> UpdateOrderStatusAsync(int orderId, string status);
        Task DeleteOrderAsync(int id);
    }
}
