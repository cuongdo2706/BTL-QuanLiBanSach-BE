using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories
{
    public class OrderRepository
    {
        private AppDbContext _appDbContext;
        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IQueryable<Order> GetAllOrdersQuery()
        {
            return _appDbContext.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Staff);
        }
        public async Task<Order?> GetByIdAsync(long id)
        {
            return await _appDbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Order> AddAsync(Order order)
        {
            _appDbContext.Orders.Add(order);
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.Entry(order).Reference(o => o.User).LoadAsync();
            await _appDbContext.Entry(order).Reference(o => o.Staff).LoadAsync();
            await _appDbContext.Entry(order).Collection(o => o.OrderDetails).LoadAsync();

            return order;
        }
        public async Task<Order?> UpdateAsync(Order order)
        {
            var existing = await _appDbContext.Orders.FindAsync(order.Id);
            if (existing == null) return null;

            _appDbContext.Entry(existing).CurrentValues.SetValues(order);
            await _appDbContext.SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var order = await _appDbContext.Orders
                 .Include(o => o.OrderDetails)
        .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return false;
            _appDbContext.OrderDetails.RemoveRange(order.OrderDetails);
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _appDbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

    }
}
