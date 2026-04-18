namespace Store.Route.Shared.DTOs.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddressDto ShiningAddress { get; set; }
        public string DeliveryMethod { get; set; }  // Navigation Property [Order(1) => DeliveryMethod(M)) 

        public ICollection<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; } // Price * Quantity
        public decimal Total { get; set; } // SubTotal + Deliverity Method Cost

    }
}
