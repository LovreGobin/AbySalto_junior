using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Note { get; set; }

        [DefaultValue("EUR")]
        public string Currency { get; set; } = "EUR";
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
