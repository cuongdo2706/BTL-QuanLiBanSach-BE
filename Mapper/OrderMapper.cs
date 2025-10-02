using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Mapper
{
    public class OrderMapper
    {
        public static OrderResponse ToResponse(Order order)
        {
            
            return new OrderResponse(
            order.Id,
            order.Code ?? "",
            order.SubTotal ?? 0m,
            order.GrandTotal ?? 0m,
            order.AmountPaid ?? 0m,
            order.ChangeAmount ?? 0m,
            order.Note ?? "",
            order.PaymentMethod ?? false,
            order.OrderedAt,
            new List<OrderDetailResponse>(),
            order.User != null ? UserMapper.ToResponse(order.User) : null,
            order.Staff != null ? UserMapper.ToResponse(order.Staff) : null
        );
        }

        public static Order ToEntity(OrderCreatedRequest request)
        {
            return new Order
            {
                CustomerId = request.customerId,
                StaffId = request.staffId,
                GrandTotal = request.totalPrice,
                PaymentMethod = request.paymentMethod,
                OrderedAt = DateTime.Now,
                OrderDetails = request.orderItems.Select(od => new OrderDetail
                {
                    ProductId = od.productid,
                    Quantity = od.quantity,
                    Price = od.price
                }).ToList(),
            };
        }
    }
}
