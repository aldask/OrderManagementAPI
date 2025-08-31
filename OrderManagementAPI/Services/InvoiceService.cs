using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;
using Serilog;

namespace OrderManagementAPI.Services
{
    public class InvoiceService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<OrderInvoiceReadDTO> GetInvoiceAsync(int orderId)
        {
            Log.Information("Generating invoice for Order ID: {OrderId}", orderId);

            var order = await _context.Orders
                .Include(o => o.Items!)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                Log.Warning("Order with ID {OrderId} not found.", orderId);
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            var invoiceItems = new List<OrderItemInvoiceReadDTO>();
            decimal totalAmount = 0;

            foreach (var item in order.Items!)
            {

                var product = item.Product;
                if (product == null) continue;

                var discount = CalculateDiscount(product, item.Quantity);
                var amount = item.Quantity * product.Price * (1 - discount / 100m);

                invoiceItems.Add(new OrderItemInvoiceReadDTO
                {
                    ProductName = product.Name!,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    DiscountPercent = discount,
                    Amount = amount
                });

                totalAmount += amount;
            }

            Log.Information("Invoice generated for Order {OrderId} with total {Total}", order.Id, totalAmount);

            return new OrderInvoiceReadDTO
            {
                OrderId = order.Id,
                Items = invoiceItems,
                TotalAmount = totalAmount
            };
        }

        public async Task<List<DiscountedProductReportDTO>> GetDiscountedProductsReportAsync()
        {
            var allItems = await _context.OrderItems
                .Include(oi => oi.Product)
                .ToListAsync();

            var reportDict = new Dictionary<int, DiscountedProductReportDTO>();

            foreach (var item in allItems)
            {
                var product = item.Product;
                if (product == null) continue;

                var discount = CalculateDiscount(product, item.Quantity);
                if (discount == 0) continue;

                var amount = item.Quantity * product.Price * (1 - discount / 100m);

                if (!reportDict.ContainsKey(product.Id))
                {
                    reportDict[product.Id] = new DiscountedProductReportDTO
                    {
                        ProductName = product.Name!,
                        DiscountPercent = discount,
                        OrdersCount = 0,
                        TotalAmount = 0
                    };
                }

                reportDict[product.Id].OrdersCount++;
                reportDict[product.Id].TotalAmount += amount;
            }

            Log.Information("Report generated with {Count} discounted products", reportDict.Count);

            return reportDict.Values.ToList();
        }
        private decimal CalculateDiscount(Product product, int quantity)
        {
            if (product.DiscountPercent.HasValue && product.MinQuantity.HasValue &&
                quantity >= product.MinQuantity.Value)
            {
                return product.DiscountPercent.Value;
            }

            return 0;
        }
    }
}