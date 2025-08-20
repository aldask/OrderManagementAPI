using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;
        
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

        [HttpPut("{id}/discount")]
        public async Task<ActionResult<ProductReadDTO>> UpdateProductDiscount(int id, [FromBody] ProductDiscountDTO productDiscountDTO)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductDiscountAsync(id, productDiscountDTO);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}