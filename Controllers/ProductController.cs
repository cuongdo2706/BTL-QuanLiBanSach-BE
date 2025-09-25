using BTL_QuanLiBanSach.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;

[ApiController]
[Route("/product")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductController(ProductRepository repository)
    {
        _repository = repository;
    }

    // [HttpGet]
    // public async Task<IActionResult> FindAll()
    // {
    //     var products = await _repository.FindAll();
    //     return Ok(products);
    // }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var products = await _repository.FindAll();
        return Ok(products);
    }
}