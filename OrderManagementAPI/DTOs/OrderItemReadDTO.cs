namespace OrderManagementAPI.DTOs
{
    public class OrderItemReadDTO
    {
        public int Id { get; set; }
        public int ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? DiscountPercent { get; set; }
    }
}
