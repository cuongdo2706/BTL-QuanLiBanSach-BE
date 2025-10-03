using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
<<<<<<< HEAD
using BTL_QuanLiBanSach.Services;
=======
>>>>>>> origin/huy_huong

namespace BTL_QuanLiBanSach.Mapper;

public class UserMapper
{
    public static UserResponse ToUserResponse(User user)
    {
        return new UserResponse(
            user.Id,
<<<<<<< HEAD
            user.Address,
            user.Code,
            user.Dob,
            user.Email,
            user.Gender,
            user.Name,
            user.PhoneNum,
=======
            user.Code,
            user.Dob ?? default,        // nếu null thì trả default (0001-01-01)
            user.Email ?? "",
            user.Gender ?? false,
            user.Name,
            user.PhoneNum ?? "",
>>>>>>> origin/huy_huong
            user.UserType
        );
    }

    public static List<UserResponse> ToUserResponses(List<User> users)
    {
        return users.Select(ToUserResponse).ToList();
    }
<<<<<<< HEAD

    public static UserResponse ToResponse(User users)
    {
        return ToUserResponse(users);
    }
=======
>>>>>>> origin/huy_huong
}
