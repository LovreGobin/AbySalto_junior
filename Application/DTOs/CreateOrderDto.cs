using Domain.Enums;
using System.ComponentModel;
using Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Customer name must be at least 2 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Delivery address must be at least 5 characters.")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?[0-9\s\-]{6,20}$", ErrorMessage = "Invalid phone number. Only digits, spaces, + and - are allowed.")]
        public string ContactNumber { get; set; } = string.Empty;
        public string? Note { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Currency is required.")]
        [DefaultValue(OrderConstants.DefaultCurrency)]
        public string Currency { get; set; } = OrderConstants.DefaultCurrency;

        [Required]
        [MinLength(1, ErrorMessage = "Order must have at least one item.")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
