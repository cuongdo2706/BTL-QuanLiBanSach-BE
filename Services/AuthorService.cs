using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<AuthorResponse> GetAllAuthors()
        {
            return AuthorMapper.ToAuthorResponses(_authorRepository.GetAllAuthor().Result);
        }

        public List<Author> GetAllByIds(List<long> ids)
        {
            return _authorRepository.GetAllByIds(ids).Result;
        }

        public PageResponse<AuthorResponse> SearchAuthorPages(string? name, int page, int pageSize)
        {
            var (authors, currentPages, totalPages, totalItems, pageSizeResult) =
                _authorRepository.GetAllAuthorPages(name, page, pageSize).Result;
            var authorResponses = AuthorMapper.ToAuthorResponses(authors);

            return new PageResponse<AuthorResponse>(content: authorResponses,
                size: pageSizeResult,
                page: currentPages,
                totalElements: totalItems,
                totalPages: totalPages);
        }

        public async Task<AuthorResponse?> FindByIdAsync(long id)
        {
            var author = await _authorRepository.FindByIdAsync(id);
            return author != null ? AuthorMapper.ToAuthorResponse(author) : null;
        }

        public async Task<AuthorResponse> CreateAsync(AuthorCreateRequest request)
        {
            // Kiểm tra tên tác giả đã tồn tại chưa
            if (await _authorRepository.ExistsByNameAsync(request.Name))
            {
                throw new InvalidOperationException($"Tác giả với tên '{request.Name}' đã tồn tại");
            }

            var author = new Author
            {
                Name = request.Name,
                IsDeleted = false
            };

            var createdAuthor = await _authorRepository.CreateAsync(author);
            return AuthorMapper.ToAuthorResponse(createdAuthor);
        }

        public async Task<AuthorResponse?> UpdateAsync(long id, AuthorCreateRequest request)
        {
            // Kiểm tra tên tác giả đã tồn tại chưa (trừ chính nó)
            if (await _authorRepository.ExistsByNameAsync(request.Name, id))
            {
                throw new InvalidOperationException($"Tác giả với tên '{request.Name}' đã tồn tại");
            }

            var author = new Author
            {
                Name = request.Name
            };

            var updatedAuthor = await _authorRepository.UpdateAsync(id, author);
            return updatedAuthor != null ? AuthorMapper.ToAuthorResponse(updatedAuthor) : null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _authorRepository.DeleteAsync(id);
        }
    }
}