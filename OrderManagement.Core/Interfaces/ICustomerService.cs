using OrderManagement.Core.DTOs;

namespace OrderManagement.Core.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<CustomerDto?> UpdateCustomerAsync(int id, CreateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);

        Task<CustomerDto?> GetCustomerWithOrdersAsync(int id);
    }
}