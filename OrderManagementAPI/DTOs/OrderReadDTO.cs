namespace OrderManagementAPI.DTOs
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public List<OrderItemReadDTO>? Items { get; set; }
    }
}
