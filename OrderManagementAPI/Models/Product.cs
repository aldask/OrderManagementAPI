namespace OrderManagementAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? MinQuantity { get; set; }
    }
}
