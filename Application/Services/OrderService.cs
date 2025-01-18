using Application.DTOs;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        //Get all orders
        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        //Get orders for user
        public async Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }


        //Add order
        public async Task<bool> AddOrderAsync(int userId, OrderDto orderDto)
        {
            if (userId < 0 && orderDto == null)
                return false;

            var addOrder = _mapper.Map<Order>(orderDto);
            addOrder.UserId = userId;
            addOrder.CreatedAt = DateTime.UtcNow;
            await _orderRepository.AddOrderAsync(addOrder);
            return true;
        }

        //Update order
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            if (orderId < 0 || string.IsNullOrEmpty(status))
                return false;

            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
            return true;
        }

        //Delete order
        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id < 0)
                return false;

            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return false;

            await _orderRepository.RemoveOrderAsync(id);
            return true;
        }

        
    }
}
