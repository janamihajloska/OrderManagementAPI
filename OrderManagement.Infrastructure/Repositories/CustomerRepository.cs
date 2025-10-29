using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(int id)
            => await _context.Customers.FindAsync(id);

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await _context.Customers.ToListAsync();

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer == null) return;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerWithOrdersAsync(int id)
            => await _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.Items)
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}
