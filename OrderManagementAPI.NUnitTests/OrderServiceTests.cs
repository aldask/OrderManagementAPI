using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.NUnitTests
{
    public class OrderServiceTests : TestBase
    {
        [Test]
        public async Task CreateOrder_AddsOrderSuccessfully()
        {
            var order = new Order
            {
                Id = 1,
                Items = new System.Collections.Generic.List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 3 }
                }
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var savedOrder = await _context.Orders.FindAsync(1);

            Assert.That(savedOrder, Is.Not.Null);
            Assert.That(savedOrder!.Items!.Count, Is.EqualTo(1));
            Assert.That(savedOrder.Items[0].Quantity, Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllOrders_ReturnsOrders()
        {
            _context.Orders.Add(new Order { Id = 1 });
            await _context.SaveChangesAsync();

            var orders = await _context.Orders.ToListAsync();

            Assert.That(orders.Count, Is.EqualTo(1));
        }
    }
}