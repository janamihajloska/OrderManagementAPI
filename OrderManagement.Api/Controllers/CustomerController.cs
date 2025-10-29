using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.DTOs;
using OrderManagement.Core.Services;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _service.GetCustomerAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllCustomersAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            var result = await _service.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateCustomerDto dto)
        {
            var result = await _service.UpdateCustomerAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteCustomerAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int id)
        {
            var customer = await _service.GetCustomerWithOrdersAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }
    }
}
