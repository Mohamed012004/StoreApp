namespace Store.Route.Shared.DTOs.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemsDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
