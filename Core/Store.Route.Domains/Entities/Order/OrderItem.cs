namespace Store.Route.Domains.Entities.Order
{


    // Table
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductInOrderItem product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductInOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}