using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.services;

namespace Mirea.freelance.backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound("Заказ не найден.");
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var (success, message, order) = await _orderService.CreateOrderAsync(dto);
        if (!success)
            return BadRequest(message);
        return CreatedAtAction(nameof(GetOrder), new { id = order!.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto dto)
    {
        var (success, message, order) = await _orderService.UpdateOrderAsync(id, dto);
        if (!success)
            return BadRequest(message);
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var (success, message) = await _orderService.DeleteOrderAsync(id);
        if (!success)
            return NotFound(message);
        return Ok(message);
    }

    [HttpGet("company/{companyProfileId}")]
    public async Task<IActionResult> GetOrdersByCompanyId(int companyProfileId)
    {
        var orders = await _orderService.GetOrdersByCompanyIdAsync(companyProfileId);
        return Ok(orders);
    }

    [HttpGet("freelancer/{freelancerProfileId}")]
    public async Task<IActionResult> GetOrdersByFreelancerId(int freelancerProfileId)
    {
        var orders = await _orderService.GetOrdersByFreelancerIdAsync(freelancerProfileId);
        return Ok(orders);
    }

    [HttpGet("open")]
    public async Task<IActionResult> GetOpenOrders()
    {
        var orders = await _orderService.GetOpenOrdersAsync();
        return Ok(orders);
    }
}