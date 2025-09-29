using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserResponse> FindAll()
    {
        return UserMapper.ToUserResponses(_userRepository.FindAll().Result);
    }

    public UserResponse? FindById(long id)
    {
        var user = _userRepository.FindById(id).Result;
        return user == null ? null : UserMapper.ToUserResponse(user);
    }

    public UserResponse Add(UserCreatedRequest request)
    {
        var user = new User
        {
            Code = request.code,
            Address = request.address ?? "",
            Dob = request.dob,
            Email = request.email,
            Gender = request.gender,
            Name = request.name,
            PhoneNum = request.phoneNum,
            UserType = true,   // nhân viên (1 = true)
            IsDeleted = false
        };

        var created = _userRepository.Add(user).Result;
        return UserMapper.ToUserResponse(created);
    }

    public UserResponse? Update(long id, UserUpdatedRequest request)
    {
        var user = new User
        {
            Id = id,
            Code = request.code,
            Address = request.address ?? "",
            Dob = request.dob,
            Email = request.email,
            Gender = request.gender,
            Name = request.name,
            PhoneNum = request.phoneNum,
            UserType = true,   // nhân viên
            IsDeleted = false
        };

        var updated = _userRepository.Update(id, user).Result;
        return updated == null ? null : UserMapper.ToUserResponse(updated);
    }

    public bool Delete(long id)
    {
        return _userRepository.Delete(id).Result;
    }
}
