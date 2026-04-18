namespace Store.Route.Domains.Entities.Order
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string userEmail, OrderAddress shiningAddress, DeliveryMethod deleveryMethod, ICollection<OrderItem> items, decimal subTotal, string? paymentIntentId)
        {
            UserEmail = userEmail;
            ShiningAddress = shiningAddress;
            DeleveryMethod = deleveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShiningAddress { get; set; }
        public DeliveryMethod DeleveryMethod { get; set; }  // Navigation Property [Order(1) => DeliveryMethod(M)) 

        public int DeliveryMethodId { get; set; } // FK

        public ICollection<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; } // Price * Quantity

        //[NotMapped]
        //public decimal Total { get; set; } // SubTotal + Deliverity Method Cost

        public decimal GetTotal() => SubTotal + DeleveryMethod.Price; // Not Mapped

        public string? PaymentIntentId { get; set; }
    }
}
