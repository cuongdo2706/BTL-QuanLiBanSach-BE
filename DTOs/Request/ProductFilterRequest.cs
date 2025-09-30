namespace BTL_QuanLiBanSach.DTOs.Request;

public class ProductFilterRequest
{
    public int page { get; set; }
    public int size { get; set; }
    public string? sortBy { get; set; }
    public string? nameOrCodeKeyword { get; set; }
    public bool isActive { get; set; } = true;
    public List<long>? categoryIds { get; set; }
    public List<long>? publisherIds { get; set; }
    public List<long>? authorIds { get; set; }
}