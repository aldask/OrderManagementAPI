using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    /// <summary>
    /// Handles operations related to orders.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<OrderReadDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">ID of the order.</param>
        /// <returns>Order details.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDTO>> GetOrdersByID(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIDAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderCreateDTO">Data for creating the order.</param>
        /// <returns>Created order details.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderReadDTO>> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (orderCreateDTO == null)
            {
                return BadRequest("Order data is null.");
            }

            var createdOrder = await _orderService.CreateOrderAsync(orderCreateDTO);
            return CreatedAtAction(nameof(GetOrdersByID), new { id = createdOrder.Id }, createdOrder);
        }
    }
}
