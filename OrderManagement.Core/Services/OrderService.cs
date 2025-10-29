using AutoMapper;
using OrderManagement.Core.Interfaces;

namespace OrderManagementSystem
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // Create an order from CreateOrderDto → returns OrderDto
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            // Check that customer exists
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId)
                ?? throw new Exception("Customer not found");

            // Map DTO → Entity
            var order = _mapper.Map<Order>(dto);
            order.CustomerId = dto.CustomerId;
            order.OrderDate = DateTime.UtcNow;

            await _orderRepository.AddAsync(order);

            // Map Entity → DTO for response
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, OrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return null;

            _mapper.Map(dto, order);
            await _orderRepository.UpdateAsync(order);

            return _mapper.Map<OrderDto>(order);
        }

        // Get order by ID → returns DTO
        public async Task<OrderDto?> GetOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        // Get all orders for a customer → returns list of DTOs
        public async Task<IEnumerable<OrderDto>> GetOrdersForCustomerAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersForCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        // Delete order → returns bool
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _orderRepository.DeleteAsync(order);
            return true;
        }
    }
}
