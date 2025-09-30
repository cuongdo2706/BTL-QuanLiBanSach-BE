using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;
[ApiController]
[Route("/author")]
public class AuthorController:ControllerBase
{
    private readonly AuthorService _authorService;
    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var authors= _authorService.GetAllAuthors();
        return Ok(authors);
    }
    [HttpGet("search")]
    public IActionResult SearchCategory([FromQuery] string? name = null,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 10)
    {
        var result = _authorService.SearchAuthorPages(name, page, pageSize);

        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailById(long id)
    {
        try
        {
            var author = await _authorService.FindByIdAsync(id);
            if (author == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy tác giả" });
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var author = await _authorService.CreateAsync(request);
            return CreatedAtAction(nameof(GetDetailById), new { id = author.Id }, author);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] AuthorCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var author = await _authorService.UpdateAsync(id, request);
            if (author == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy tác giả" });
            }

            return Ok(author);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { success = false, message = "Không tìm thấy tác giả" });
            }

            return Ok(new { success = true, message = "Xóa tác giả thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

}