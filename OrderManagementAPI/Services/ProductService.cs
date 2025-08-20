using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public class ProductService(AppDbContext context, IMapper mapper)
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<ProductReadDTO>> GetAllProductsAsync(string? name = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(name));
            }

            var products = await query.ToListAsync();

            return _mapper.Map<List<ProductReadDTO>>(products);
        }

        public async Task<ProductReadDTO> GetProductByIDAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> CreateProductAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> UpdateProductDiscountAsync(int id, ProductDiscountDTO productDiscountDTO)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            if (productDiscountDTO.DiscountPercent < 0 || productDiscountDTO.DiscountPercent > 100)
                throw new ArgumentOutOfRangeException(nameof(productDiscountDTO.DiscountPercent), "Discount percent must be between 0 and 100.");

            if (productDiscountDTO.MinQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(productDiscountDTO.MinQuantity), "Minimum quantity cannot be negative.");

            _mapper.Map(productDiscountDTO, product);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductReadDTO>(product);
        }

    }
}
