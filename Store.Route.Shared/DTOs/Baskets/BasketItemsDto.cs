namespace Store.Route.Shared.DTOs.Baskets
{
    public class BasketItemsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
