namespace BTL_QuanLiBanSach.DTOs.Request;

public class OrderCreatedRequest
{
    public decimal totalPrice { get; set; }
    public long customerId { get; set; }
    public long staffId { get; set; }
    public bool paymentMethod{ get; set; }
    public List<OrderItems> orderItems { get; set; }
}


