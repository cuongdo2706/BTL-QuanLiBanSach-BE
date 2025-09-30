namespace BTL_QuanLiBanSach.DTOs.Request;

public class UserUpdatedRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNum { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateOnly? Dob { get; set; }
    public bool? Gender { get; set; }
}