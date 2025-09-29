using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // 📋 Danh sách user
    [HttpGet]
    public IActionResult FindAll()
    {
        var users = _userService.FindAll();
        return Ok(users);
    }

    // 🔎 Lấy user theo id
    [HttpGet("{id:long}")]
    public IActionResult FindById(long id)
    {
        var user = _userService.FindById(id);
        return user is null ? NotFound() : Ok(user);
    }

    // ➕ Thêm user
    [HttpPost]
    public IActionResult Create([FromBody] UserCreatedRequest request)
    {
        var created = _userService.Add(request);
        return CreatedAtAction(nameof(FindById), new { id = created.id }, created);
    }

    // ✏️ Sửa user
    [HttpPut("{id:long}")]
    public IActionResult Update(long id, [FromBody] UserUpdatedRequest request)
    {
        var updated = _userService.Update(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    // ❌ Xóa user
    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        var deleted = _userService.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
