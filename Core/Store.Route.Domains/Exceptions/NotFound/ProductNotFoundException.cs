namespace Store.Route.Domains.Exceptions.NotFound
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id)
            : base($"Product With {id} is Not Found !!")
        {

        }
    }
}
