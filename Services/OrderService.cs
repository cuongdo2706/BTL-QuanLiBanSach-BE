using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;
using BTL_QuanLiBanSach.Helpers;
using BTL_QuanLiBanSach.Mapper;
using BTL_QuanLiBanSach.Repositories;

namespace BTL_QuanLiBanSach.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<PageResponse<OrderResponse>> GetAllOrdersAsync(PagingRequest request)
        {
            var query = _orderRepository.GetAllOrdersQuery();
            var mappedQuery = query.Select(o => OrderMapper.ToResponse(o));
            return await mappedQuery.ToPagedResponseAsync(request);
        }
        public async Task<Order?> GetByIdAsync(long id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }
        public async Task<OrderResponse> CreateAsync(OrderCreatedRequest request)
        {
            var orderDetails = new List<OrderDetail>();

            foreach (var od in request.orderItems)
            {
                var product = await _productRepository.GetByIdAsync(od.productid);
                if (product == null) throw new Exception($"Product with id {od.productid} not found");

                orderDetails.Add(new OrderDetail
                {
                    ProductId = od.productid,
                    Quantity = od.quantity,
                    Price = od.price,
                    ProductName = product.Name,
                    ProductCode = product.Code,
                    TotalPrice = od.price * od.quantity
                });
            }
            var orderEntity = new Order
            {
                CustomerId = request.customerId,
                StaffId = request.staffId,
                GrandTotal = orderDetails.Sum(d => d.TotalPrice),
                SubTotal = orderDetails.Sum(d => d.TotalPrice),
                PaymentMethod = request.paymentMethod,
                OrderedAt = DateTime.Now,
                OrderDetails = orderDetails,
                AmountPaid = orderDetails.Sum(d => d.TotalPrice),
                Code = CodeGenerator.GenerateOrderCode(),
            };
            var createdOrder = await _orderRepository.AddAsync(orderEntity);

            return OrderMapper.ToResponse(createdOrder);
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
