using Application.Interfaces.Services;
using Application.DTOs;
using Api.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public RestaurantController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Gets all orders, optionally sorted by total amount.
        /// </summary>
        /// <param name="sortByAmount">If true, orders are sorted by total amount ascending.</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool sortByAmount = false)
        {
            var result = await _orderService.GetAllOrdersAsync(sortByAmount);
            return this.HandleResult(result);
        }

        /// <summary>
        /// Gets a specific order by ID.
        /// </summary>
        /// <param name="id">The order ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return this.HandleResult(result);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateOrderAsync(dto);
            return this.HandleResult(result);
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <param name="status">The new status.</param>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] OrderStatus status)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, status);
            return this.HandleResult(result);
        }

        /// <summary>
        /// Calculates the total amount of an order.
        /// </summary>
        /// <param name="id">The order ID.</param>
        [HttpGet("{id}/total")]
        public async Task<IActionResult> GetTotal(int id)
        {
            var result = await _orderService.CalculateTotalAsync(id);
            return this.HandleResult(result);
        }
    }
}