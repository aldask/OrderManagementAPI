namespace OrderManagementAPI.DTOs
{
    public class OrderCreateDTO
    {
        public List<OrderItemCreateDTO>? Items { get; set; }
    }
}
