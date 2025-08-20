using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    /// <summary>
    /// Handles operations related to invoices
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Retrieves the invoice for a specific order
        /// </summary>
        /// <param name="orderId">ID of the order</param>
        /// <returns>Order invoice details.</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderInvoiceReadDTO>> GetInvoice(int orderId)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceAsync(orderId);
                return Ok(invoice);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a report of all discounted products
        /// </summary>
        /// <returns>List of discounted products</returns>
        [HttpGet("discounted-products")]
        public async Task<ActionResult<List<DiscountedProductReportDTO>>> GetDiscountedProductsReport()
        {
            try
            {
                var report = await _invoiceService.GetDiscountedProductsReportAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}