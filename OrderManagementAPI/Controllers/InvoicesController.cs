using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController(InvoiceService invoiceService) : ControllerBase
    {
        private readonly InvoiceService _invoiceService = invoiceService;

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