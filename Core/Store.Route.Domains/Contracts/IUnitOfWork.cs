using Store.Route.Domains.Entities;

namespace Store.Route.Domains.Contracts
{
    public interface IUnitOfWork  /*<TKey, TEntity> where TEntity : BaseEntity<TKey>*/
    {
        // Create Repository
        IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>;
        // Save Changes
        Task<int> SaveChangesAsync();

    }
}
