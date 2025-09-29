using Microsoft.AspNetCore.Mvc;
using BTL_QuanLiBanSach.Services;
using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;

namespace BTL_QuanLiBanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            var customer = await _customerService.GetById(id);
            return customer is null ? NotFound() : Ok(customer);
        }


        [HttpPost]
        public async Task<ActionResult<UserResponse>> Create(UserCreatedRequest request)
        {
            var created = await _customerService.Create(request);
            return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Update(int id, UserUpdatedRequest request)
        {
            var updated = await _customerService.Update(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerService.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
