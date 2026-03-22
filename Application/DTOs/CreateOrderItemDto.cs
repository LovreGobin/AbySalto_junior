using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Item name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [DefaultValue(1)]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int Quantity { get; set; } = 1;

        [DefaultValue(0.01)]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
        public decimal Price { get; set; } = 0.01m;
    }
}