using Application.DTOs;
using Domain.Enums;
using Application.Common;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<List<OrderDto>>> GetAllOrdersAsync(bool sortByAmount);
        Task<Result<OrderDto>> GetOrderByIdAsync(int id);
        Task<Result<object>> CreateOrderAsync(CreateOrderDto dto);
        Task<Result<object>> UpdateOrderStatusAsync(int id, OrderStatus status);
        Task<Result<decimal>> CalculateTotalAsync(int id);
    }
}
