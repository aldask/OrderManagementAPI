namespace OrderManagementAPI.DTOs
{
    public class OrderItemReadDTO
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal Amount => Price * Quantity * (1 - (DiscountPercent ?? 0) / 100m);
    }
}
