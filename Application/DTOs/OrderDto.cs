using Domain.Enums;

namespace Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public DateTime OrderTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Note { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
