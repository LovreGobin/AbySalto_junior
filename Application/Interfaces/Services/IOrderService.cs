using Application.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync(bool sortByAmount);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(CreateOrderDto dto);
        Task UpdateOrderStatusAsync(int id, OrderStatus status);
        Task<decimal> CalculateTotalAsync(int id);
    }
}
