using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryResponse> GetAllCategories()
        {

            return CategoryMapper.ToCategoryResponses(_categoryRepository.GetAllCategory().Result);
        }

        public PageResponse<CategoryResponse> GeachCategoryPages(string? name, int page, int pageSize)
        {
            var (categorys, currentPages, totalPages, totalItems, pageSizeResult) = _categoryRepository.GetAllCategoryPages(name, page, pageSize).Result;
            var categoryResponses = CategoryMapper.ToCategoryResponses(categorys);

            return new PageResponse<CategoryResponse>(content: categoryResponses,
                size: pageSizeResult,
                page: currentPages,
                totalElements: totalItems,
                totalPages: totalPages);
        }

        public async Task<CategoryResponse?> FindByIdAsync(long id)
        {
            var category = await _categoryRepository.FindByIdAsync(id);
            return category != null ? CategoryMapper.ToCategoryResponse(category) : null;
        }

        public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
        {
            // Kiểm tra tên loại sách đã tồn tại chưa
            if (await _categoryRepository.ExistsByNameAsync(request.Name))
            {
                throw new InvalidOperationException($"Loại sách với tên '{request.Name}' đã tồn tại");
            }

            var category = new Category
            {
                Name = request.Name,
                IsDeleted = false
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);
            return CategoryMapper.ToCategoryResponse(createdCategory);
        }


        public async Task<CategoryResponse?> UpdateAsync(long id, CategoryCreateRequest request)
        {
            // Kiểm tra tên loại sách đã tồn tại chưa (trừ chính nó)
            if (await _categoryRepository.ExistsByNameAsync(request.Name, id))
            {
                throw new InvalidOperationException($"Loại sách với tên '{request.Name}' đã tồn tại");
            }

            var category = new Category
            {
                Name = request.Name
            };

            var updatedCategory = await _categoryRepository.UpdateAsync(id, category);
            return updatedCategory != null ? CategoryMapper.ToCategoryResponse(updatedCategory) : null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }


    }
}
