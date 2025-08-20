using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.Models;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.NUnitTests
{
    public class TestBase
    {
        protected AppDbContext _context = null!;
        protected OrderService _orderService = null!;
        protected ProductService _productService = null!;
        protected InvoiceService _invoiceService = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            var p1 = new Product { Id = 1, Name = "Product A", Price = 10m };
            var p2 = new Product { Id = 2, Name = "Product B", Price = 20m, DiscountPercent = 10, MinQuantity = 2 };
            _context.Products.AddRange(p1, p2);
            _context.SaveChanges();

            _orderService = new OrderService(_context, null!);
            _productService = new ProductService(_context, null!);
            _invoiceService = new InvoiceService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}