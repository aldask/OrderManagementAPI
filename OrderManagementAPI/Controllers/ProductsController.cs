using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    /// <summary>
    /// Handles operations related to products.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products with optional name filtering.
        /// </summary>
        /// <param name="name">Optional product name filter.</param>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductReadDTO>>> GetAllProducts([FromQuery] string? name)
        {
            var products = await _productService.GetAllProductsAsync(name);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>Product details.</returns>
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

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productCreateDTO">Data for creating the product.</param>
        /// <returns>Created product details.</returns>
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

        /// <summary>
        /// Updates the discount settings for a product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <param name="productDiscountDTO">Discount details to apply.</param>
        /// <returns>Updated product details.</returns>
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
