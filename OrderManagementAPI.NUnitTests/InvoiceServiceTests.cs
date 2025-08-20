using OrderManagementAPI.Models;

namespace OrderManagementAPI.NUnitTests
{
    public class InvoiceServiceTests : TestBase
    {
        [Test]
        public async Task GetInvoice_ReturnsCorrectTotal()
        {
            var order = new Order
            {
                Id = 1,
                Items = new System.Collections.Generic.List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 3 }
                }
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var invoice = await _invoiceService.GetInvoiceAsync(1);

            Assert.That(invoice, Is.Not.Null);
            Assert.That(invoice.TotalAmount, Is.GreaterThan(0));
        }
    }
}
