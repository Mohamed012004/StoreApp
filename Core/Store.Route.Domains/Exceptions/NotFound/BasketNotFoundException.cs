namespace Store.Route.Domains.Exceptions.NotFound
{
    public class BasketNotFoundException(string id) : NotFoundException($"The Basket With {id} is Not Found !!")
    {

    }
}
