using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Repositories
{
    public class PublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllPublisher()
        {
            return await _context.Publishers
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<(List<Publisher> Publishers, int CurrentPage, int TotalPages, int TotalItems, int PageSize)> GetAllPublisherPages(string? name, int page, int pageSize)
        {
            // Mặc định là chỉ lấy những NXB chưa bị xóa
            var query = _context.Publishers
                .Where(p => !p.IsDeleted);

            // Nếu name có giá trị thì thêm điều kiện lọc theo tên
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Đếm tổng số bản ghi để tính tổng trang
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Phân trang
            var publishers = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (publishers, page, totalPages, totalItems, pageSize);
        }

        public async Task<Publisher?> FindByIdAsync(long id)
        {
            return await _context.Publishers
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> ExistsByNameAsync(string name, long? excludeId = null)
        {
            var query = _context.Publishers
                .Where(p => p.Name == name && !p.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<Publisher?> UpdateAsync(long id, Publisher publisher)
        {
            var existingPublisher = await FindByIdAsync(id);
            if (existingPublisher == null)
                return null;

            existingPublisher.Name = publisher.Name;
            await _context.SaveChangesAsync();
            return existingPublisher;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var publisher = await FindByIdAsync(id);
            if (publisher == null)
                return false;

            publisher.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
