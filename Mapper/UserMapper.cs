using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Mapper;

public class UserMapper
{
    public static UserResponse ToUserResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.Code,
            user.Dob ?? default,        // nếu null thì trả default (0001-01-01)
            user.Email ?? "",
            user.Gender ?? false,
            user.Name,
            user.PhoneNum ?? "",
            user.UserType
        );
    }

    public static List<UserResponse> ToUserResponses(List<User> users)
    {
        return users.Select(ToUserResponse).ToList();
    }
}
