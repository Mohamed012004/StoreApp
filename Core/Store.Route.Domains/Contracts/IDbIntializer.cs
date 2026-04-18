namespace Store.Route.Domains.Contracts
{
    public interface IDbIntializer
    {
        public Task InitializeAsync();
        public Task InitializeIdentityAsync();


    }
}
