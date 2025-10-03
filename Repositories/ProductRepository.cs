using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
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

    public async Task<PageResponse<ProductResponse>> SearchProduct(ProductFilterRequest request)
    {
        IQueryable<Product> query = _context.Products;
        switch (request.sortBy)
        {
            case "name":
                query = query.OrderBy(p => p.Name); break;
            case "name-d":
                query = query.OrderByDescending(p => p.Name); break;
            case "price":
                query = query.OrderBy(p => p.Price); break;
            case "price-d":
                query = query.OrderByDescending(p => p.Price); break;
        }

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

        if (!string.IsNullOrWhiteSpace(request.nameOrCodeKeyword))
        {
            query = query.Where(p =>
                p.Name.Contains(request.nameOrCodeKeyword) || p.Code.Contains(request.nameOrCodeKeyword));
        }

        int totalElements = await query.CountAsync();
        int skip = (request.page - 1) * request.size;
        var products = await query
            .Where(p => p.IsActive == request.isActive&&!p.IsDeleted)
            .Include(p => p.Authors)
            .Include(p => p.Categories)
            .Include(p => p.Publisher)
            .Skip(skip)
            .Take(request.size)
            .ToListAsync();
        return new PageResponse<ProductResponse>(
            ProductMapper.ToProductResponses(products),
            request.size,
            request.page,
            totalElements,
            (int)Math.Ceiling(totalElements / (double)request.size));
    }

    public async Task<Product> FindById(long id)
    {
        return await _context.Products
            .Include(p => p.Authors)
            .Include(p => p.Categories)
            .Include(p => p.Publisher)
            .Where(p => p.Id == id && !p.IsDeleted)
            .SingleAsync();
        ;
    }

    public async Task<Product> Save(Product product)
    {
        Product newProduct = _context.Products.Add(product).Entity;
        await _context.SaveChangesAsync();
        return newProduct;
    }

    public async Task<Product> Update(Product product)
    {
        Product newProduct = _context.Products.Update(product).Entity;
        await _context.SaveChangesAsync();
        return newProduct;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
            throw new InvalidOperationException($"Product with Id {productId} not found");

        return product;
    }
}