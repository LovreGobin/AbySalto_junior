using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Note { get; set; }
        public string Currency { get; set; } = "EUR";
        public List<OrderItem> Items { get; set; } = new();
    }
}
