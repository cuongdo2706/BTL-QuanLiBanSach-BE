using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepo;

    public CustomerService(CustomerRepository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    // Lấy toàn bộ khách hàng
    public async Task<List<UserResponse>> GetAllCustomersAsync()
    {
        var customers = await _customerRepo.GetAll();
        return customers.Select(UserMapper.ToUserResponse).ToList();
    }

    // Tìm kiếm khách hàng
    public async Task<List<UserResponse>> SearchCustomer(UserFilterRequest request)
    {
        var customers = await _customerRepo.SearchCustomer(request);
        return customers.Select(UserMapper.ToUserResponse).ToList();
    }

    // Lấy theo Id
    public async Task<UserResponse?> GetById(int id)
    {
        var customer = await _customerRepo.GetById(id);
        return customer == null ? null : UserMapper.ToUserResponse(customer);
    }

    // Tạo mới khách hàng
    public async Task<UserResponse> Create(UserCreatedRequest request)
    {
        var existing = await _customerRepo.SearchCustomer(new UserFilterRequest { Email = request.Email });
        if (existing.Any())
        {
            throw new Exception("Email đã tồn tại!");
        }

        var newCustomer = new User
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNum = request.PhoneNum,
            Address = request.Address,
            Code = request.Code,
            Dob = request.Dob,
            Gender = request.Gender,
            UserType = false,
            IsDeleted = false
        };

        await _customerRepo.Add(newCustomer);
        return UserMapper.ToUserResponse(newCustomer);
    }

    // Cập nhật khách hàng
    public async Task<UserResponse?> Update(int id, UserUpdatedRequest request)
    {
        var existing = await _customerRepo.GetById(id);
        if (existing == null) return null;

        existing.Name = request.Name;
        existing.Email = request.Email;
        existing.PhoneNum = request.PhoneNum;
        existing.Address = request.Address;
        existing.Dob = request.Dob;
        existing.Gender = request.Gender;

        await _customerRepo.Update(existing);
        return UserMapper.ToUserResponse(existing);
    }

    // Xóa khách hàng
    public async Task<bool> Delete(int id)
    {
        var customer = await _customerRepo.GetById(id);
        if (customer == null) return false;

        await _customerRepo.Delete(id);
        return true;
    }
}
