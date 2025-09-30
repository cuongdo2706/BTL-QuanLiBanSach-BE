using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BTL_QuanLiBanSach.Repositories;

public class CustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.Where(u => !u.UserType).ToListAsync();
    }

    public async Task<List<User>> SearchCustomer(UserFilterRequest request)
    {
        IQueryable<User> query = _context.Users;

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.Contains(request.Name));
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNum))
        {
            query = query.Where(c => c.PhoneNum.Contains(request.PhoneNum));
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            query = query.Where(c => c.Email.Contains(request.Email));
        }

        return await query.ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users
/*            .Include(c => c.Orders)*/
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Add(User customer)
    {
        _context.Users.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User customer)
    {
        _context.Users.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var customer = await _context.Users.FindAsync(id);
        if (customer != null)
        {
            _context.Users.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
