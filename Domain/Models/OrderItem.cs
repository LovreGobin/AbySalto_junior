using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
