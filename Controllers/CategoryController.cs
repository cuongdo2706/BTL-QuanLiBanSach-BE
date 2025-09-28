using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;

[ApiController]
[Route("/category")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryService.GetAllCategories();
        return Ok(categories);
    }

    [HttpGet("search")]
    public IActionResult searchCategory([FromQuery] string? name = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    { var result = _categoryService.GeachCategoryPages(name, page, pageSize);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailById(long id)
    {
        try
        {
            var category = await _categoryService.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy loại sách" });
            }

            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        } 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var category = await _categoryService.CreateAsync(request);
            return CreatedAtAction(nameof(GetDetailById), new { id = category.Id }, category);
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
    public async Task<IActionResult> Update(long id, [FromBody] CategoryCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var category = await _categoryService.UpdateAsync(id, request);
            if (category == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy loại sách" });
            }

            return Ok(category);
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
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { success = false, message = "Không tìm thấy loại sách" });
            }

            return Ok(new { success = true, message = "Xóa loại sách thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

}