using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> FindAll()
    {
        return await _context.Products.ToListAsync();
    }
}