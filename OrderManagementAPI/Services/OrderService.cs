using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public class OrderService(AppDbContext context, IMapper mapper)
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<OrderReadDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items!)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return _mapper.Map<List<OrderReadDTO>>(orders);
        }

        public async Task<OrderReadDTO> GetOrderByIDAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items!)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            var orderDTO = _mapper.Map<OrderReadDTO>(order);

            foreach (var item in orderDTO.Items!)
            {
                if (item.DiscountPercent == null && item.Quantity > 0 &&
                    order.Items!.Any(i => i.Product!.Name == item.ProductName))
                {
                    var product = order.Items.First(i => i.Product!.Name == item.ProductName).Product!;
                    if (product.DiscountPercent.HasValue && product.MinQuantity.HasValue &&
                        item.Quantity >= product.MinQuantity.Value)
                    {
                        item.DiscountPercent = product.DiscountPercent.Value;
                    }
                    else
                    {
                        item.DiscountPercent = 0;
                    }
                }
            }

            return orderDTO;
        }

        public async Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<Order>(orderCreateDTO);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderReadDTO>(order);
        }
    }
}