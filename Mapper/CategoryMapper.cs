using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Services;

public class CategoryMapper
{
    public static CategoryResponse ToCategoryResponse(Category category)
    {
        return new CategoryResponse(category.Id, category.Name);
    }

    public static List<CategoryResponse> ToCategoryResponses(List<Category> categories)
    {
        return categories
            .Select(ToCategoryResponse)
            .ToList();
    }
}