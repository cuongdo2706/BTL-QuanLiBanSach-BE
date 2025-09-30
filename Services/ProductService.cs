using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BTL_QuanLiBanSach.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CloudinaryService _cloudinaryService;
    private readonly AuthorService _authorService;
    private readonly CategoryService _categoryService;
    private readonly PublisherService _publisherService;

    public ProductService(
        ProductRepository productRepository,
        CloudinaryService cloudinaryService,
        AuthorService authorService,
        CategoryService categoryService,
        PublisherService publisherService
    )
    {
        _productRepository = productRepository;
        _cloudinaryService = cloudinaryService;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    public PageResponse<ProductResponse> SearchProduct(ProductFilterRequest request)
    {
        return _productRepository.SearchProduct(request).Result;
    }

    public ProductResponse FindById(long id)
    {
        return ProductMapper.ToProductResponse(_productRepository.FindById(id).Result);
    }

    public ProductResponse Save(ProductCreatedRequest request, IFormFile file)
    {
        ImageResponse imageResponse = _cloudinaryService.upload(file).Result;
        List<Author> authors = _authorService.GetAllByIds(request.authorIds);
        List<Category> categories = _categoryService.GetAllByIds(request.categoryIds);
        Publisher publisher = _publisherService.FindEntityById(request.publisherId);
        Product product = new Product();
        product.Code = request.code;
        product.Description = request.description;
        product.ImgUrl = imageResponse.imgUrl;
        product.PublicId = imageResponse.publicId;
        product.IsActive = true;
        product.IsDeleted = false;
        product.Name = request.name;
        product.NumOfPages = request.numOfPages;
        product.Price = request.price;
        product.PublishedYear = request.publishedYear;
        product.Quantity = request.quantity;
        product.Translator = request.translator;
        product.Publisher = publisher;
        product.Authors = authors;
        product.Categories = categories;
        return ProductMapper.ToProductResponse(_productRepository.Save(product).Result);
    }

    public ProductResponse Update(long id, ProductCreatedRequest request, IFormFile file)
    {
        Product existedProduct = _productRepository.FindById(id).Result;


        List<Author> authors = _authorService.GetAllByIds(request.authorIds);
        List<Category> categories = _categoryService.GetAllByIds(request.categoryIds);
        Publisher publisher = _publisherService.FindEntityById(request.publisherId);
        if (file != null && file.Length <= 0)
        {
            ImageResponse imageResponse;
            if (!existedProduct.PublicId.IsNullOrEmpty())
                imageResponse = _cloudinaryService.update(existedProduct.PublicId, file).Result;
            else imageResponse = _cloudinaryService.upload(file).Result;
            existedProduct.ImgUrl = imageResponse.imgUrl;
            existedProduct.PublicId = imageResponse.publicId;
        }

        existedProduct.Description = request.description;
        existedProduct.IsActive = true;
        existedProduct.IsDeleted = false;
        existedProduct.Name = request.name;
        existedProduct.NumOfPages = request.numOfPages;
        existedProduct.Price = request.price;
        existedProduct.PublishedYear = request.publishedYear;
        existedProduct.Quantity = request.quantity;
        existedProduct.Translator = request.translator;
        existedProduct.Publisher = publisher;
        existedProduct.Authors = authors;
        existedProduct.Categories = categories;
        return ProductMapper.ToProductResponse(_productRepository.Update(existedProduct).Result);
    }

    public void Delete(long id)
    {
        Product existedProduct = _productRepository.FindById(id).Result;
        existedProduct.IsDeleted = true;
        _productRepository.Update(existedProduct);
    }
}