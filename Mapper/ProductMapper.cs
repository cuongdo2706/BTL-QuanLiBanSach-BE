using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Services;

namespace BTL_QuanLiBanSach.Mapper;

public class ProductMapper
{
    public static ProductResponse ToProductResponse(Product product)
    {
        List<AuthorResponse> authors = AuthorMapper.ToAuthorResponses(product.Authors);
        List<CategoryResponse> categories = CategoryMapper.ToCategoryResponses(product.Categories);
        return new ProductResponse(
            product.Id,
            product.Code,
            product.Description,
            product.ImgUrl,
            product.IsDeleted,
            product.IsActive,
            product.Name,
            product.NumOfPages,
            product.Price,
            product.PublicId,
            product.PublishedYear,
            product.Quantity,
            product.Translator,
            PublisherMapper.ToPublisherResponse(product.Publisher),
            authors,
            categories
        );
    }

    public static List<ProductResponse> ToProductResponses(List<Product> products)
    {
        return products.Select(ToProductResponse).ToList();
    }
}