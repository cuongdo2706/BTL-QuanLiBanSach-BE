using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Repositories;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Cors;
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


    [HttpGet("search")]
    public IActionResult FindAll([FromQuery] ProductFilterRequest request)
    {
        var products = _productService.SearchProduct(request);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult FindById(long id)
    {
        return Ok(_productService.FindById(id));
    }
}