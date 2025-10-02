using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Mapper
{
    public static class OrderDetailMapper
    {
        public static OrderDetailResponse ToResponse(OrderDetail d)
        {
            return new OrderDetailResponse(
                d.Id,
                d.Price,
                d.ProductId,
                d.ProductName ?? "",
                d.ProductCode ?? "",
                d.Quantity,
                d.TotalPrice
            );
        }
    }

}
