namespace BTL_QuanLiBanSach.DTOs.Request;

public class UserCreatedRequest
{
    public string code { get; set; }
    public string? address { get; set; }
    public DateOnly dob { get; set; }
    public string email { get; set; }
    public bool gender { get; set; }
    public string name { get; set; }
    public string phoneNum { get; set; }
}