namespace OrderManagementAPI.DTOs
{
    public class OrderInvoiceReadDTO
    {
        public int OrderId { get; set; }
        public List<OrderItemInvoiceReadDTO>? Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
