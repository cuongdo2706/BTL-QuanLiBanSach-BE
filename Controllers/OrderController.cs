using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;
[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("getAll")]
    public async Task<IActionResult> GetAllOrders([FromBody] PagingRequest pagingRequest)
    {
        var result = await _orderService.GetAllOrdersAsync(pagingRequest);
        return Ok(result);
    }

    // GET: api/order/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    // POST: api/order/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreatedRequest request)
    {
        var result = await _orderService.CreateAsync(request);
        return Ok(result);
    }

    // PUT: api/order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Order order)
    {
        if (id != order.Id) return BadRequest();

        var updated = await _orderService.UpdateAsync(order);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE: api/order/5
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _orderService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}

