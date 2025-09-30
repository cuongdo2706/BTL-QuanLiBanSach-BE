namespace BTL_QuanLiBanSach.DTOs.Response;

public record UserResponse(
    long id,
    string address,
    string code,
    DateOnly? dob,
    string? email,
    bool? gender,
    string name,
    string? phoneNum,
    bool isDeleted,
    bool userType);