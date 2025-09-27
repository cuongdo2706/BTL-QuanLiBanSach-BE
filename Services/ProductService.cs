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

    public List<ProductResponse> FindAll()
    {
        return ProductMapper.ToProductResponses(_productRepository.FindAll().Result);
    }
}