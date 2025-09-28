namespace BTL_QuanLiBanSach.DTOs.Request;

public class UserFilterRequest
{
    public int page { get; set; }
    public int size { get; set; }
    public string? sortBy { get; set; }
    public string? nameKeyword { get; set; }
}