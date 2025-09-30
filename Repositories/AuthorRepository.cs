using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories
{
    public class AuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthor()
        {
            return await _context.Authors.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<List<Author>> GetAllByIds(List<long> ids)
        {
            return await _context.Authors.Where(a => ids.Contains(a.Id)&&!a.IsDeleted).ToListAsync();
        }

        public async Task<(List<Author> Authors, int CurrentPage, int TotalPages, int TotalItems, int PageSize)>
            GetAllAuthorPages(string? name, int page, int pageSize)
        {
            var query = _context.Authors.Where(c => !c.IsDeleted);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }


            var totalItems = await query.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var authors = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (authors, page, totalPages, totalItems, pageSize);
        }

        public async Task<Author?> FindByIdAsync(long id)
        {
            return await _context.Authors
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Author> CreateAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> ExistsByNameAsync(string name, long? excludeId = null)
        {
            var query = _context.Authors
                .Where(a => a.Name == name && !a.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(a => a.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<Author?> UpdateAsync(long id, Author author)
        {
            var existingAuthor = await FindByIdAsync(id);
            if (existingAuthor == null)
                return null;

            existingAuthor.Name = author.Name;
            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var author = await FindByIdAsync(id);
            if (author == null)
                return false;

            author.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}