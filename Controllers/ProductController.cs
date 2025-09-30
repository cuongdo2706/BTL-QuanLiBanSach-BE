using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Repositories;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpPost]
    public IActionResult Save(
        [FromForm] string product,
        [FromForm] IFormFile? file
        
    )
    {
        var productObj = JsonConvert.DeserializeObject<ProductCreatedRequest>(product);
        return Ok(_productService.Save(productObj, file));
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(
        long id,
        [FromForm] string product,
        [FromForm] IFormFile? file
        
    )
    {
        var productObj = JsonConvert.DeserializeObject<ProductCreatedRequest>(product);
        return Ok(_productService.Update(id,productObj, file));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _productService.Delete(id);
        return Ok(null);
    }
}