using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Services;

namespace BTL_QuanLiBanSach.Mapper;

public class UserMapper
{
    public static UserResponse ToUserResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.Address,
            user.Code,
            user.Dob,
            user.Email,
            user.Gender,
            user.PhoneNum,
            user.Name,
            user.IsDeleted,
            user.UserType
        );
    }

    public static List<UserResponse> ToUserResponses(List<User> users)
    {
        return users.Select(ToUserResponse).ToList();
    }
}
