using Application.Common;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Enums;
using Domain.Models;
using Domain.Constants;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderDto>>> GetAllOrdersAsync(bool sortByAmount)
        {
            var orders = await _orderRepository.GetAllAsync();

            if (sortByAmount)
                orders = orders.OrderBy(o =>
                {
                    var total = o.Items.Sum(i => i.Price * i.Quantity);
                    var rate = OrderConstants.ConversionRates.TryGetValue(o.Currency, out var r) ? r : 1.0m;
                    return total * rate;
                }).ToList();

            return Result<List<OrderDto>>.Success(orders.Select(MapToDto).ToList());
        }

        public async Task<Result<OrderDto>> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return Result<OrderDto>.Failure(new List<string> { $"Order with id {id} was not found." });

            return Result<OrderDto>.Success(MapToDto(order));
        }

        public async Task<Result<object>> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                PaymentMethod = dto.PaymentMethod,
                DeliveryAddress = dto.DeliveryAddress,
                ContactNumber = dto.ContactNumber,
                Note = dto.Note,
                Currency = string.IsNullOrWhiteSpace(dto.Currency) ? OrderConstants.DefaultCurrency : dto.Currency,
                Status = OrderStatus.Pending,
                OrderTime = DateTime.UtcNow,
                Items = dto.Items.Select(i => new OrderItem
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            return Result<object>.Success("Order created successfully.");
        }

        public async Task<Result<object>> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return Result<object>.Failure(new List<string> { $"Order with id {id} was not found." });

            order.Status = status;
            await _orderRepository.UpdateAsync(order);
            return Result<object>.Success("Order status updated.");
        }

        public async Task<Result<decimal>> CalculateTotalAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return Result<decimal>.Failure(new List<string> { $"Order with id {id} was not found." });

            return Result<decimal>.Success(order.Items.Sum(i => i.Price * i.Quantity));
        }

        private static OrderDto MapToDto(Order order) => new()
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Status = order.Status,
            OrderTime = order.OrderTime,
            PaymentMethod = order.PaymentMethod,
            DeliveryAddress = order.DeliveryAddress,
            ContactNumber = order.ContactNumber,
            Note = order.Note,
            Currency = order.Currency,
            TotalAmount = order.Items.Sum(i => i.Price * i.Quantity),
            Items = order.Items.Select(i => new OrderItemDto
            {
                Name = i.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}