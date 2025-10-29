using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Interfaces;


namespace OrderManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        // ✅ GET: api/orders/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _service.GetOrderAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // ✅ GET: api/orders/customer/{customerId}
        [HttpGet("customer/{customerId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersForCustomer(int customerId)
        {
            var orders = await _service.GetOrdersForCustomerAsync(customerId);
            return Ok(orders);
        }

        // ✅ POST: api/orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var createdOrder = await _service.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderById),
                new { id = createdOrder.Id },
                createdOrder);
        }

        // ✅ PUT: api/orders/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch between URL and body");

            var updatedOrder = await _service.UpdateOrderAsync(id, dto);
            if (updatedOrder == null) return NotFound();

            return Ok(updatedOrder);
        }

        // ✅ DELETE: api/orders/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteOrderAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
