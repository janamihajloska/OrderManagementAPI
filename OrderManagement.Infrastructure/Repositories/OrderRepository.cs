using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Customer)
                .ToListAsync();
        }
        public Task<Order?> GetByIdAsync(int id) =>
            _context.Orders.Include(o => o.Items)
                           .Include(o => o.Customer)
                           .FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }



        public Task<List<Order>> GetOrdersForCustomerAsync(int customerId) =>
            _context.Orders.Where(o => o.CustomerId == customerId)
                           .Include(o => o.Items)
                           .ToListAsync();

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
