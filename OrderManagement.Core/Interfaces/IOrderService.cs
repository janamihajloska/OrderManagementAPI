using OrderManagementSystem;

namespace OrderManagement.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetOrderAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersForCustomerAsync(int customerId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto> UpdateOrderAsync(int id, OrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
