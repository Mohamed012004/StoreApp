namespace Store.Route.Shared.DTOs.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
    }
}