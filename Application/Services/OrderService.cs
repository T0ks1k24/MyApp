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

        public async Task<IEnumerable<OrderDto>> GetOrderForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }


        //Add order
        public async Task<OrderDto> AddOrderAsync(int userId, Order order)
        {
            var addOrder = _mapper.Map<Order>(order);
            addOrder.UserId = userId;
            addOrder.CreatedAt = DateTime.UtcNow;
            await _orderRepository.AddOrderAsync(addOrder);
            return _mapper.Map<OrderDto>(addOrder);
        }

        //Update order
        public async Task<OrderDto> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.UpdateOrderStatusAsync(orderId, status);
            return _mapper.Map<OrderDto>(order);
        }

        //Delete order
        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.RemoveOrderAsync(id);
        }

        //Get a order for user
        
    }
}
