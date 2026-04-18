namespace Store.Route.Domains.Exceptions.NotFound
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"The Order With {id} is Not Found !!")
    {

    }
}
