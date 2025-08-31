using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;
using Serilog;

namespace OrderManagementAPI.Services
{
    public class ProductService(AppDbContext context, IMapper mapper)
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<ProductReadDTO>> GetAllProductsAsync(string? name = null)
        {
            Log.Information("Fetching all products from the DB with filter: {Name}", name ?? "None");

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(name));
            }

            var products = await query.ToListAsync();

            Log.Information("Retrieved {Count} products", products.Count);

            return _mapper.Map<List<ProductReadDTO>>(products);
        }

        public async Task<ProductReadDTO> GetProductByIDAsync(int id)
        {
            Log.Information("Fetching product with ID {Id} from the DB.", id);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            Log.Information("Retrieved product: {ProductName}", product.Name);

            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> CreateProductAsync(ProductCreateDTO productCreateDTO)
        {
            Log.Information("Creating a new product: {ProductName}", productCreateDTO.Name);

            var product = _mapper.Map<Product>(productCreateDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            Log.Information("Created product with ID {ProductId}", product.Id);

            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> UpdateProductDiscountAsync(int id, ProductDiscountDTO productDiscountDTO)
        {
            Log.Information("Updating discount for product ID {ProductId}", id);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            if (productDiscountDTO.DiscountPercent < 0 || productDiscountDTO.DiscountPercent > 100)
                throw new ArgumentOutOfRangeException(nameof(productDiscountDTO), "Discount percent must be between 0 and 100.");

            if (productDiscountDTO.MinQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(productDiscountDTO), "Minimum quantity cannot be negative.");

            _mapper.Map(productDiscountDTO, product);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            Log.Information("Updated discount for product ID {ProductId}", id);

            return _mapper.Map<ProductReadDTO>(product);
        }

    }
}
