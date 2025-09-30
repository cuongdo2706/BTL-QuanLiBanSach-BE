using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services
{
    public class PublisherService
    {
        private readonly PublisherRepository _publisherRepository;

        public PublisherService(PublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public List<PublisherResponse> GetAllPublishers()
        {
            return PublisherMapper.ToPublisherResponses(_publisherRepository.GetAllPublisher().Result);
        }

        public PageResponse<PublisherResponse> GetAllPublisherPages(string? name, int page, int pageSize)
        {
            var (publishers, currentPages, totalPages, totalItems, pageSizeResult) = _publisherRepository.GetAllPublisherPages(name, page, pageSize).Result;
            var publisherResponses = PublisherMapper.ToPublisherResponses(publishers);

            return new PageResponse<PublisherResponse>(content: publisherResponses,
                size: pageSizeResult,
                page: currentPages,
                totalElements: totalItems,
                totalPages: totalPages);
        }

        public async Task<PublisherResponse?> FindByIdAsync(long id)
        {
            var publisher = await _publisherRepository.FindByIdAsync(id);
            return publisher != null ? PublisherMapper.ToPublisherResponse(publisher) : null;
        }

        public Publisher FindEntityById(long id)
        {
            return _publisherRepository.FindByIdAsync(id).Result;
        }
        
        
        public async Task<PublisherResponse> CreateAsync(PublisherCreateRequest request)
        {
            // Kiểm tra tên nhà xuất bản đã tồn tại chưa
            if (await _publisherRepository.ExistsByNameAsync(request.Name))
            {
                throw new InvalidOperationException($"Nhà xuất bản với tên '{request.Name}' đã tồn tại");
            }

            var publisher = new Publisher
            {
                Name = request.Name,
                IsDeleted = false
            };

            var createdPublisher = await _publisherRepository.CreateAsync(publisher);
            return PublisherMapper.ToPublisherResponse(createdPublisher);
        }

        public async Task<PublisherResponse?> UpdateAsync(long id, PublisherCreateRequest request)
        {
            // Kiểm tra tên nhà xuất bản đã tồn tại chưa (trừ chính nó)
            if (await _publisherRepository.ExistsByNameAsync(request.Name, id))
            {
                throw new InvalidOperationException($"Nhà xuất bản với tên '{request.Name}' đã tồn tại");
            }

            var publisher = new Publisher
            {
                Name = request.Name
            };

            var updatedPublisher = await _publisherRepository.UpdateAsync(id, publisher);
            return updatedPublisher != null ? PublisherMapper.ToPublisherResponse(updatedPublisher) : null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _publisherRepository.DeleteAsync(id);
        }
    }
}
