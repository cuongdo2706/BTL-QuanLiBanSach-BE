using BTL_QuanLiBanSach.Repositories;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;

[ApiController]
[Route("/product")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    

    [HttpGet]
    public IActionResult FindAll()
    {
        var products = _productService.FindAll();
        return Ok(products);
    }
}