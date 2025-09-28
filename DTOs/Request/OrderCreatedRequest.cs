namespace BTL_QuanLiBanSach.DTOs.Request;

public class OrderCreatedRequest
{
    public decimal amountPaid { get; set; }
    public long? customerId { get; set; }
    public string staffUsername{ get; set; }
    public bool paymentMethod{ get; set; }
    public List<OrderItems> orderItems { get; set; }
}


