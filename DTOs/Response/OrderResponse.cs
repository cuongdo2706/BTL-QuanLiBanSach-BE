namespace BTL_QuanLiBanSach.DTOs.Response;

public record OrderResponse(
    long id,
    string code,
    decimal subTotal,
    decimal grandTotal,
    decimal amountPaid,
    decimal changeAmount,
    string note,
    bool paymentMethod,
    DateTime orderedAt,
    List<OrderDetailResponse> orderDetails,
    UserResponse? customer,
    UserResponse staff
);