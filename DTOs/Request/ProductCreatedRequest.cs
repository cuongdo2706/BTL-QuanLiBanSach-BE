namespace BTL_QuanLiBanSach.DTOs.Request;

public class ProductCreatedRequest
{
    public string code { get; set; }
    public string description { get; set; }
    public string name { get; set; }
    public int numOfPages { get; set; }
    public decimal price { get; set; }
    public int publishedYear{ get; set; }
    public int quantity{ get; set; }
    public long publisherId{ get; set; }
    public string translator{ get; set; }
    public List<long> authorIds{ get; set; }
    public List<long> categoryIds{ get; set; }
}