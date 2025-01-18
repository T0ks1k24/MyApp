using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrderAsync();
            if (orders == null || orders.Any())
                return NotFound("No orders found!");
            return Ok(orders);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderForUser(int id)
    {
        try
        {
            var orderForUser = _orderService.GetOrderForUserAsync(id);
            if(orderForUser == null)
                return NotFound($"Order with user ID {id} not found.");
            return Ok(orderForUser);
        }
        catch (Exception ex) 
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddOrder(int userId, [FromBody] OrderDto orderDto)
    {
        if (orderDto == null)
            return BadRequest("Order data cannot be null.");

        if (userId <= 0)
            return BadRequest("Invalid user ID.");

        try
        {
            var addedOrder = await _orderService.AddOrderAsync(userId, orderDto);
            if(addedOrder)
                return Ok(new { Message = "Order added successfully!"});
            return BadRequest("Failed to add the product.");
        } 
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        if (id < 0)
            return BadRequest("Invalid order ID.");

        if (string.IsNullOrWhiteSpace(status))
            return BadRequest("Status cannot be null or empty.");

        try
        {
            var isUpdated = await _orderService.UpdateOrderStatusAsync(id, status);

            if (isUpdated)
                return Ok("Order status updated successfully.");

            return NotFound("Order not found or status update failed.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveOrder(int id)
    {
        try
        {
            bool removeOrder = await _orderService.DeleteOrderAsync(id);
            if (removeOrder)
                return Ok("Order removed successfully!");

            return NotFound($"Order with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
