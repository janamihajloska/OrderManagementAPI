using AutoMapper;
using OrderManagement.Core.DTOs;
using OrderManagementSystem;

namespace OrderManagement.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _repo.AddAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(int id, CreateCustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return null;

            _mapper.Map(dto, customer);
            await _repo.UpdateAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }

        public async Task<CustomerDto?> GetCustomerWithOrdersAsync(int id)
        {
            var customer = await _repo.GetCustomerWithOrdersAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }
    }
}

