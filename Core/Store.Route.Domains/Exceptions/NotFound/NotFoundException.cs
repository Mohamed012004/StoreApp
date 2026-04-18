namespace Store.Route.Domains.Exceptions.NotFound
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string Message) : base(Message)
        {

        }
    }
}
