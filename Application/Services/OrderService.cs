using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepositories;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepositories, IMapper mapper)
        {
            _orderRepositories = orderRepositories;
            _mapper = mapper;
        }


        //Get all orders
        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        //Get order by id
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        //Add order
        public async Task<OrderDto> CreateOrderAsync(int userId, Order order)
        {
            var addOrder = _mapper.Map<Order>(order);
            addOrder.UserId = userId;
            addOrder.CreatedAt = DateTime.UtcNow;
            await _orderRepository.AddAsync(addOrder);
            return _mapper.Map<OrderDto>(addOrder);
        }

        //Update order
        public async Task<OrderDto> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = _orderRepository.(orderId);
        }

        //Delete order
        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        //Get a order for user
        public async Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId)
        {
            var orders = await _orderRepositories.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
