using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> FindAll()
    {
        return await _context.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();
    }

    public async Task<User?> FindById(long id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    }

    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Update(long id, User user)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> Delete(long id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        if (user == null) return false;

        user.IsDeleted = true; // chỉ đánh dấu xóa mềm
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
