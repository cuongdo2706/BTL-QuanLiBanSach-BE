using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
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

    public async Task<bool> ExistedByPhoneNum(string phoneNum)
    {
        return await _context.Users.AnyAsync(c => c.PhoneNum.Equals(phoneNum));
    }

    public async Task<PageResponse<UserResponse>> SearchCustomer(UserFilterRequest request)
    {
        IQueryable<User> query = _context.Users;

        switch (request.sortBy)
        {
            case "name":
                query = query.OrderBy(c => c.Name); break;
            case "name-d":
                query = query.OrderByDescending(c => c.Name); break;
            case "code":
                query = query.OrderBy(c => c.Code); break;
            case "code-d":
                query = query.OrderByDescending(p => p.Code); break;
        }

        if (!string.IsNullOrWhiteSpace(request.nameOrCodeOrPhoneNumKeyword))
        {
            query = query.Where(c =>
                c.Code.Contains(request.nameOrCodeOrPhoneNumKeyword) ||
                c.Name.Contains(request.nameOrCodeOrPhoneNumKeyword) ||
                c.PhoneNum.Contains(request.nameOrCodeOrPhoneNumKeyword)
            );
        }

        query = query.Where(c => !c.UserType && !c.IsDeleted);
        int totalElements = await query.CountAsync();
        int skip = (request.page - 1) * request.size;
        List<User> customer = await query
            .Skip(skip)
            .Take(request.size)
            .ToListAsync();

        return new PageResponse<UserResponse>(
            UserMapper.ToUserResponses(customer),
            request.size,
            request.page,
            totalElements,
            (int)Math.Ceiling(totalElements / (double)request.size)
        );
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users
/*            .Include(c => c.Orders)*/
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
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