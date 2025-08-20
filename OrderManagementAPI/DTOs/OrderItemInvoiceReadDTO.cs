namespace OrderManagementAPI.DTOs
{
    public class OrderItemInvoiceReadDTO
    {
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal Amount { get; set; }
    }
}
