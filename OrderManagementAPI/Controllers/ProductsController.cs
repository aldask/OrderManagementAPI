using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ProductReadDTO>>> GetAllProducts([FromQuery] string? name)
        {
            var products = await _productService.GetAllProductsAsync(name);
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDTO>> GetProductByID(int id)
        {
            try
            {
                var product = await _productService.GetProductByIDAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<ProductReadDTO>> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (productCreateDTO == null)
            {
                return BadRequest("Product data is null.");
            }
            var createdProduct = await _productService.CreateProductAsync(productCreateDTO);
            return CreatedAtAction(nameof(GetProductByID), new { id = createdProduct.Id }, createdProduct);
        }
    }
}