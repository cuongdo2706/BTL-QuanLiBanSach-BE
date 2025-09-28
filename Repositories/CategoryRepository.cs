

using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;
     

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await _context.Categories.
                Where(c =>!c.IsDeleted)
                .ToListAsync();
        }
        public async Task<(List<Category> Categories, int CurrentPage, int TotalPages, int TotalItems, int PageSize)> GetAllCategoryPages(string? name, int page, int pageSize)
        {
            // mac dinh la phai tim loai chua bi xoa
            var query = _context.Categories
            .Where(c => !c.IsDeleted);

            // neu name khac null va khac rong thi add them dieu kien where vao
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name))  ; 
            }

            // dem so trang de tra ve page va total pages
            var totalItems = await query.CountAsync();

            var totalPages = (int) Math.Ceiling((double)totalItems / pageSize);

            // Phân trang
            var categories = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return (categories, page, totalPages, totalItems, pageSize);
        }

        public async Task<Category?> FindByIdAsync(long id)
        {
            return await _context.Categories
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> ExistsByNameAsync(string name, long? excludeId = null)
        {
            var query = _context.Categories
                .Where(c => c.Name == name && !c.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }


        public async Task<Category?> UpdateAsync(long id, Category category)
        {
            var existingCategory = await FindByIdAsync(id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var category = await FindByIdAsync(id);
            if (category == null)
                return false;

            category.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }

      
    }
