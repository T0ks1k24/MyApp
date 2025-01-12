using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrderAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> AddOrderAsync(int userId, Order order);
        Task DeleteOrderAsync(int id);
        Task<OrderDto> UpdateOrderAsync(Order order);

        Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId);



    }
}
