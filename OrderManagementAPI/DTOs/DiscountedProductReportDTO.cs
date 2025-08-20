namespace OrderManagementAPI.DTOs
{
    public class DiscountedProductReportDTO
    {
        public string ProductName { get; set; } = null!;
        public decimal DiscountPercent { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
