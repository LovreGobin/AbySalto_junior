using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Enums;
using Domain.Models;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync(bool sortByAmount)
        {
            var orders = await _orderRepository.GetAllAsync();

            if (sortByAmount)
                orders = orders.OrderBy(o => o.Items.Sum(i => i.Price * i.Quantity)).ToList();

            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                PaymentMethod = dto.PaymentMethod,
                DeliveryAddress = dto.DeliveryAddress,
                ContactNumber = dto.ContactNumber,
                Note = dto.Note,
                Currency = dto.Currency,
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
        }

        public async Task UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return;

            order.Status = status;
            await _orderRepository.UpdateAsync(order);
        }

        public async Task<decimal> CalculateTotalAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return 0;

            return order.Items.Sum(i => i.Price * i.Quantity);
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
