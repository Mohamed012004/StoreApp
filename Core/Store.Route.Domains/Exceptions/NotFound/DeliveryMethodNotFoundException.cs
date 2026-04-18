namespace Store.Route.Domains.Exceptions.NotFound
{
    public class DeliveryMethodNotFoundException(int deliveryMethodId) : NotFoundException($"DeliveyMethod With {deliveryMethodId} is Not Found !!")
    {
    }
}
