namespace OrderManagementAPI.DTOs
{
    public class ProductCreateDTO
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? MinQuantity { get; set; }
    }
}
