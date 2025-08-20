using OrderManagementAPI.DTOs;
using OrderManagementAPI.Data;
using OrderManagementAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementAPI.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderReadDTO>> GetAllOrdersAsync()
        {
            var order = await _context.Orders.ToListAsync();
            return _mapper.Map<List<OrderReadDTO>>(order);
        }

        public async Task<OrderReadDTO> GetOrderByIDAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            return _mapper.Map<OrderReadDTO>(order);
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
