using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public PageResponse<ProductResponse> SearchProduct(ProductFilterRequest request)
    {
        return _productRepository.SearchProduct(request).Result;
    }

    public ProductResponse FindById(long id)
    {
        return _productRepository.FindById(id).Result;
    }
}