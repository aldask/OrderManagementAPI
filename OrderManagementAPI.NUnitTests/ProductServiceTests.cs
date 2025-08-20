using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OrderManagementAPI.Models;
using System.Threading.Tasks;

namespace OrderManagementAPI.NUnitTests
{
    public class ProductServiceTests : TestBase
    {
        [Test]
        public async Task GetAllProducts_ReturnsSeededProducts()
        {
            var products = await _context.Products.ToListAsync();

            Assert.That(products.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateProduct_AddsProduct()
        {
            var product = new Product { Id = 3, Name = "Product C", Price = 15m };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var saved = await _context.Products.FindAsync(3);
            Assert.That(saved, Is.Not.Null);
            Assert.That(saved!.Name, Is.EqualTo("Product C"));
        }
    }
}