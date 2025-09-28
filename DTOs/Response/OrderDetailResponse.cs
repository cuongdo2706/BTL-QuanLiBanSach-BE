namespace BTL_QuanLiBanSach.DTOs.Response;

public record OrderDetailResponse(
    long id,
    decimal price,
    long productId,
    string productName,
    string productCode,
    int quantity,
    decimal totalPrice
);