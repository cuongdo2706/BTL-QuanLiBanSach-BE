using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BTL_QuanLiBanSach.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> SearchProduct(ProductFilterRequest request)
    {
        IQueryable<Product> query = _context.Products;
        if (!request.publisherIds.IsNullOrEmpty())
        {
            query = query.Where(p => request.publisherIds.Contains(p.PublisherId));
        }

        if (!request.authorIds.IsNullOrEmpty())
        {
            query = query.Where(p => p.Authors.Any(a => request.authorIds.Contains(a.Id)));
        }
        if (!request.categoryIds.IsNullOrEmpty())
        {
            query = query.Where(p => p.Categories.Any(c => request.categoryIds.Contains(c.Id)));
        }

        return await query
            .Include(p => p.Authors)
            .Include(p => p.Categories)
            .Include(p => p.Publisher)
            .ToListAsync();
    }
}