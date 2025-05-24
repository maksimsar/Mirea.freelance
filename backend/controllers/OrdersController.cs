using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.services;
using System.Threading.Tasks;


namespace Mirea.freelance.backend.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Заказ не найден.");
            return Ok(order);
        }

        [HttpGet("by-mentor/{mentorProfileId}")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByMentor(int mentorProfileId)
        {
            var orders = await _orderService.GetOrdersByMentorIdAsync(mentorProfileId);
            return Ok(orders);
        }

        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByStatus(OrderStatus status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var (success, message, order) = await _orderService.CreateOrderAsync(dto);
            if (!success)
                return BadRequest(new { message });
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, message, order) = await _orderService.UpdateOrderAsync(id, dto);
            if (!success)
                return BadRequest(new { message });
            return Ok(order);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            var (success, message) = await _orderService.ChangeStatusAsync(id, dto);
            if (!success)
                return BadRequest(new { message });
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var (success, message) = await _orderService.DeleteOrderAsync(id);
            if (!success)
                return BadRequest(new { message });
            return Ok(new { message });
        }

        [HttpPost("{id}/assign-mentor/{mentorProfileId}")]
        [Authorize(Roles = "Mentor,Company")]
        public async Task<IActionResult> AssignMentor(int id, int mentorProfileId)
        {
            var (success, message) = await _orderService.AssignMentorAsync(id, mentorProfileId);
            if (!success)
                return BadRequest(new { message });
            return Ok(new { message });
        }

        [HttpPost("{id}/attach-student/{studentProfileId}")]
        [Authorize(Roles = "Mentor")]
        public async Task<IActionResult> AttachStudent(int id, int studentProfileId)
        {
            var (success, message) = await _orderService.AttachStudentAsync(id, studentProfileId);
            if (!success)
                return BadRequest(new { message });
            return Ok(new { message });
        }
    }
}