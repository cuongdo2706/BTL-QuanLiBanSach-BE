using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTL_QuanLiBanSach.Controllers;

[ApiController]
[Route("/publisher")]
public class PublisherController : ControllerBase
{
    private readonly PublisherService _publisherService;

    public PublisherController(PublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var publishers = _publisherService.GetAllPublishers();
        return Ok(publishers);
    }

    [HttpGet("search")]
    public IActionResult SearchPublisher([FromQuery] string? name = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = _publisherService.GetAllPublisherPages(name, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailById(long id)
    {
        try
        {
            var publisher = await _publisherService.FindByIdAsync(id);
            if (publisher == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy nhà xuất bản" });
            }

            return Ok(publisher);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PublisherCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var publisher = await _publisherService.CreateAsync(request);
            return CreatedAtAction(nameof(GetDetailById), new { id = publisher.Id }, publisher);
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
    public async Task<IActionResult> Update(long id, [FromBody] PublisherCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var publisher = await _publisherService.UpdateAsync(id, request);
            if (publisher == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy nhà xuất bản" });
            }

            return Ok(publisher);
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
            var result = await _publisherService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new { success = false, message = "Không tìm thấy nhà xuất bản" });
            }

            return Ok(new { success = true, message = "Xóa nhà xuất bản thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi server: {ex.Message}" });
        }
    }
}
